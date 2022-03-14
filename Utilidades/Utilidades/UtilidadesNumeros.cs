using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades.Utilidades
{
   public  class UtilidadesNumeros 
    {
        Random ram = new Random();
        public int NumeroRamdom(int min, int max)
        {
            return  ram.Next(min, max);
        }
    }
}
