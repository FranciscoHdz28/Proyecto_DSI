using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeatroAPI.BussinessLogic.Interface;
using TeatroAPI.Model;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {
        private readonly IEmpleado _empleado;
        private readonly ISeguridad _seguridad;

        public InicioSesionController(IEmpleado empleado, ISeguridad seguridad)
        {
            _empleado = empleado;
            _seguridad = seguridad;
        }

        [HttpGet("GetAllEmpleados")]
        public ResponseGeneric<IEnumerable<Empleado>> GetAllEmpleados()
        {
            try
            {
                var result = _empleado.GetAllEmpleados();
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<IEnumerable<Empleado>>(ex.Message);
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
