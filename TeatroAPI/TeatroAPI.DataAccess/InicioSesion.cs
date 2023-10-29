using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.DataAccess
{
    public class InicioSesion : Interface.IInicioSesion
    {
        private readonly Interface.IConnectionManager _connectionManager;

        public InicioSesion(Interface.IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public ResponseGeneric<IEnumerable<AuthenticationResponse>> Autenticar(Credential credential)
        {
            using (var connection = _connectionManager.GetConnection())
            {
                var response = connection.Query<AuthenticationResponse>(
                        "usp_Autenticar",
                        param: new
                        {
                            Empleado = credential.CodEmpleado,
                            Clave = credential.Contrasenia
                        },
                        commandTimeout: 300,
                        commandType: System.Data.CommandType.StoredProcedure
                    );
                return new ResponseGeneric<IEnumerable<AuthenticationResponse>>(response) { Status = ResponseStatus.Success };
            }
        }
    }
}
