﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeatroAPI.BussinessLogic.Interface;
using TeatroAPI.Model;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleado _empleado;

        public EmpleadoController(IEmpleado empleado)
        {
            _empleado = empleado;
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

        [HttpGet("GetInfoEmpleado")]
        public ResponseGeneric<Model.Empleado> GetInfoEmpleado(string CodEmpleado)
        {
            try
            {
                var result = _empleado.GetInfoEmpleado(CodEmpleado);
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<Model.Empleado>(ex.Message);
            }
        }

        [HttpGet("GetRoles")]
        public ResponseGeneric<IEnumerable<Model.Role>> GetRoles()
        {
            try
            {
                var result = _empleado.GetRoles();
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<IEnumerable<Model.Role>>(ex.Message);
            }
        }

        [HttpPost("SaveInfoEmpleado")]
        public ResponseGeneric<Model.Empleado> SaveInfoEmpleado(Model.Empleado empleado)
        {
            try
            {
                var result = _empleado.SaveInfoEmpleado(empleado);
                return result;
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new ResponseGeneric<Model.Empleado>(ex.Message);
            }
        }
    }
}
