using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOS;

namespace DominioPokemon
{
    
    public class DominioStat 
    {
        public int IdPokemon { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }

        public DominioStat(int IdPokemon, int Ataque, int Defensa, int EspecialAtaque, int EspecialDefensa, int Velocidad, int Vida)
        {
            this.IdPokemon=IdPokemon;
            this.Ataque = Ataque;
            this.Defensa = Defensa;
            this.EspecialAtaque = EspecialAtaque;
            this.EspecialDefensa = EspecialDefensa;
            this.Velocidad =Velocidad;
            this.Vida = Vida;
            Validar();
        }

        //todo: cambiar los valores de ingreso para el genero no esta limpio
        private string GenerarTextoError(string tipo, string genero, int valor)
        {

            string preTipo;
            string preValor;
            if (genero == "Masculino")
            {
                preTipo = "el";
                preValor = "negativo";
            }
            else
            {
                preTipo = "la";
                preValor = "negativa";
            }
            string mensaje = $"Para actualizar la información {preTipo} {tipo} no puede ser {preValor}: {valor}";

            return mensaje;
        }

        //todo:Como se puede validar la existencia de un id pokemon sin depender del repositorio //Respueta se deja en el servicio
       
        private void Validar()
        {
            if (IdPokemon == 0)
            {
                throw new Exception($"Para actualizar el stat, el Id del pokemon no puede ser Cero");
            }
            if (Ataque < 0)
            {
                throw new Exception(GenerarTextoError("Ataque", "Masculino", Ataque));
            }
            if (Defensa < 0)
            {
                throw new Exception(GenerarTextoError("Defensa", "", Defensa));
            }
            if (EspecialAtaque < 0)
            {
                throw new Exception(GenerarTextoError("Ataque Especial", "Masculino", EspecialAtaque));
            }
            if (EspecialDefensa < 0)
            {
                throw new Exception(GenerarTextoError("Defensa Especial", "", EspecialDefensa));
            }
            if (Velocidad < 0)
            {
                throw new Exception(GenerarTextoError("Velocidad", "", Velocidad));
            }
            if (Vida < 0)
            {
                throw new Exception(GenerarTextoError("Vida", "", Vida));
            }            
        }
    }
}