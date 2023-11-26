using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model.Mantenimientos;

namespace TeatroAPI.BussinessLogic
{
    public class Mantenimientos : Interface.IMantenimientos
    {
        private readonly DataAccess.Interface.IMantenimientos _mantenimientos;

        public Mantenimientos(DataAccess.Interface.IMantenimientos mantenimientos)
        {
            _mantenimientos = mantenimientos;
        }

        public ResponseGeneric<IEnumerable<EmpresaMant>> GetAllEmpresas()
        {
            try
            {
                return _mantenimientos.GetAllEmpresas();
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<IEnumerable<Model.Mantenimientos.EmpresaMant>>(ex);
            }
        }

        public ResponseGeneric<EmpresaMant> SaveInfoEmpresa(EmpresaMant empresa)
        {
            try
            {
                return _mantenimientos.SaveInfoEmpresa(empresa);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<Model.Mantenimientos.EmpresaMant>(ex);
            }
        }
    }
}
