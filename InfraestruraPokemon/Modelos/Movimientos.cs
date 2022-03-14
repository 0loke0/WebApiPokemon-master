using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Modelos
{
    public class Movimientos
    {

        public int IdMovimiento { get; set; }
        public string NombreMovimiento { get; set; }
        public int Valor { get; set; }

        public ICollection<DirectorioMovimientos> DirectorioMovimientos { get; set; }

        public static void ConfigurarModelo(DbModelBuilder Modelo)
        {
            var entidad = Modelo.Entity<Movimientos>();
            entidad.HasKey(x => x.IdMovimiento);


            entidad.Property(x => x.NombreMovimiento)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();

            entidad.Property(x => x.Valor).IsRequired();
        }
    }
}
