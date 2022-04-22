using DTOsPokemon.DTOS;
using DTOsPokemon.DTOs;
using InfraestructuraPokemon.Repositorios;
using System;
using System.Collections.Generic;
using DominioPokemon;
using InfraestructuraPokemon.Modelos;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Utilidades.Utilidades;

namespace ServiciosPokemon
{
    public interface IServicioPokemon {
        IEnumerable<DTODetallePokemon> ListarPokemones(DTOPaginacion paginacion);
        IEnumerable<DTODetallePokemon> ListarPokemonesSP(DTOPaginacion paginacion);
        void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon);
        void ModificarPokemon(DTOModificacionAPokemon ModificacionAPokemon);
       
        void EliminarPokemon(int idPokemon);

        int ObtenerCantidadPokemones();
        
    }
    public class ServicioPokemon : IServicioPokemon
    {
        public IRepositorioPokemon repositorioPokemon;
        public IRepositorioDirectorioTipos repositorioDirectorioTipos;
        public IRepositorioDirectorioMovimientos repositorioDirectorioMovimientos;
        public IRepositorioImagenes repositorioImagenes;

        public ServicioPokemon(
            IRepositorioPokemon repositorioPokemon,
            IRepositorioDirectorioTipos repositorioDirectorioTipos,
            IRepositorioDirectorioMovimientos repositorioDirectorioMovimientos,
            IRepositorioImagenes repositorioImagenes
            )
        {
            this.repositorioPokemon = repositorioPokemon;
            this.repositorioDirectorioTipos = repositorioDirectorioTipos;
            this.repositorioDirectorioMovimientos = repositorioDirectorioMovimientos;
            this.repositorioImagenes = repositorioImagenes;
        }

        private string GuardarImagenEnLocal(byte[] imagenBytes, string nombre)
        {
            string directorioDeGuardado = @"C:\Users\danfe\Documents\GitHub\consumoapi\src\ImagenesPokemon";
            string ruta = directorioDeGuardado + @"\" + nombre;

            if (!Directory.Exists(directorioDeGuardado))
            {
                Directory.CreateDirectory(directorioDeGuardado);
            }
            using (var imageFile = new FileStream(ruta, FileMode.Create))
            {
                imageFile.Write(imagenBytes, 0, imagenBytes.Length);
                imageFile.Flush();
            }
            return ruta;
        }

        public IEnumerable<DTODetallePokemon> ListarPokemones(DTOPaginacion paginacion)
        {
            return repositorioPokemon.RecogerPokemon(paginacion);

        }

        public IEnumerable<DTODetallePokemon> ListarPokemonesSP(DTOPaginacion paginacion)
        {
            return repositorioPokemon.RecogerPokemonDesdeSp( paginacion);

        }


        public void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon)
        {
            
            //Pokemon hace referencia al domonio
            Pokemon pokemon = new Pokemon(nuevoPokemon.NombrePokemon,nuevoPokemon.Detalle);//todo:Pendiente verificar si existe algún nombre en bd igual que el que se está agregando
            DominioDirectorioTipos directorioTipos = new DominioDirectorioTipos(nuevoPokemon.IdsTipo);
            DominioDirectorioMovimiento directorioMovimiento = new DominioDirectorioMovimiento(nuevoPokemon.IdsMovimiento);
            DominioImagenes imagenes = new DominioImagenes(nuevoPokemon.Imagen.Nombre,nuevoPokemon.Imagen.ArchivoImagen);

            //convercion y guardado de imagen
            byte[] imagenConvertida= UtilidadesImagenes.ConvertirDeBase64Aimagen(nuevoPokemon.Imagen.ArchivoImagen);
            string rutaGuardadoImagen = GuardarImagenEnLocal(imagenConvertida,nuevoPokemon.Imagen.Nombre);

            //DTOImagen img= repositorioImagenes.ConvertirDominioADtoGuardandoImagenEnLocal(imagenes);
            var idPokemonGuardado = repositorioPokemon.GuardarPokemon(pokemon, nuevoPokemon.Imagen, rutaGuardadoImagen);

            repositorioDirectorioTipos.GuardarRelacion(directorioTipos.IdsTipo, idPokemonGuardado);
            repositorioDirectorioMovimientos.GuardarRelacion(directorioMovimiento.IdsMovimiento, idPokemonGuardado);
          
        }

        public void EliminarPokemon(int idPokemon)
        {
            repositorioPokemon.EliminarPokemon(idPokemon);
        }

        public int ObtenerCantidadPokemones()
        {
            return repositorioPokemon.ObtenerCantidadPokemones();
        }



        public void ModificarPokemon(DTOModificacionAPokemon ModificacionAPokemon)
        {
            Pokemon pokemon = new Pokemon(ModificacionAPokemon.NombrePokemon, ModificacionAPokemon.Detalle);
            DominioDirectorioTipos directorioTipos = new DominioDirectorioTipos(ModificacionAPokemon.IdsTipo);
            DominioDirectorioMovimiento directorioMovimiento = new DominioDirectorioMovimiento(ModificacionAPokemon.IdsMovimiento);
            repositorioPokemon.ModificacionNombrePokemon(ModificacionAPokemon.Id, pokemon.Nombre);
            repositorioDirectorioTipos.ModificacionDirectorioTipos(ModificacionAPokemon.Id,ModificacionAPokemon.IdsTipo);
            repositorioDirectorioMovimientos.ModificacionDirectorioMovimientos(ModificacionAPokemon.Id,ModificacionAPokemon.IdsMovimiento);


        }


        
    }
}
