using TeatroAPI.Model.Generico;

namespace TeatroAPI.BussinessLogic
{
    public class Empleado : Interface.IEmpleado
    {
        private readonly DataAccess.Interface.IEmpleado _empleado;

        public Empleado(DataAccess.Interface.IEmpleado empleado)
        {
            _empleado = empleado;
        }

        public ResponseGeneric<IEnumerable<Model.Empleado>> GetAllEmpleados()
        {
            try
            {
                return _empleado.GetAllEmpleados();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<IEnumerable<Model.Empleado>>(ex);
            }
        }

        public ResponseGeneric<Model.Empleado> GetInfoEmpleado(string CodEmpleado)
        {
            try
            {
                return _empleado.GetInfoEmpleado(CodEmpleado);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Model.Empleado>(ex);
            }
        }

        public ResponseGeneric<IEnumerable<Model.Role>> GetRoles()
        {
            try
            {
                return _empleado.GetRoles();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<IEnumerable<Model.Role>>(ex);
            }
        }

        public ResponseGeneric<Model.Empleado> SaveInfoEmpleado(Model.Empleado empleado)
        {
            try
            {
                return _empleado.SaveInfoEmpleado(empleado);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Model.Empleado>(ex);
            }
        }
    }
}
