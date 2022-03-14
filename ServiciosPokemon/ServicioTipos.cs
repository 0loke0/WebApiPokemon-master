using InfraestructuraPokemon;
using InfraestructuraPokemon.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOS;
using InfraestructuraPokemon.Repositorios;

namespace ServiciosPokemon
{
    public interface IServicioTipos
    {
        IEnumerable<DTOTipo> LeerTipos();
    }
    public class ServicioTipos : IServicioTipos
    {

        
        IRepositorioTipo repositorioTipos;
        public ServicioTipos(
            IRepositorioTipo repositorioTipos)
        {        
            this.repositorioTipos = repositorioTipos;
        }
       
        public IEnumerable<DTOTipo> LeerTipos()
        {
            return repositorioTipos.LeerTipos();
        }
    }
}
