using DTOsPokemon.DTOS;
using ServiciosPokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiPokemon.Controllers
{
    public class MovimientoController : ApiController
    {
        IServicioMovimientos servicioMovimientos;
        public MovimientoController(IServicioMovimientos servicioMovimientos)
        {
            this.servicioMovimientos = servicioMovimientos;
        }

        [HttpGet]
        public IHttpActionResult ListaMovimientos()
        {
            return Ok(servicioMovimientos.LeerMovimientos());
        }

    }
}
