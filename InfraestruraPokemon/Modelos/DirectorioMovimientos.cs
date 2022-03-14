using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Modelos
{
    public class DirectorioMovimientos
    {
        public int IdPokemon { get; set; }
        public int IdMovimiento { get; set; }
        public static void ConfigurarModelo(DbModelBuilder modelo)
        {
            var entidad = modelo.Entity<DirectorioMovimientos>();
            entidad.HasKey(x => new { x.IdPokemon,x.IdMovimiento});

            entidad.Property(x => x.IdPokemon).IsRequired();
            entidad.Property(x => x.IdMovimiento).IsRequired();
        }
    }
}
