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

        public ResponseGeneric<Model.Empleado> GetInfoEmpleado(string CodEmpleado)
        {
            using (IDbConnection connection = _connectionManager.GetConnection())
            {
                var resultado = connection.Query<Model.Empleado>(
                        "usp_Empleado_GetInfo",
                        param: new
                        {
                            CodEmpleado = CodEmpleado
                        },
                        commandTimeout: 300,
                        commandType: CommandType.StoredProcedure
                    );
                var response = resultado.FirstOrDefault();

                return new ResponseGeneric<Model.Empleado>(response) { Status = ResponseStatus.Success };
            }
        }

        public ResponseGeneric<IEnumerable<Model.Role>> GetRoles()
        {
            using (IDbConnection connection = _connectionManager.GetConnection())
            {
                var resultado = connection.Query<Model.Role>(
                        "usp_Roles_Listar",
                        commandTimeout: 300,
                        commandType: CommandType.StoredProcedure
                    );

                return new ResponseGeneric<IEnumerable<Model.Role>>(resultado) { Status = ResponseStatus.Success };
            }
        }

        public ResponseGeneric<Model.Empleado> SaveInfoEmpleado(Model.Empleado empleado)
        {
            using (IDbConnection connection = _connectionManager.GetConnection())
            {
                var resultado = connection.Query<Model.Empleado>(
                        "usp_Empleado_Guardar",
                        param: new
                        {
                            CodEmpleado = empleado.CodEmpleado,
                            Nombre = $"{CombineNombre(empleado.Nombre1, empleado.Nombre2)} {CombineNombre(empleado.Apellido1, empleado.Apellido2)}",
                            Correo = empleado.Correo,
                            Estado = empleado.Estado,
                            TipoDocumento = 1,
                            NumDocumento = empleado.NumDocumento,
                            Nombre1 = empleado.Nombre1,
                            Nombre2 = empleado.Nombre2,
                            Apellido1 = empleado.Apellido1,
                            Apellido2 = empleado.Apellido2,
                            FechaNacimiento = empleado.FechaNacimiento,
                            Direccion = empleado.Direccion,
                            TelMovil = empleado.TelMovil,
                            IdRol = empleado.IdRol
                        },
                        commandTimeout: 300,
                        commandType: CommandType.StoredProcedure
                    );
                var roleResult = resultado.FirstOrDefault();

                return new ResponseGeneric<Model.Empleado>(roleResult!) { Status = ResponseStatus.Success };
            }
        }

        private string CombineNombre(string parte1, string parte2)
        {
            // Verificar si ambos partes tienen valor
            if (!string.IsNullOrWhiteSpace(parte1) && !string.IsNullOrWhiteSpace(parte2))
            {
                // Combinar partes con un espacio entre ellas
                return $"{parte1} {parte2}";
            }
            else if (!string.IsNullOrWhiteSpace(parte1))
            {
                // Si solo parte1 tiene valor, devolver parte1
                return parte1;
            }
            else if (!string.IsNullOrWhiteSpace(parte2))
            {
                // Si solo parte2 tiene valor, devolver parte2
                return parte2;
            }
            else
            {
                // Si ambos son vacíos, devolver una cadena vacía
                return string.Empty;
            }
        }

    }
}
