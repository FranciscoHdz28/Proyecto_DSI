using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeatroAPI.BussinessLogic.Interface;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model.Mantenimientos;

namespace TeatroAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class MantenimientosController : ControllerBase
    {
        private readonly IMantenimientos _mantenimientos;

        public MantenimientosController(IMantenimientos mantenimientos)
        {
            _mantenimientos = mantenimientos;
        }

        [HttpGet("GetAllEmpresas")]
        public ResponseGeneric<IEnumerable<EmpresaMant>> GetAllEmpresas()
        {
            try
            {
                var result = _mantenimientos.GetAllEmpresas();
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<IEnumerable<Model.Mantenimientos.EmpresaMant>>(ex.Message);
            }
        }

        [HttpPost("SaveInfoEmpresa")]
        public ResponseGeneric<EmpresaMant> SaveInfoEmpresa(EmpresaMant empresa)
        {
            try
            {
                var result = _mantenimientos.SaveInfoEmpresa(empresa);
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<Model.Mantenimientos.EmpresaMant>(ex.Message);
            }
        }
    }
}
