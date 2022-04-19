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
        IEnumerable<DTODetallePokemon> RecogerPokemon(DTOPaginacion paginacion);
        IEnumerable<DTOPokemon> LeerPokemon();
        IEnumerable<DTOPokemon> BuscarPokemones(string nombrePokemon);
        DTOPokemon BuscarPokemonEspecifico(string nombrePokemon);
        int GuardarPokemon(Pokemon nuevoPokemon, DTOIngresoImagen imagenes, string rutaGuardadoImagen);
        void EliminarPokemon(int idPokemon);
        void ActualizarPokemon(DTOPokemon pokemon);
        int ObtenerCantidadPokemones();
        void ModificacionNombrePokemon (int id, string nombre);
        IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSp();




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
          Velocidad =pokemon.Velocidad,
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
            var modeloPokemon = ConvertirDominiosAPersistencia(nuevoPokemon, imagenes,  rutaGuardadoImagen);
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

        //todo: dejar mas clean la funcion recogerPokemon (muchas acciones dentro de una misma funcion)
        public IEnumerable<DTODetallePokemon> RecogerPokemon(DTOPaginacion paginacion)
        {
            int ubicacionPagina = paginacion.Indice * paginacion.CantidadRegistros;

            return (from x in contextoPokemon.Pokemones

                    select new DTODetallePokemon
                    {
                        Id=x.IdPokemon,
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
                        Detalle=x.Detalle,
                        Movimientos = contextoPokemon.DirectorioMovimientos
                                .Join(
                                 contextoPokemon.Movimientos,
                                 directorioMovi => directorioMovi.IdMovimiento,
                                movi => movi.IdMovimiento,
                                (directorioMovi, movi) =>

                                new DTOMovimiento
                                {
                                    IdTemporalPokemon = directorioMovi.IdPokemon,
                                    IdMovimiento = movi.IdMovimiento,
                                    NombreMovimiento = movi.NombreMovimiento,
                                    Valor = movi.Valor
                                }).Where(dm => dm.IdTemporalPokemon == x.IdPokemon)
                                .ToList(),

                        Tipos = contextoPokemon.DirectorioTipos
                                .Join(
                                contextoPokemon.Tipos,
                                directorioTip => directorioTip.IdTipo,
                                tip => tip.IdTipo,

                                (directorioTip, tip) =>

                                new DTOTipo
                                {
                                    IdTemporalPokemon = directorioTip.IdPokemon,
                                    IdTipo = tip.IdTipo,
                                    NombreTipo = tip.NombreTipo
                                }
                                ).Where(dt => dt.IdTemporalPokemon == x.IdPokemon)
                                .ToList()

                       
                        }).OrderBy(ft=>ft.Id).Skip(ubicacionPagina).Take(paginacion.CantidadRegistros).ToList();



        }


        public IEnumerable<DTODetallePokemon> RecogerPokemonDesdeSp()
        {

            ContextoPokemon contextoPokemons = new ContextoPokemon();
            var inicio = new SqlParameter("@InicioBusqueda", 3107);
            var final = new SqlParameter("@UltimoBusqueda", 3109);
            //var test = contextoPokemons.Database.SqlQuery<T>("dbo.GetSeccionPokemones @InicioBusqueda, @UltimoBusqueda", inicio, final).ToList();

            //using (var multi = conecction.QueryMultiple("dbo.GetSeccionPokemones @InicioBusqueda, @UltimoBusqueda", inicio, final))
            //{
            //    int countA = multi.Read<int>().Single();
            //    int countB = multi.Read<int>().Single();
            //}
            string conn = "Data Source=LOKE;Initial Catalog=DbPokemones;User ID=UsuarioParaApi;Password=Contrasena12345;Trusted_Connection=False;MultipleActiveResultSets=true;";

            SqlConnection connection = new SqlConnection(conn);

            var procedure = "[GetSeccionPokemones]";
            var values = new { InicioBusqueda = 3107, UltimoBusqueda = 3109 };
            var results = connection.Query(procedure, values, commandType: CommandType.StoredProcedure).ToList();
            var results2 = connection.Query(procedure, values, commandType: CommandType.StoredProcedure).ToList();
            results.ForEach(r => Console.WriteLine($"{r.OrderID} {r.Subtotal}"));
            return null;
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
            if (pokemon == null) {
                throw new Exception($"No se encontró información del pokémon que se desea modificar ");
            }
            pokemon.Nombre = nombre;
            contextoPokemon.SaveChanges();
        }
    }
}