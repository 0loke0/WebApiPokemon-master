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

namespace ServiciosPokemon
{
    public interface IServicioPokemon {
        IEnumerable<DTODetallePokemon> ListarPokemones();
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

        public IEnumerable<DTODetallePokemon> ListarPokemones()
        {
            return repositorioPokemon.RecogerPokemon();
        }
        public void GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon)
        {
            //Pokemon hace referencia al domonio
            Pokemon pokemon = new Pokemon(nuevoPokemon.NombrePokemon);//todo:Pendiente verificar si existe algún nombre en bd igual que el que se está agregando
            DominioDirectorioTipos directorioTipos = new DominioDirectorioTipos(nuevoPokemon.IdsTipo);
            DominioDirectorioMovimiento directorioMovimiento = new DominioDirectorioMovimiento(nuevoPokemon.IdsMovimiento);
            DominioImagenes imagenes = new DominioImagenes(nuevoPokemon.Imagen.Nombre,nuevoPokemon.Imagen.ArchivoImagen);

            DTOImagen img= repositorioImagenes.ConvertirDominioADtoGuardandoImagenEnLocal(imagenes);
            var idPokemonGuardado = repositorioPokemon.GuardarPokemon(pokemon, img);

            repositorioDirectorioTipos.GuardarRelacion(directorioTipos.IdsTipo, idPokemonGuardado);
            repositorioDirectorioMovimientos.GuardarRelacion(directorioMovimiento.IdsMovimiento, idPokemonGuardado);
          
        }

           

       
    }
}
