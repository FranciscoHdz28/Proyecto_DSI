using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeatroAPI.BussinessLogic.Extensions
{
    public static class KeyValidator
    {
        public static (bool, string) ValidateKey(this string clave)
        {
            const int longitudMinima = 6;
            const int cantidadMinusculas = 1;
            const int cantidadMayusculas = 1;
            const int cantidadNumeros = 2;



            if (clave.Length < longitudMinima)
            {
                return (false, "La contraseña debe tener al menos 6 caracteres.");
            }



            if (!ClaveUtils.ContieneCaracterEspecial(clave))
            {
                return (false, "La contraseña debe contener caracteres especiales.");
            }



            if (ClaveUtils.ContarLetrasMinusculas(clave) < cantidadMinusculas)
            {
                return (false, "La contraseña debe tener al menos una letra minúscula.");
            }



            if (ClaveUtils.ContarLetrasMayusculas(clave) < cantidadMayusculas)
            {
                return (false, "La contraseña debe tener al menos una letra mayúscula.");
            }



            if (ClaveUtils.ContarNumeros(clave) < cantidadNumeros)
            {
                return (false, "La contraseña debe tener al menos 2 números.");
            }



            return (true, "Contraseña válida");
        }



        public static TimeSpan GetKeyExpiration(this DateTime startDate)
        {
            DateTime fechaActual = DateTime.Now;
            return fechaActual - startDate;
        }



        public static class ClaveUtils
        {
            public static bool ContieneCaracterEspecial(string clave)
            {
                return !clave.All(c => char.IsLetterOrDigit(c));
            }



            public static int ContarLetrasMinusculas(string clave)
            {
                return clave.Count(c => char.IsLower(c));
            }



            public static int ContarLetrasMayusculas(string clave)
            {
                return clave.Count(c => char.IsUpper(c));
            }



            public static int ContarNumeros(string clave)
            {
                return clave.Count(c => char.IsDigit(c));
            }
        }
    }
}
