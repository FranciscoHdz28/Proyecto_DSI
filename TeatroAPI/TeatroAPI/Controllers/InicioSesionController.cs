using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeatroAPI.BussinessLogic.Interface;
using TeatroAPI.Model;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        private readonly string _secret;
        private readonly IEmpleado _empleado;
        private readonly ISeguridad _seguridad;
        private readonly IInicioSesion _login;
        private readonly IConfiguration _config;
        private readonly ITokenBuilder _tokenBuilder;

        public InicioSesionController(IConfiguration config, IEmpleado empleado, ISeguridad seguridad,
                                        IInicioSesion login, IConfiguration configuration, ITokenBuilder tokenBuilder)
        {
            _secret = config.GetSection("TokenSettings").GetSection("SecretKey").ToString()!;
            _empleado = empleado;
            _seguridad = seguridad;
            _login = login;
            _config = configuration;
            _tokenBuilder = tokenBuilder;
        }

        [HttpPost("Autenticar")]
        public async Task<ResponseGeneric<TokenResult>> Autenticar([FromBody] Credential credential)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new ResponseGeneric<TokenResult>()
                    {
                        Status = ResponseStatus.Failed,
                        CurrentException = "Modelo no válido",
                    };
                }

                var claveEncriptada = _seguridad.EncriptarClave(credential.Contrasenia!);
                credential.Contrasenia = claveEncriptada.Response;
                var response = _login.Autenticar(credential);

                if (response.Status == ResponseStatus.Failed)
                {
                    return new ResponseGeneric<TokenResult>() { CurrentException = response.CurrentException };
                }

                if (response.Response!.FirstOrDefault()!.Autenticado)
                {
                    //var keyBytes = Encoding.ASCII.GetBytes(_secret);
                    var claims = new ClaimsIdentity();
                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, credential.CodEmpleado!));

                    var key = _config["Jwt:Key"];
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer = _config["Jwt:Issuer"],
                        Audience = _config["Jwt:Audience"],
                        Expires = DateTime.UtcNow.AddMinutes(60),
                        SigningCredentials = credentials
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokencreado = tokenHandler.WriteToken(tokenConfig);

                    var result = new ResponseGeneric<TokenResult>
                    {
                        Response = new TokenResult { Success = true, Token = tokencreado },
                        Status = ResponseStatus.Success
                    };

                    return result;
                }
                else
                {
                    var fail = new ResponseGeneric<TokenResult> { CurrentException = response.Response!.FirstOrDefault()!.Mensaje };
                    return fail;
                }
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<TokenResult> { };
            }
        }

        [HttpPost("EncriptarClave")]
        public ResponseGeneric<string> EncriptarClave([FromBody]string clave)
        {
            try
            {
                var result = _seguridad.EncriptarClave(clave);
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<string>(ex.Message);
            }
        }

        [HttpPost("DesencriptarClave")]
        public ResponseGeneric<string> DesencriptarClave([FromBody] string clave)
        {
            try
            {
                var result = _seguridad.DesencriptarClave(clave);
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<string>(ex.Message);
            }
        }

        //[HttpGet("ObtenerApplicationToken")]
        //public string ObtenerApplicationToken()
        //{
        //    try
        //    {
        //        var result = _tokenBuilder.ObtenerApplicationToken();
        //        return result;
        //    }
        //    catch(Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
    }
}
