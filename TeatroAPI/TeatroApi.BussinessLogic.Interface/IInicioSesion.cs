using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model;

namespace TeatroAPI.BussinessLogic.Interface
{
    public interface IInicioSesion
    {
        ResponseGeneric<IEnumerable<AuthenticationResponse>> Autenticar(Credential credential);
    }
}
