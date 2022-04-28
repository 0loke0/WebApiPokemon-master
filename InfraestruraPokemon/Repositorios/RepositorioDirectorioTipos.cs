using DTOsPokemon.DTOS;
using InfraestructuraPokemon.Modelos;
using InfraestructuraPokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Repositorios
{
    public interface IRepositorioDirectorioTipos {
        void GuardarRelacion(List<int> directorioTipos, int idPokemonGuardado);
        IEnumerable<DTORelacionPokemonTipo> BuscarRelacionPokemonTipo(int idpokemon);      
        void EliminarRelacionTipos(int id);
    }
    public class RepositorioDirectorioTipos: IRepositorioDirectorioTipos
    {
        private readonly ContextoPokemon contextoPokemon;
        public RepositorioDirectorioTipos(ContextoPokemon contextoPokemon)
        {
            this.contextoPokemon = contextoPokemon;
        }
        

        private DirectorioTipos ConvertirDominioAPersistencia(int idTipo,int idPokemonGuardado) {
            return new DirectorioTipos
            {
                IdTipo = idTipo,
                IdPokemon = idPokemonGuardado
            };
        }
        public void  EliminarRelacionTipos(int id)
        {
            var info = contextoPokemon.DirectorioTipos.Where(x => x.IdPokemon == id).Select(t=>t).ToList();
            try
            {
                if (info == null)
                {
                    throw new Exception($"No se ha encontrado ninguna relacion de los tipos asociados con el pokemon");
                }
                foreach (var item in info)
                {
                    contextoPokemon.DirectorioTipos.Remove(item);
                }
                
                contextoPokemon.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Se ha generado un error al eliminar la relacion de los tipos asociados con el pokemon : {e}");
            }
        }
        


        public void GuardarRelacion(List<int> directorioTipos, int idPokemonGuardado)
        {
            foreach (var tipo in directorioTipos)
            {
                contextoPokemon.DirectorioTipos.Add(ConvertirDominioAPersistencia(tipo, idPokemonGuardado));
                contextoPokemon.SaveChanges();
            }
            
        }
        public IEnumerable<DTORelacionPokemonTipo> BuscarRelacionPokemonTipo(int idpokemon) {

            IEnumerable<DTORelacionPokemonTipo> data = from pokemon in contextoPokemon.Pokemones
                                                       join directorioT in contextoPokemon.DirectorioTipos
                                                       on pokemon.IdPokemon equals directorioT.IdPokemon
                                                       join tipos in contextoPokemon.Tipos
                                                       on directorioT.IdTipo equals tipos.IdTipo
                                                       where pokemon.IdPokemon == idpokemon
                                                       select new DTORelacionPokemonTipo
                                                       {
                                                           Id = pokemon.IdPokemon,
                                                           Nombre = pokemon.Nombre,
                                                           IdTipo = tipos.IdTipo,
                                                            NombreTipo = tipos.NombreTipo
                                                    };
                    
            return data;
        }

      
    }
}
