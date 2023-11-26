using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model.Mantenimientos;

namespace TeatroAPI.BussinessLogic
{
    public class Servicios : Interface.IServicios
    {
        private readonly DataAccess.Interface.IServicios _servicios;

        public Servicios(DataAccess.Interface.IServicios servicios)
        {
            _servicios = servicios;
        }

        public ResponseGeneric<IEnumerable<Model.Eventos.Servicios>> GetAllServicios()
        {
            try
            {
                return _servicios.GetAllServicios();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<IEnumerable<Model.Eventos.Servicios>>(ex);
            }
        }

        public ResponseGeneric<Model.Eventos.Servicios> SaveInfoServicio(Model.Eventos.Servicios servicio)
        {
            try
            {
                return _servicios.SaveInfoServicio(servicio);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Model.Eventos.Servicios>(ex);
            }
        }
    }
}
