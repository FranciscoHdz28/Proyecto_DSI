using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.DataAccess.Interface
{
    public interface IEmpleado
    {
        ResponseGeneric<IEnumerable<Empleado>> GetAllEmpleados();
    }
}
