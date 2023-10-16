namespace TeatroAPI.Model
{
    public class Empleado
    {
        public string CodEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public int Estado { get; set; }
        public int TipoDocumento { get; set; }
        public string NumDocumento { get; set; }
        public string Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdClave { get; set; }
        public string? Direccion { get; set; }
        public string? TelMovil { get; set; }
        public int? IntentosAutenticacion { get; set; }
    }
}