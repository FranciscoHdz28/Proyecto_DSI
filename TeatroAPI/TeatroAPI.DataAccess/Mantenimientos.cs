using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model.Mantenimientos;

namespace TeatroAPI.DataAccess
{
    public class Mantenimientos : Interface.IMantenimientos
    {
        private readonly Interface.IConnectionManager _connectionManager;

        public Mantenimientos(Interface.IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public ResponseGeneric<IEnumerable<EmpresaMant>> GetAllEmpresas()
        {
            using (IDbConnection connection = _connectionManager.GetConnection())
            {
                var resultado = connection.Query<Model.Mantenimientos.EmpresaMant>(
                        "usp_EmpresasMant_Listar",
                        commandTimeout: 300,
                        commandType: CommandType.StoredProcedure
                    );

                return new ResponseGeneric<IEnumerable<Model.Mantenimientos.EmpresaMant>>(resultado) { Status = ResponseStatus.Success };
            }
        }

        public ResponseGeneric<EmpresaMant> SaveInfoEmpresa(EmpresaMant empresa)
        {
            using (IDbConnection connection = _connectionManager.GetConnection())
            {
                var resultado = connection.Query<Model.Mantenimientos.EmpresaMant>(
                        "usp_EmpresasMant_Guardar",
                        param: new
                        {
                            IdEmpresa = empresa.IdEmpresa,
                            Nombre = empresa.Nombre,
                            Direccion = empresa.Direccion,
                            Telefono = empresa.Telefono
                        },
                        commandTimeout: 300,
                        commandType: CommandType.StoredProcedure
                    );
                var empresaResult = resultado.FirstOrDefault();

                return new ResponseGeneric<Model.Mantenimientos.EmpresaMant>(empresaResult!) { Status = ResponseStatus.Success };
            }
        }
    }
}
