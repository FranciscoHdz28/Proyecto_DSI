using Microsoft.AspNetCore.DataProtection;
using TeatroAPI.BussinessLogic;
using TeatroAPI.BussinessLogic.Interface;
using TeatroAPI.DataAccess;
using TeatroAPI.DataAccess.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registra la dependencia ConnectionManager
builder.Services.AddScoped<IConnectionManager, ConnectionManager>();

// Registra la dependencia Empleado
builder.Services.AddScoped<TeatroAPI.DataAccess.Interface.IEmpleado, TeatroAPI.DataAccess.Empleado>();
builder.Services.AddScoped<TeatroAPI.BussinessLogic.Interface.IEmpleado, TeatroAPI.BussinessLogic.Empleado>();
builder.Services.AddScoped<TeatroAPI.BussinessLogic.Interface.ISeguridad, TeatroAPI.BussinessLogic.Seguridad>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
