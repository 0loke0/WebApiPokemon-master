
using System.Web.Http;
using ServiciosPokemon;
using DTOsPokemon.DTOS;
using System.Collections.Generic;

namespace ApiPokemon.Controllers
{
    public class PokemonesController : ApiController
    {
        public IServicioPokemon servicioPokemon;
        public PokemonesController(IServicioPokemon servicioPokemon)
        {
            this.servicioPokemon = servicioPokemon;
        }

        

        [HttpPost]
        public IEnumerable<DTODetallePokemon> ObtenerPokemonesConFiltros(DTOPaginacionConFiltros paginacion)
        {
            return servicioPokemon.ListarPokemonesConFiltros(paginacion);
        }


        
        [HttpPost]
        public IHttpActionResult ObtenerPokemonesSP(DTOPaginacion paginacion)
        {
            return Ok(servicioPokemon.ListarPokemonesSP(paginacion));
        }

        [HttpPost]
        public IHttpActionResult GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon)
        {
          
            servicioPokemon.GuardarNuevoPokemon(nuevoPokemon);
            return Ok($"Se guardo de forma correcta el pokemon {nuevoPokemon.NombrePokemon}");
        }

        [HttpDelete]
        public IHttpActionResult EliminarPokemon(int idPokemon)
        {
          
            servicioPokemon.EliminarPokemon(idPokemon);
            return Ok($"Se Eliminio de forma correcta el pokemon con el identificador: {idPokemon}");
        }

        [HttpGet]
        public IHttpActionResult ObtenerCantidadRegistrosPokemon() { 
        return Ok( servicioPokemon.ObtenerCantidadPokemones());
        }

        [HttpPost]
        public IHttpActionResult ModificarPokemon(DTOModificacionAPokemon ModificacionAPokemon)
        {            
            servicioPokemon.ModificarPokemon(ModificacionAPokemon);
            return Ok($"Se guardo de forma correcta el pokemon {ModificacionAPokemon.NombrePokemon}");
        }
    }
}
