using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.Utilidades
{
    public class UtilidadesImagenes
    {
        public  byte[] ConvertirDeBase64Aimagen(string base64String)
        {
            int inicioBase64 = base64String.IndexOf(",", 0) + 1;
            string imagenBase64 = base64String.Substring(inicioBase64);
            return Convert.FromBase64String(imagenBase64);
        }


    }
}
