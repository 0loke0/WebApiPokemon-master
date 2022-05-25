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
        IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSp(DTOPaginacion paginacion);
        IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSpConFiltros(DTOFormularioConsulta paginacion);
        int BuscarCantidadMismoNombre(string nombrePokemon,int?  id);
        int ObtenerCantidadPokemonesFiltrados(DTOFiltros Filtros );
        void ModificarPokemon(int id, Pokemon dominioPokemon);


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

        public IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSpConFiltros(DTOFormularioConsulta paginacion)
        {
            int ubicacionPagina = paginacion.Paginacion.Indice * paginacion.Paginacion.CantidadRegistros;
            string conn = contextoPokemon.Database.Connection.ConnectionString;
            SqlConnection connection = new SqlConnection(conn);

            var values = new {
                UbicacionPagina = ubicacionPagina,
                CantidadRegistros = paginacion.Paginacion.CantidadRegistros,
                Identificador = paginacion.Filtros.Identificador,
                Nombre = paginacion.Filtros.Nombre,
                AtaqueMinimo = paginacion.Filtros.AtaqueMinimo,    
                AtaqueMaximo = paginacion.Filtros.AtaqueMaximo,    
                AtaqueEspecialMinimo = paginacion.Filtros.AtaqueEspecialMinimo,    
                AtaqueEspecialMaximo = paginacion.Filtros.AtaqueEspecialMaximo,     
                VidaMinima = paginacion.Filtros.VidaMinima,    
                VidaMaxima = paginacion.Filtros.VidaMaxima,    
                DefensaMinima =paginacion.Filtros.DefensaMinima,    
                DefensaMaxima = paginacion.Filtros.DefensaMaxima,    
                DefensaEspecialMinima =paginacion.Filtros.AtaqueEspecialMinimo,   
                DefensaEspecialMaxima =paginacion.Filtros.AtaqueEspecialMaximo,    
                VelocidadMinima = paginacion.Filtros.VelocidadMinima,    
                VelocidadMaxima = paginacion.Filtros.VelocidadMaxima,             
               
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
        public int ObtenerCantidadPokemones( )
        {
            
            return contextoPokemon.Pokemones.Count();
        }

        public int ObtenerCantidadPokemonesFiltrados(DTOFiltros Filtros)
        {
            string conn = contextoPokemon.Database.Connection.ConnectionString;
            SqlConnection connection = new SqlConnection(conn);

            var values = new
            {
                Identificador = Filtros.Identificador,
                Nombre = Filtros.Nombre,
                AtaqueMinimo = Filtros.AtaqueMinimo,
                AtaqueMaximo = Filtros.AtaqueMaximo,
                AtaqueEspecialMinimo = Filtros.AtaqueEspecialMinimo,
                AtaqueEspecialMaximo = Filtros.AtaqueEspecialMaximo,
                VidaMinima = Filtros.VidaMinima,
                VidaMaxima = Filtros.VidaMaxima,
                DefensaMinima = Filtros.DefensaMinima,
                DefensaMaxima = Filtros.DefensaMaxima,
                DefensaEspecialMinima = Filtros.AtaqueEspecialMinimo,
                DefensaEspecialMaxima = Filtros.AtaqueEspecialMaximo,
                VelocidadMinima = Filtros.VelocidadMinima,
                VelocidadMaxima = Filtros.VelocidadMaxima,
            };
            var procedure = "[GetCantidadPokemonesFiltrados]";

            //pendiente dejarlo como singular no multiple
            var multi = connection.QueryMultiple(procedure, values, commandType: CommandType.StoredProcedure);

            var pokemon = multi.Read<int>().ToList();
            
            return pokemon.FirstOrDefault();

        }


      
        public int BuscarCantidadMismoNombre(string nombrePokemon,int? id)
        {
            nombrePokemon = nombrePokemon.Trim();
        
            var query = contextoPokemon.Pokemones.Where(pokemon => pokemon.Nombre.Equals(nombrePokemon));
            if (id.HasValue) 
            {
                int idPokemon = id.GetValueOrDefault();
                query = query.Where(pokemon => !pokemon.IdPokemon.Equals(idPokemon)); 
            }

            return query.Count();

        }
        public void ModificarPokemon(int id, Pokemon dominioPokemon)
        {
            var pokemon = contextoPokemon.Pokemones.Where(poke => poke.IdPokemon == id).FirstOrDefault();

            if (pokemon == null)
            {
                throw new Exception($"No se encontró información del pokémon al cual se desea modificar");
            }
            pokemon.Nombre = dominioPokemon.Nombre;
            pokemon.Detalle = dominioPokemon.Detalle;
            contextoPokemon.SaveChanges();
        }


    }
}