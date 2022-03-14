using DTOsPokemon.DTOS;
using InfraestructuraPokemon;
using InfraestructuraPokemon.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Repositorios
{
    public interface IRepositorioTipo
    {
        IEnumerable<DTORelacionPokemonTipo> RelacionPokemonesTipos();
        void EliminarTipo(int idTipo);
        void AgregarTipo(string nombreTipo);
        void ActualizarTipo(DTOTipo ingresoTipo);
        IEnumerable<DTOTipo> LeerTipos();


    }

    public class RepositorioTipos : IRepositorioTipo
    {
       
        ContextoPokemon contextoPokemon;
        public RepositorioTipos(ContextoPokemon contextoPokemon)
        {
            this.contextoPokemon = contextoPokemon;
        }
        private Tipos DtoPersistencia(DTOTipo ingresoTipo)
        {
            return new Tipos()
            {
                IdTipo = ingresoTipo.IdTipo,
                NombreTipo = ingresoTipo.NombreTipo
            };
        }
        private DTOTipo ConvertirADto(Tipos Tipo)
        {
            return new DTOTipo()
            {
                IdTipo = Tipo.IdTipo,
                NombreTipo = Tipo.NombreTipo
            };
        }


        public IEnumerable<DTORelacionPokemonTipo> RelacionPokemonesTipos()
        {

            var info = from pokemons in contextoPokemon.Pokemones
                       join DirTipos in contextoPokemon.DirectorioTipos
                       on pokemons.IdPokemon equals DirTipos.IdPokemon
                       join tipo in contextoPokemon.Tipos
                       on DirTipos.IdTipo equals tipo.IdTipo
                       select new DTORelacionPokemonTipo
                       {
                           Id = pokemons.IdPokemon,
                           Nombre = pokemons.Nombre,
                           NombreTipo = tipo.NombreTipo
                       };

            return info.ToList();
        }

        public void EliminarTipo(int idTipo)
        {
            var data = contextoPokemon.Tipos.FirstOrDefault(x => x.IdTipo == idTipo);

            if (data == null)
            {
                throw new Exception($"No se encontro Tipo con Id {idTipo} para eliminar");
            }
            contextoPokemon.Tipos.Remove(data);
            contextoPokemon.SaveChanges();

        }

        public void AgregarTipo(string nombreTipo)
        {
            var info = new DTOTipo();
            info.NombreTipo = nombreTipo;
            contextoPokemon.Tipos.Add(DtoPersistencia(info));
            contextoPokemon.SaveChanges();
        }

        public void ActualizarTipo(DTOTipo tipo)
        {
            var data = contextoPokemon.Tipos.Where(x => x.IdTipo == tipo.IdTipo).SingleOrDefault();
            if (data == null)
            {
                throw new Exception($"No se encontro Tipo con Id {tipo.IdTipo} para actializar");
            }
            data.NombreTipo = tipo.NombreTipo;
            contextoPokemon.SaveChanges();
        }
               
        public IEnumerable<DTOTipo> LeerTipos()
        {
            return contextoPokemon.Tipos.ToList().Select(x => ConvertirADto(x));
        }

        public DTOTipo BuscarTipo(int idTipo) {           
            return contextoPokemon.Tipos.Where(x => x.IdTipo == idTipo).Select(x=> ConvertirADto(x)).FirstOrDefault();
        }

        public int CantidadTipo() {
            return contextoPokemon.Tipos.Count();
        }

       
    }
}
