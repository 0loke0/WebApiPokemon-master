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
        void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon);
    }
    public class ServicioPokemon : IServicioPokemon
    {
        public IRepositorioPokemon repositorioPokemon;
        public IRepositorioStats repositorioStats;
        public IRepositorioDirectorioTipos repositorioDirectorioTipos;
        public IRepositorioDirectorioMovimientos repositorioDirectorioMovimientos;
        public IRepositorioImagenes repositorioImagenes;

        public ServicioPokemon(
            IRepositorioPokemon repositorioPokemon,
            IRepositorioStats repositorioStats,
            IRepositorioDirectorioTipos repositorioDirectorioTipos,
            IRepositorioDirectorioMovimientos repositorioDirectorioMovimientos,
            IRepositorioImagenes repositorioImagenes
            )
        {
            this.repositorioPokemon = repositorioPokemon;
            this.repositorioStats = repositorioStats;
            this.repositorioDirectorioTipos = repositorioDirectorioTipos;
            this.repositorioDirectorioMovimientos = repositorioDirectorioMovimientos;
            this.repositorioImagenes = repositorioImagenes;
        }

        private string GuardarImagenEnLocal(byte[] imagenBytes, string nombre)
        {
            string directorioDeGuardado = @"C:\nueva";
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

       


        public void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon)
        {
            UtilidadesImagenes utilidades = new UtilidadesImagenes();
            //Pokemon hace referencia al domonio
            Pokemon pokemon = new Pokemon(nuevoPokemon.NombrePokemon);//todo:Pendiente verificar si existe algún nombre en bd igual que el que se está agregando
            DominioDirectorioTipos directorioTipos = new DominioDirectorioTipos(nuevoPokemon.IdsTipo);
            DominioDirectorioMovimiento directorioMovimiento = new DominioDirectorioMovimiento(nuevoPokemon.IdsMovimiento);
            DominioImagenes imagenes = new DominioImagenes(nuevoPokemon.Imagen.Nombre,nuevoPokemon.Imagen.ArchivoImagen);

            //convercion y guardado de imagen
            byte[] imagenConvertida= utilidades.ConvertirDeBase64Aimagen(nuevoPokemon.Imagen.ArchivoImagen);
            string rutaGuardadoImagen = GuardarImagenEnLocal(imagenConvertida,nuevoPokemon.Imagen.Nombre);

            //DTOImagen img= repositorioImagenes.ConvertirDominioADtoGuardandoImagenEnLocal(imagenes);
            var idPokemonGuardado = repositorioPokemon.GuardarPokemon(pokemon, nuevoPokemon.Imagen, rutaGuardadoImagen);

            repositorioDirectorioTipos.GuardarRelacion(directorioTipos.IdsTipo, idPokemonGuardado);
            repositorioDirectorioMovimientos.GuardarRelacion(directorioMovimiento.IdsMovimiento, idPokemonGuardado);
          
        }

           

       
    }
}
