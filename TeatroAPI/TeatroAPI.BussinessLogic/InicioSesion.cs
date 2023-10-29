using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeatroAPI.Model.Generico;
using TeatroAPI.Model;
using TeatroAPI.BussinessLogic.Interface;

namespace TeatroAPI.BussinessLogic
{
    public class InicioSesion : Interface.IInicioSesion
    {
        private readonly DataAccess.Interface.IInicioSesion _inicioSesion;

        public InicioSesion(DataAccess.Interface.IInicioSesion inicioSesion)
        {
            _inicioSesion = inicioSesion;
        }

        public ResponseGeneric<IEnumerable<AuthenticationResponse>> Autenticar(Credential credential)
        {
            try
            {
                return _inicioSesion.Autenticar(credential);
            }
            catch (Exception ex)
            {
                return new ResponseGeneric<IEnumerable<AuthenticationResponse>>()
                {
                    Status = ResponseStatus.Failed,
                    CurrentException = ex.Message
                };
            }
        }
    }
}
