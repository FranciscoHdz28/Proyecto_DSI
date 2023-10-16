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

        public InicioSesionController(IConfiguration config, IEmpleado empleado, ISeguridad seguridad)
        {
            _secret = config.GetSection("TokenSettings").GetSection("SecretKey").ToString()!;
            _empleado = empleado;
            _seguridad = seguridad;
        }

        [HttpPost("Autenticar")]
        public IActionResult Autenticar([FromBody] Credential credential)
        {
            if (credential.CodEmpleado == "ADMIN" && credential.Contrasenia == "123")
            {
                var keyBytes = Encoding.ASCII.GetBytes(_secret);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, credential.CodEmpleado));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);

                var response = new ResponseGeneric<TokenResult>
                {
                    Response = new TokenResult { Success = true, Token = tokencreado },
                    Status = ResponseStatus.Success
                };

                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                var fail = new ResponseGeneric<TokenResult> { CurrentException = "Credenciales no válidas"};
                return StatusCode(StatusCodes.Status401Unauthorized, fail);
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
    }
}
