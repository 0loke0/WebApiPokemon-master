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
        //    repositorioStats.GuardarStatsAleatorio(idPokemonGuardado);
        }
        //public void validacionNombreDtoPokemon(string NombrePokemon) {
          
        //}
        //public void validacionIdDtoPokemon(DTOPokemon pokemon) {
        //    if (pokemon.Id < 0) {
        //        throw new Exception($"El Id del Pokémon no puede ser negativo: {pokemon.Id}");
        //    }
        //}
        //public void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon) {
        //    validacionNombreDtoPokemon(nuevoPokemon.NombrePokemon);
        //    var idPokemonGuardado = repositorioPokemon.GuardarPokemon(nuevoPokemon);


        //    var nuevaRelacionPokemonTipo = new DTODirectorioTipos();
        //    nuevaRelacionPokemonTipo.IdPokemon = idPokemonGuardado;
        //    nuevaRelacionPokemonTipo.IdTipo = nuevoPokemon.IdTipo;
        //    repositorioDirectorioTipos.GuardarRelacion(nuevaRelacionPokemonTipo);
        //}



    }
}
