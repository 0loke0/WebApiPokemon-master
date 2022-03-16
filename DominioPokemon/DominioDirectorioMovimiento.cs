using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominioPokemon
{
    public  class DominioDirectorioMovimiento
    {

        public List<int> IdsMovimiento { get; set; }
        public DominioDirectorioMovimiento(List<int> idsMovimientos)
        {
                validar();
                this.IdsMovimiento = idsMovimientos;
        }

       
        private void validar() {
            if (IdsMovimiento.Count() == 2)
            {
                if (IdsMovimiento[0] == IdsMovimiento[1])
                {
                    throw new Exception("Se ha seleccionado dos veces el mismo movimiento");
                }
            }
        } 
      
    }
}
