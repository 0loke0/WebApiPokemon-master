using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestruraPokemon.Migrations.Utilidades
{
   public  class UtilidadesNumeros : IUtilidadesNumeros
    {
        Random ram = new Random();
        public int NumeroRamdom(int min, int max)
        {
            return  ram.Next(min, max);
        }
    }
}
