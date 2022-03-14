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
                this.IdsMovimiento = idsMovimientos;
        }


      
    }
}
