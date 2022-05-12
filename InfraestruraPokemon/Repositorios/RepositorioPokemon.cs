using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfraestructuraPokemon;
using InfraestruraPokemon;
using DTOsPokemon.DTOS;
using InfraestructuraPokemon.Modelos;
using DominioPokemon;
using DTOsPokemon.DTOs;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace InfraestructuraPokemon.Repositorios
{
    public interface IRepositorioPokemon
    {
       
        IEnumerable<DTOPokemon> LeerPokemon();
        IEnumerable<DTOPokemon> BuscarPokemones(string nombrePokemon);
        DTOPokemon BuscarPokemonEspecifico(string nombrePokemon);
        int GuardarPokemon(Pokemon nuevoPokemon, DTOIngresoImagen imagenes, string rutaGuardadoImagen);
        void EliminarPokemon(int idPokemon);
        void ActualizarPokemon(DTOPokemon pokemon);
        int ObtenerCantidadPokemones();
        void ModificacionNombrePokemon(int id, string nombre);
        IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSp(DTOPaginacion paginacion);
        IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSpConFiltros(DTOPaginacionConFiltros paginacion);
        void ValidarNombreExistentePokemon(string nombrePokemon);
    }
    public class RepositorioPokemon : IRepositorioPokemon
    {
        private readonly ContextoPokemon contextoPokemon;
        public RepositorioPokemon(ContextoPokemon contextoPokemon)
        {
            this.contextoPokemon = contextoPokemon;
        }

        private DTOPokemon ConvertirADto(Pokemones dataPokemonConsultado)
       => new DTOPokemon
       {
           Id = dataPokemonConsultado.IdPokemon,
           Nombre = dataPokemonConsultado.Nombre
       };
        private Pokemones ConvertirDominiosAPersistencia(Pokemon pokemon, DTOIngresoImagen imagenes, string rutaGuardadoImagen)
      => new Pokemones
      {
          Nombre = pokemon.Nombre,
          Ataque = pokemon.Ataque,
          Defensa = pokemon.Defensa,
          EspecialAtaque = pokemon.EspecialAtaque,
          EspecialDefensa = pokemon.EspecialDefensa,
          Velocidad = pokemon.Velocidad,
          Vida = pokemon.Vida,
          NombreImagen = imagenes.Nombre,
          ArchivoImagen = imagenes.ArchivoImagen,
          RutaImagen = rutaGuardadoImagen,
          Rareza = pokemon.Rareza,
          Detalle = pokemon.Detalle
      };




        public IEnumerable<DTOPokemon> BuscarPokemones(string nombrePokemon)
        {
            var dataPokemonConsultado =
                contextoPokemon.Pokemones
                .Where(x => x.Nombre == nombrePokemon)
                .ToList().Select(x => ConvertirADto(x));

            return dataPokemonConsultado;
        }

        public int GuardarPokemon(Pokemon nuevoPokemon, DTOIngresoImagen imagenes, string rutaGuardadoImagen)
        {
            var modeloPokemon = ConvertirDominiosAPersistencia(nuevoPokemon, imagenes, rutaGuardadoImagen);
            contextoPokemon.Pokemones.Add(modeloPokemon);
            contextoPokemon.SaveChanges();
            return modeloPokemon.IdPokemon;
        }


        public void GuardarNuevoPokemon(
            Pokemon dominioPokemon,
            DominioDirectorioTipos directorioTipos,
            DominioDirectorioMovimiento directorioMovimiento)
        {

        }

        public void EliminarPokemon(int idPokemon)
        {

            var info = contextoPokemon.Pokemones.FirstOrDefault(x => x.IdPokemon == idPokemon);
            try
            {
                if (info == null)
                {
                    throw new Exception($"No se ha encontado la informacion para el Pokemon con el id {idPokemon} para eliminar");
                }
                contextoPokemon.Pokemones.Remove(info);
                contextoPokemon.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Se ha generado un error al eliminar el pokemon: {e}");
            }
        }

        public void ActualizarPokemon(DTOPokemon pokemon)
        {


            var data = contextoPokemon.Pokemones.Where(x => x.IdPokemon == pokemon.Id).SingleOrDefault();

            if (data == null)
            {
                throw new Exception($"No se encontro Tipo con Id {pokemon.Id} para actualizar");
            }
            data.Nombre = pokemon.Nombre;
            contextoPokemon.SaveChanges();
        }

        public IEnumerable<DTOPokemon> LeerPokemon()
        {

            var data = contextoPokemon.Pokemones.AsNoTracking().ToList().Select(x => ConvertirADto(x));
            if (data == null)
            {
                throw new Exception($"No se encontro informacion de pokemones");
            }
            return data;
        }

        public DTOPokemon BuscarPokemonEspecifico(string nombrePokemon)
        {

            var data = contextoPokemon.Pokemones.Where(x => x.Nombre == nombrePokemon).Select(x => ConvertirADto(x)).FirstOrDefault();
            if (data == null)
            {
                throw new Exception($"No se encontro informacion del pokemon: {nombrePokemon}");
            }
            return data;
        }

     



      
        public IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSp(DTOPaginacion paginacion)
        {
            int ubicacionPagina = paginacion.Indice * paginacion.CantidadRegistros;
            string conn = contextoPokemon.Database.Connection.ConnectionString;
            SqlConnection connection = new SqlConnection(conn);

            var values = new { UbicacionPagina = ubicacionPagina, CantidadRegistros = paginacion.CantidadRegistros };
            var procedure = "[GetSeccionPokemones]";

            var multi = connection.QueryMultiple(procedure, values, commandType: CommandType.StoredProcedure);

            var pokemon = multi.Read<DTOPokemon>().ToList();
            var movimiento = multi.Read<DTOMovimiento>().ToList();
            var tipo = multi.Read<DTOTipo>().ToList();


            return  from x in pokemon
                    select new DTODetallePokemon
                    {
                        Id = x.Id,
                        Nombre = x.Nombre,
                        Ataque = x.Ataque,
                        Defensa = x.Defensa,
                        EspecialAtaque = x.EspecialAtaque,
                        EspecialDefensa = x.EspecialDefensa,
                        Velocidad = x.Velocidad,
                        Vida = x.Vida,
                        NombreImagen = x.NombreImagen,
                        RutaImagen = x.RutaImagen,
                        Rareza = x.Rareza,
                        Detalle = x.Detalle,
                        Movimientos = movimiento.Where(m=>m.IdTemporalPokemon == x.Id)
                        .Select(sm=>new DTOMovimiento 
                            {
                            IdTemporalPokemon = sm.IdTemporalPokemon,
                            IdMovimiento = sm.IdMovimiento,
                            NombreMovimiento=sm.NombreMovimiento,
                            Valor=sm.Valor,
                        }).ToList(),
                        Tipos = tipo.Where( t => t.IdTemporalPokemon == x.Id)
                        .Select(st => new DTOTipo 
                            {
                            IdTemporalPokemon = st.IdTemporalPokemon, 
                            NombreTipo = st.NombreTipo,
                            IdTipo = st.IdTipo                        
                        }).ToList(),
                    };
             
        }

        public IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSpConFiltros(DTOPaginacionConFiltros paginacion)
        {
            int ubicacionPagina = paginacion.Indice * paginacion.CantidadRegistros;
            string conn = contextoPokemon.Database.Connection.ConnectionString;
            SqlConnection connection = new SqlConnection(conn);

            var values = new {
                UbicacionPagina = ubicacionPagina,
                CantidadRegistros = paginacion.CantidadRegistros,
                Nombre = paginacion.Nombre,
                VidaMaxima = paginacion.VidaMaxima,
                VidaMinima = paginacion.VidaMinima
            };
            var procedure = "[GetSeccionPokemonesConFiltros]";

            var multi = connection.QueryMultiple(procedure, values, commandType: CommandType.StoredProcedure);

            var pokemon = multi.Read<DTOPokemon>().ToList();
            var movimiento = multi.Read<DTOMovimiento>().ToList();
            var tipo = multi.Read<DTOTipo>().ToList();


            return from x in pokemon 
                   select new DTODetallePokemon
                   {
                       Id = x.Id,
                       Nombre = x.Nombre,
                       Ataque = x.Ataque,
                       Defensa = x.Defensa,
                       EspecialAtaque = x.EspecialAtaque,
                       EspecialDefensa = x.EspecialDefensa,
                       Velocidad = x.Velocidad,
                       Vida = x.Vida,
                       NombreImagen = x.NombreImagen,
                       RutaImagen = x.RutaImagen,
                       Rareza = x.Rareza,
                       Detalle = x.Detalle,
                       Movimientos = movimiento.Where(m => m.IdTemporalPokemon == x.Id)
                       .Select(sm => new DTOMovimiento
                       {
                           IdTemporalPokemon = sm.IdTemporalPokemon,
                           IdMovimiento = sm.IdMovimiento,
                           NombreMovimiento = sm.NombreMovimiento,
                           Valor = sm.Valor,
                       }).ToList(),
                       Tipos = tipo.Where(t => t.IdTemporalPokemon == x.Id)
                       .Select(st => new DTOTipo
                       {
                           IdTemporalPokemon = st.IdTemporalPokemon,
                           NombreTipo = st.NombreTipo,
                           IdTipo = st.IdTipo
                       }).ToList(),
                   };

        }
        public int ObtenerCantidadPokemones()
        {
            return contextoPokemon.Pokemones.Count();
        }

        public void ModificacionNombrePokemon(int id, string nombre)
        {
            var pokemon = (from x in contextoPokemon.Pokemones
                           where x.IdPokemon == id
                           select x).FirstOrDefault();
            if (pokemon == null)
            {
                throw new Exception($"No se encontró información del pokémon que se desea modificar ");
            }
            pokemon.Nombre = nombre;
            contextoPokemon.SaveChanges();
        }

        public void ValidarNombreExistentePokemon(string nombrePokemon)
        {
            if (contextoPokemon.Pokemones.Any(pokemon => nombrePokemon.Equals(pokemon.Nombre)))
            {
                throw new Exception($"El nombre del Pokémon '{nombrePokemon}' ya se encuentra registrado");
            }
             
        }

        
    }
}