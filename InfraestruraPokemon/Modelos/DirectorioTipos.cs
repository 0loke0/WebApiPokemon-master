using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Modelos
{
    public class DirectorioTipos
    {
        

        public int IdPokemon { get; set; }
        public int IdTipo { get; set; }
        public static void ConfigurarModelo(DbModelBuilder Modelo)
        {
            var entidad = Modelo.Entity<DirectorioTipos>();
            entidad.HasKey(x => new { x.IdPokemon, x.IdTipo });


            entidad.Property(x => x.IdPokemon).IsRequired();
            entidad.Property(x => x.IdTipo).IsRequired();
        }
    }
}
