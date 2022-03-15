using DTOsPokemon.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using InfraestructuraPokemon.Modelos;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Repositorios
{
    public interface IRepositorioMovimientos {
        IEnumerable<DTOMovimiento> LeerMovimientos();
    }
    public class RepositorioMovimientos : IRepositorioMovimientos
    {        

        ContextoPokemon contextoPokemon;
        public RepositorioMovimientos(ContextoPokemon contextoPokemon )
        {
            this.contextoPokemon = contextoPokemon;
        }

        public DTOMovimiento ConbertirADto(Movimientos movimiento) {
            return new DTOMovimiento {
                IdMovimiento = movimiento.IdMovimiento,
                NombreMovimiento = movimiento.NombreMovimiento,
                Valor = movimiento.Valor            
            };
        }
        public IEnumerable<DTOMovimiento> LeerMovimientos()
        {
           
            return contextoPokemon.Movimientos.ToList().Select(x=>ConbertirADto(x));    
           
        }
    }
}
