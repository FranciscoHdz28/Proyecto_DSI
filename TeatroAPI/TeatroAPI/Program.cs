using Microsoft.AspNetCore.DataProtection;
using TeatroAPI.BussinessLogic;
using TeatroAPI.BussinessLogic.Interface;
using TeatroAPI.DataAccess;
using TeatroAPI.DataAccess.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using TeatroAPI.Model.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Configuration.AddJsonFile("appsettings.json");
//var secret = builder.Configuration.GetSection("TokenSettings").GetSection("SecretKey").ToString();
//var keyBytes = Encoding.UTF8.GetBytes(secret!);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Teatro API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Ingresar Token de seguridad",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra la dependencia ConnectionManager
builder.Services.AddScoped<IConnectionManager, ConnectionManager>();

// Registra las dependencias desde el archivo dependencies.json
var dependencyList = LoadDependencyConfiguration();
foreach (var dependencyConfig in dependencyList.DependencyConfiguration)
{
    var fromType = Type.GetType(dependencyConfig.From);
    var toType = Type.GetType(dependencyConfig.To);
    if (fromType != null && toType != null)
    {
        if (dependencyConfig.DependencyType == "Scoped")
        {
            builder.Services.AddScoped(fromType, toType);
        }
        else if (dependencyConfig.DependencyType == "Transient")
        {
            builder.Services.AddTransient(fromType, toType);
        }
        else if (dependencyConfig.DependencyType == "Singleton")
        {
            builder.Services.AddSingleton(fromType, toType);
        }
    }
}

DependencyList LoadDependencyConfiguration()
{
    var jsonString = File.ReadAllText("dependencies.json");
    var configuration = JsonSerializer.Deserialize<DependencyList>(jsonString);
    return configuration;
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
