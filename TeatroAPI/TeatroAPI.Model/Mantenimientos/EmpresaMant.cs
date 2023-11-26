using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeatroAPI.Model.Mantenimientos
{
    public class EmpresaMant
    {
        public int? IdEmpresa { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion {  get; set; }
        public string? Telefono { get; set; }
    }
}
