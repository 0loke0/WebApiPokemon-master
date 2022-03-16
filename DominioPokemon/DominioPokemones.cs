using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOS;

namespace DominioPokemon
{



    public class Pokemon
    {
        public string Nombre { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }

        //public string Tipo { get; set; }
        public Pokemon(string nombre)
        {
            this.Nombre = nombre;
            Validar();
            GenerarStats();
           
        }


        private void GenerarStats() {
            Random ram = new Random();

            Ataque = ram.Next(1, 100);
            Defensa = ram.Next(1, 100);
            EspecialAtaque = ram.Next(1, 100);
            EspecialDefensa = ram.Next(1, 100);
            Velocidad = ram.Next(1, 100);
            Vida = ram.Next(1, 100);           

        }

        private void Validar() {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception($"El nombre del Pokémon es requerido");
            }
            if (Nombre.Length >= 100)
            {
                throw new Exception($"El nombre del Pokémon ingresado supera la longitud de 100 letras");
            }        
        }
       



    }
}
