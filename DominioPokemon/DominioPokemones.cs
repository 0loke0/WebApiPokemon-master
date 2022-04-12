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
        public string Rareza { get; set; }
        public string Detalle { get; set; }


        //public string Tipo { get; set; }
        public Pokemon(string nombre,string detalle)
        {
            this.Nombre = nombre;
            this.Detalle = detalle;
            Validar();
            GenerarStats();
            GenerarRareza();
        }


        private void GenerarStats()
        {
            Random ram = new Random();

            Ataque = ram.Next(1, 100);
            Defensa = ram.Next(1, 100);
            EspecialAtaque = ram.Next(1, 100);
            EspecialDefensa = ram.Next(1, 100);
            Velocidad = ram.Next(1, 100);
            Vida = ram.Next(1, 100);

        }
        private double logartimoDeterminarRareza(double x)
        {
            return 300 * Math.Log10(x);
        }
        private void GenerarRareza()
        {
            int sumatoriaCaracteristicas = Ataque + Defensa + EspecialAtaque + EspecialDefensa + Velocidad + Vida;





            if (sumatoriaCaracteristicas < logartimoDeterminarRareza((100 / 6)))
            {
                Rareza = "Comun";
                return;
            }
            if (sumatoriaCaracteristicas < logartimoDeterminarRareza((100 / 4.8)))
            {
                Rareza = "Poco comun";
                return;
            }
            if (sumatoriaCaracteristicas < logartimoDeterminarRareza((100 / 3.6)))
            {
                Rareza = "Rara";
                return;
            }
            if (sumatoriaCaracteristicas < logartimoDeterminarRareza((100 / 2.4)))
            {
                Rareza = "Epica";
                return;
            }
            if (sumatoriaCaracteristicas < logartimoDeterminarRareza((100 / 1.2)))
            {
                Rareza = "Epica Singular";
                return;
            }
            if (sumatoriaCaracteristicas > logartimoDeterminarRareza((100 / 1.2)))
            {
                Rareza = "Legendaria";
                return;
            }




        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(Nombre))
            {
                throw new Exception($"El nombre del Pokémon es requerido");
            }
            if (Nombre.Length >= 100)
            {
                throw new Exception($"El nombre del Pokémon ingresado supera la longitud de 100 letras");
            }
            if (string.IsNullOrEmpty(Detalle)) 
            { 
                throw new Exception($"La descripcion del pokemon a agregar no contiene informacion"); 
            }

        }




    }
}
