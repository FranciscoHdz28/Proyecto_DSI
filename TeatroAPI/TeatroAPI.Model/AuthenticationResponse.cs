using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeatroAPI.Model
{
    public class AuthenticationResponse
    {
        public DateTime Fecha_Aut {  get; set; }
        public bool Autenticado {  get; set; }
        public string Mensaje { get; set; }
    }
}
