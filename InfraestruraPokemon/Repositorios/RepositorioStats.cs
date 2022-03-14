using DTOsPokemon.DTOS;
using InfraestructuraPokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfraestructuraPokemon.Modelos;
using DominioPokemon;





namespace InfraestructuraPokemon.Repositorios
{
    public interface IRepositorioStats
    {
        IEnumerable<DTOStats> LeerStats();
        DTOStats BuscarStat(int idPokemon);

        //todo: REfactorizar para que no ingrese el Dominio de pokemon
        void GuardarStat(Pokemon pokemon,int idPokemonGuardado);
        void GuardarStatGenerico(DominioStat dominioStat);
        void EliminarStat(int idPokemon);
        void ActualizarStat(DTOStats stat);
        void GuardarStatsAleatorio(int idPokemon);
    }
    public class RepositorioStats : IRepositorioStats
    {
        private readonly ContextoPokemon contextoPokemon;
        public RepositorioStats(ContextoPokemon contextoPokemon)
        {
            this.contextoPokemon = contextoPokemon;
        }


        private DTOStats ConvertirADto(Stats stat)
        {
            return new DTOStats
            {
                IdPokemon = stat.IdPokemon,
                Ataque = stat.Ataque,
                Defensa = stat.Defensa,
                EspecialAtaque = stat.EspecialAtaque,
                EspecialDefensa = stat.EspecialDefensa,
                Velocidad = stat.Velocidad,
                Vida = stat.Vida
            };
        }

        private Stats ConvertirDeDominioPokemonAPersistencia(Pokemon pokemon, int idPokemonGuardado)
        {
            return new Stats
            {
                IdPokemon = idPokemonGuardado,
                Ataque = pokemon.Ataque,
                Defensa = pokemon.Defensa,
                EspecialAtaque = pokemon.EspecialAtaque,
                EspecialDefensa = pokemon.EspecialDefensa,
                Velocidad = pokemon.Velocidad,
                Vida = pokemon.Vida
            };
        }

        private Stats ConvertirDeDominioStatsAPersistencia(DominioStat dominioStat)
        {
           
            return new Stats
            {
                IdPokemon = dominioStat.IdPokemon,
                Ataque = dominioStat.Ataque,
                Defensa = dominioStat.Defensa,
                EspecialAtaque = dominioStat.EspecialAtaque,
                EspecialDefensa = dominioStat.EspecialDefensa,
                Velocidad = dominioStat.Velocidad,
                Vida = dominioStat.Vida
            };
        }
        public void ActualizarStat(DTOStats stat)
        {
            var data = contextoPokemon.Stats.Where(x => x.IdPokemon == stat.IdPokemon).SingleOrDefault();

            if (data == null)
            {
                throw new Exception($"No se encontro Tipo con Id {stat.IdPokemon} para actializar");
            }
            data.Ataque = stat.Ataque;
            data.Defensa = stat.Defensa;
            data.EspecialAtaque = stat.EspecialAtaque;
            data.EspecialDefensa = stat.EspecialDefensa;
            data.Velocidad = stat.Velocidad;
            data.Vida = stat.Vida;

            contextoPokemon.SaveChanges();
        }

        public DTOStats BuscarStat(int idPokemon)
        {
            var data = contextoPokemon.Stats.Where(x => x.IdPokemon == idPokemon).ToList().Select(x => ConvertirADto(x)).FirstOrDefault();
            return data;
        }

        public  void EliminarStat (int idPokemon)
        {
            var data =  contextoPokemon.Stats.FirstOrDefault( x => x.IdPokemon == idPokemon);
            if (data == null) {
                throw new Exception($"No se ha encontado la informacion para el Stat con el id {idPokemon} para eliminar");
            }
            contextoPokemon.Stats.Remove(data);
            contextoPokemon.SaveChanges();
        }

        public void GuardarStat(Pokemon pokemon, int idPokemonGuardado)
        {
            try
            {
                contextoPokemon.Stats.Add(ConvertirDeDominioPokemonAPersistencia(pokemon, idPokemonGuardado));
                contextoPokemon.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception($"Se ha generado un error al guardar el Stat del pokemon {pokemon.Nombre} "+e);
            }
        }

        public IEnumerable<DTOStats> LeerStats()
        {
            var data = contextoPokemon.Stats.ToList().Select(x => ConvertirADto(x));
            if (data == null)
            {
                throw new Exception($"No se encontro informacion de Stats");
            }
            return data;
        }

        public void GuardarStatsAleatorio(int idPokemon)
        {


            //    try
            //    {
            //        contextoPokemon.Stats.Add(ConvertirAModelos(statAleatorio));
            //        contextoPokemon.SaveChanges();
            //    }
            //    catch (Exception e)
            //    {
            //        throw new Exception($"Se ha producido un error al generar stat aleatorio" + e);
            //    }

        }

        //todo: re factorizar para que ingrese el Dominio de pokemon
        public void GuardarStatGenerico(DominioStat dominioStat)
        {
            contextoPokemon.Stats.Add(ConvertirDeDominioStatsAPersistencia(dominioStat));
            contextoPokemon.SaveChanges();
        }
    }
}
