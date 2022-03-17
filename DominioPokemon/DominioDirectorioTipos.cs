
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTOsPokemon.DTOS;

namespace DominioPokemon
{


    public class DominioDirectorioTipos
    {
        public List<int> IdsTipo { get; set; }
        public DominioDirectorioTipos(List<int> idsTipos)
        {
            this.IdsTipo = idsTipos;
            Validar();
        }



        private void Validar()        {           

            if (IdsTipo.Count() > 2)
            {
                throw new Exception("Un Pokémon solo puede tener máximo 2 tipos");
            }

            if (IdsTipo.Count() == 2)
            {
                if (IdsTipo[0] == IdsTipo[1])
                {
                    throw new Exception("Se ha seleccionado dos veces el mismo tipo ");
                }
            }

        }
        //public void GuardarNuevaRelacionDirectorioTipos(DTODirectorioTipos relacionPokemonTipo)
        //{
        //    if (
        //    validadorCantidadRegistrosDirectorioTipo(relacionPokemonTipo.IdPokemon))
        //    {
        //        repositorioDirectorioTipos.GuardarRelacion(relacionPokemonTipo);
        //    }
        //    else
        //    {
        //        throw new Exception("No se puede guardar más de dos tipos para un mismo Pokémon");
        //    }
        //}





    }
}
