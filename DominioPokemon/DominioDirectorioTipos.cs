
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
            Validar(idsTipos);
            this.IdsTipo = idsTipos;    

        }

        private void Validar(List<int> idsTipos)        {           

            if (idsTipos.Count() > 2)
            {
                throw new Exception("Un pokemon solo puede tener maximo 2 tipos");
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
