using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model.Mantenimientos;

namespace TeatroAPI.DataAccess.Interface
{
    public interface IMantenimientos
    {
        ResponseGeneric<IEnumerable<EmpresaMant>> GetAllEmpresas();
        ResponseGeneric<EmpresaMant> SaveInfoEmpresa(EmpresaMant empresa);
    }
}
