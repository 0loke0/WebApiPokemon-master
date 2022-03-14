using InfraestructuraPokemon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServiciosPokemon;
using Autofac;
using InfraestructuraPokemon.Modelos;
using DTOsPokemon.DTOS;



namespace ApiPokemon.Controllers
{
    public class TiposController : ApiController
    {             
        IServicioTipos servicioTipos;
        public TiposController(IServicioTipos servicioTipos)
        {
            this.servicioTipos = servicioTipos;
        }
        
        [HttpGet]
        public IEnumerable<DTOTipo> ListaTipos()
        {
           return servicioTipos.LeerTipos();
        }




        
    }
}
