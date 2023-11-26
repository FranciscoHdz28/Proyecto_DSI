using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Eventos;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model.Mantenimientos;

namespace TeatroAPI.DataAccess
{
    public class Servicios : Interface.IServicios
    {
        private readonly Interface.IConnectionManager _connectionManager;

        public Servicios(Interface.IConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public ResponseGeneric<IEnumerable<Model.Eventos.Servicios>> GetAllServicios()
        {
            using (IDbConnection connection = _connectionManager.GetConnection())
            {
                var resultado = connection.Query<Model.Eventos.Servicios>(
                        "usp_Servicio_Listar",
                        commandTimeout: 300,
                        commandType: CommandType.StoredProcedure
                    );

                return new ResponseGeneric<IEnumerable<Model.Eventos.Servicios>>(resultado) { Status = ResponseStatus.Success };
            }
        }

        public ResponseGeneric<Model.Eventos.Servicios> SaveInfoServicio(Model.Eventos.Servicios servicio)
        {
            using (IDbConnection connection = _connectionManager.GetConnection())
            {
                var resultado = connection.Query<Model.Eventos.Servicios>(
                        "usp_Servicio_Guardar",
                        param: new
                        {
                            IdServicio = servicio.IdServicio,
                            Servicio = servicio.Servicio,
                            Tipo = servicio.Tipo,
                            Descripcion = servicio.Descripcion,
                            Precio = servicio.Precio
                        },
                        commandTimeout: 300,
                        commandType: CommandType.StoredProcedure
                    );
                var servicioResult = resultado.FirstOrDefault();

                return new ResponseGeneric<Model.Eventos.Servicios>(servicioResult!) { Status = ResponseStatus.Success };
            }
        }
    }
}
