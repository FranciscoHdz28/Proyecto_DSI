using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.DataAccess.Interface
{
    public interface IServicios
    {
        ResponseGeneric<IEnumerable<Model.Eventos.Servicios>> GetAllServicios();
        ResponseGeneric<Model.Eventos.Servicios> SaveInfoServicio(Model.Eventos.Servicios servicio);
    }
}
