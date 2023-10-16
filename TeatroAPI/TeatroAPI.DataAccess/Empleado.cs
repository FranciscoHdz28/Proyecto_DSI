using Dapper;
using System.Data;
using TeatroAPI.Model.Generico;


namespace TeatroAPI.DataAccess
{
    public class Empleado : Interface.IEmpleado
    {
        private readonly Interface.IConnectionManager _connectionManager;

        public Empleado(Interface.IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public ResponseGeneric<IEnumerable<Model.Empleado>> GetAllEmpleados()
        {
            try
            {
                using (IDbConnection connection = _connectionManager.GetConnection())
                {
                    string query = "SELECT * FROM Empleado";
                    var empleados = connection.Query<Model.Empleado>(query);
                    return new ResponseGeneric<IEnumerable<Model.Empleado>>(empleados) { Status = ResponseStatus.Success};
                }
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<IEnumerable<Model.Empleado>>(ex) { Status = ResponseStatus.Failed, CurrentException = ex.Message};
            }
        }
    }
}
