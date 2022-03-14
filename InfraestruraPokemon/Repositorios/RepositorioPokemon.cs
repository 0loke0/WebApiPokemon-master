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

namespace InfraestructuraPokemon.Repositorios
{
    public interface IRepositorioPokemon
    {
        IEnumerable<DTODetallePokemon> RecogerPokemon();
        IEnumerable<DTOPokemon> LeerPokemon();
        IEnumerable<DTOPokemon> BuscarPokemones(string nombrePokemon);
        DTOPokemon BuscarPokemonEspecifico(string nombrePokemon);
        int GuardarPokemon(Pokemon nuevoPokemon);
        void EliminarPokemon(int idPokemon);
        void ActualizarPokemon(DTOPokemon pokemon);

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
        private Pokemones ConvertirDominioAPersistencia(Pokemon pokemon)
      => new Pokemones
      {
          Nombre = pokemon.Nombre
      };




        public IEnumerable<DTOPokemon> BuscarPokemones(string nombrePokemon)
        {
            var dataPokemonConsultado =
                contextoPokemon.Pokemones
                .Where(x => x.Nombre == nombrePokemon)
                .ToList().Select(x => ConvertirADto(x));

            return dataPokemonConsultado;
        }

        public int GuardarPokemon(Pokemon nuevoPokemon)
        {
            var modeloPokemon = ConvertirDominioAPersistencia(nuevoPokemon);
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
        public IEnumerable<DTODetallePokemon> RecogerPokemon()
        {
            List<DTODetallePokemon> listaPokemones = new List<DTODetallePokemon>();
            

            var ids = (from poke in contextoPokemon.Pokemones
                      select poke.IdPokemon).ToList();

            
            foreach (var id in ids)
            {
                var Pokemon = (from x in contextoPokemon.Pokemones
                              where id == x.IdPokemon
                              select new DTOPokemon { Id = x.IdPokemon, Nombre = x.Nombre }).FirstOrDefault();

                var movimiento = (from DM in contextoPokemon.DirectorioMovimientos
                                 join M in contextoPokemon.Movimientos
                                 on DM.IdMovimiento equals M.IdMovimiento
                                 where DM.IdPokemon == id
                                 select new DTOMovimientoBD { NombreMovimiento = M.NombreMovimiento, Valor = M.Valor }).ToList();

                var tipo =( from DT in contextoPokemon.DirectorioTipos
                           join T in contextoPokemon.Tipos
                           on DT.IdTipo equals T.IdTipo
                           where DT.IdPokemon == id
                           select new DTOTiposBD { IdTipo=T.IdTipo,NombreTipo = T.NombreTipo}).ToList();

                var stast = (from P in contextoPokemon.Pokemones
                            join S in contextoPokemon.Stats
                                 on P.IdPokemon equals S.IdPokemon
                            where P.IdPokemon == id
                            select new DTOStatsdBD
                            {
                                Ataque = S.Ataque,
                                Defensa = S.Defensa,
                                EspecialAtaque = S.EspecialAtaque,
                                EspecialDefensa = S.EspecialDefensa,
                                Velocidad = S.Velocidad,
                                Vida = S.Vida,
                            }).FirstOrDefault();

                var imagen = (from P in contextoPokemon.Pokemones
                             join I in contextoPokemon.Imagenes
                             on P.IdPokemon equals I.IdPokemon
                             where P.IdPokemon == id
                             select new DTOImagenBD
                             {
                                 ArchivoImagen = I.ArchivoImagen,
                                 Nombre = I.Nombre,
                                 RutaImagen = I.RutaImagen
                             }).FirstOrDefault();


                listaPokemones.Add(new DTODetallePokemon {
                    Pokemon = Pokemon,
                    Imagen = imagen, 
                    Movimientos = movimiento, 
                    Stats = stast, 
                    Tipos = tipo });

            }
           
            return listaPokemones;


        }


    }
}