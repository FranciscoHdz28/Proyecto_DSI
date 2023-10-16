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
    }
}
