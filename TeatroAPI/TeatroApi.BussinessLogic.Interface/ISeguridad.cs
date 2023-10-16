using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.BussinessLogic.Interface
{
    public interface ISeguridad
    {
        ResponseGeneric<string> EncriptarClave(string clave);

        ResponseGeneric<string> DesencriptarClave(string clave);
    }
}
