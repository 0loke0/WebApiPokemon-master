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
    public interface IServicioPokemon
    {
        
        IEnumerable<DTODetallePokemon> ListarPokemonesSP(DTOPaginacion paginacion);
        IEnumerable<DTODetallePokemon> ListarPokemonesConFiltros(DTOFormularioConsulta paginacion);
        void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon);
        void ModificarPokemon(DTOModificacionAPokemon ModificacionAPokemon);
        void EliminarPokemon(int idPokemon);
        int ObtenerCantidadPokemones();
        int ObtenerCantidadPokemonesFiltrado(DTOFiltros Filtros);

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

        private void ValidarExistenciaNombrePokemon(string nombre)
        {
            int x = repositorioPokemon.BuscarCantidadMismoNombre(nombre);
            if (repositorioPokemon.BuscarCantidadMismoNombre(nombre) > 2)
            {
                throw new Exception($"Ya existe mas de un pokemon con este nombre ");
            }
            if (repositorioPokemon.BuscarCantidadMismoNombre(nombre) == 1)
            {
                throw new Exception($"Ya existe un pokemon con este nombre ");
            }
                    
        }

       

        public IEnumerable<DTODetallePokemon> ListarPokemonesSP(DTOPaginacion paginacion)
        {
            return repositorioPokemon.RecogerPokemonDesdeSp(paginacion);
        }

         public IEnumerable<DTODetallePokemon> ListarPokemonesConFiltros(DTOFormularioConsulta paginacion)
        {
            return repositorioPokemon.RecogerPokemonDesdeSpConFiltros(paginacion);
        }

        public void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon)
        {
            ValidarExistenciaNombrePokemon(nuevoPokemon.NombrePokemon);
            //Pokemon hace referencia al domonio
            Pokemon pokemon = new Pokemon(nuevoPokemon.NombrePokemon, nuevoPokemon.Detalle);
            DominioDirectorioTipos directorioTipos = new DominioDirectorioTipos(nuevoPokemon.IdsTipo);
            DominioDirectorioMovimiento directorioMovimiento = new DominioDirectorioMovimiento(nuevoPokemon.IdsMovimiento);
            DominioImagenes imagenes = new DominioImagenes(nuevoPokemon.Imagen.Nombre, nuevoPokemon.Imagen.ArchivoImagen);

            //convercion y guardado de imagen
            byte[] imagenConvertida = UtilidadesImagenes.ConvertirDeBase64Aimagen(nuevoPokemon.Imagen.ArchivoImagen);
            string rutaGuardadoImagen = GuardarImagenEnLocal(imagenConvertida, nuevoPokemon.Imagen.Nombre);

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

        public int ObtenerCantidadPokemonesFiltrado(DTOFiltros Filtros)
        {
            return repositorioPokemon.ObtenerCantidadPokemonesFiltrados(Filtros);
        }




        public void ModificarPokemon(DTOModificacionAPokemon ModificacionAPokemon)
        {

            DominioDirectorioTipos directorioTipos = new DominioDirectorioTipos(ModificacionAPokemon.IdsTipo);
            DominioDirectorioMovimiento directorioMovimiento = new DominioDirectorioMovimiento(ModificacionAPokemon.IdsMovimiento);

            if (!string.IsNullOrEmpty(ModificacionAPokemon.NombrePokemon))
            {
                ValidarExistenciaNombrePokemon(ModificacionAPokemon.NombrePokemon);
                Pokemon pokemon = new Pokemon(ModificacionAPokemon.NombrePokemon, ModificacionAPokemon.Detalle);
                repositorioPokemon.ModificacionNombrePokemon(ModificacionAPokemon.Id, pokemon.Nombre);
            }          

            repositorioDirectorioTipos.EliminarRelacionTipos(ModificacionAPokemon.Id);
            repositorioDirectorioTipos.GuardarRelacion(directorioTipos.IdsTipo, ModificacionAPokemon.Id);

            repositorioDirectorioMovimientos.EliminarRelacionMovimientos(ModificacionAPokemon.Id);
            repositorioDirectorioMovimientos.GuardarRelacion(directorioMovimiento.IdsMovimiento,ModificacionAPokemon.Id);

        }


    }
}
