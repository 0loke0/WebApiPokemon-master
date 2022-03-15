using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOS;
using InfraestructuraPokemon.Repositorios;

namespace ServiciosPokemon
{
    public interface IServicioMovimientos {
        IEnumerable<DTOMovimiento> LeerMovimientos();
    }
    public class ServicioMovimientos : IServicioMovimientos
    {
        IRepositorioMovimientos repositorioMovimientos;
        public ServicioMovimientos(IRepositorioMovimientos repositorioMovimientos)
        {
            this.repositorioMovimientos = repositorioMovimientos;
        }
        public IEnumerable<DTOMovimiento> LeerMovimientos()
        {
            return repositorioMovimientos.LeerMovimientos();
        }

    }
}
