using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Todo:Creo que esto va en global pero es necesario preguntar : si señor y son static
namespace InfraestruraPokemon.Migrations.Utilidades
{
    class UtilidadesString : IUtilidadesString
    {
        //Todo: pendiente mejorar la convercion para capitalizar texto 
        public string PrimeraMayuscula(string texto) {            
            char[] letras = texto.ToCharArray();
            letras[0] = char.ToUpper(letras[0]);
            return new string(letras);
        }
    }
}
