
using System.Web.Http;
using ServiciosPokemon;
using DTOsPokemon.DTOS;

namespace ApiPokemon.Controllers
{
    public class PokemonesController : ApiController
    {
        public IServicioPokemon servicioPokemon;
        public PokemonesController(IServicioPokemon servicioPokemon)
        {
            this.servicioPokemon = servicioPokemon;
        }

        [HttpGet]
        public IHttpActionResult ObtenerPokemones(DTOPaginacion paginacion)
        {
            return Ok(servicioPokemon.ListarPokemones(paginacion));
        }
       

        [HttpPost]
        public IHttpActionResult GuardarNuevoPokemon(DTONuevoPokemon nuevoPokemon)
        {
            servicioPokemon.GuardarNuevoPokemon(nuevoPokemon);
            return Ok($"Se guardo de forma correcta el pokemon {nuevoPokemon.NombrePokemon}");
        }       

       
    }
}
