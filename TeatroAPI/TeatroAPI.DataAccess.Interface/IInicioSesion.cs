using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model;
using TeatroAPI.Model.Generico;

namespace TeatroAPI.DataAccess.Interface
{
    public interface IInicioSesion
    {
        ResponseGeneric<IEnumerable<AuthenticationResponse>> Autenticar(Credential credential);
    }
}
