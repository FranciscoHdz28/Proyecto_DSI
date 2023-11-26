using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeatroAPI.Model.Eventos
{
    public class Servicios
    {
        public int? IdServicio { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Servicio { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Tipo { get; set; }

        [MaxLength(200)]
        public string? Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }
}
