using Microsoft.AspNetCore.Mvc;
using TeatroAPI.BussinessLogic.Interface;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model.Mantenimientos;

namespace TeatroAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicios _servicios;

        public ServiciosController(IServicios servicios)
        {
            _servicios = servicios;
        }

        [HttpGet("GetAllServicios")]
        public ResponseGeneric<IEnumerable<Model.Eventos.Servicios>> GetAllServicios()
        {
            try
            {
                var result = _servicios.GetAllServicios();
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<IEnumerable<Model.Eventos.Servicios>>(ex.Message);
            }
        }

        [HttpPost("SaveInfoServicio")]
        public ResponseGeneric<Model.Eventos.Servicios> SaveInfoServicio(Model.Eventos.Servicios servicio)
        {
            try
            {
                var result = _servicios.SaveInfoServicio(servicio);
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<Model.Eventos.Servicios>(ex.Message);
            }
        }
    }
}
