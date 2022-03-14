using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Modelos
{
    public class Imagenes
    {

        public int IdPokemon { get; set; }
        public int IdImagen { get; set; }
        public string Nombre { get; set; }

        public string ArchivoImagen { get; set; }
        public string RutaImagen { get; set; }


        public static void ConfigurarModelo(DbModelBuilder modelo)
        {
            var entidad = modelo.Entity<Imagenes>();

            entidad.Property(x => x.IdPokemon).IsRequired();

            entidad.HasKey( x => x.IdImagen);

            entidad.Property(x => x.Nombre)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();

            entidad.Property(x => x.ArchivoImagen)                 
                .IsUnicode(true)                
                .IsRequired();

            entidad.Property(x => x.RutaImagen)
                .IsUnicode(false);          
          
        }
    }
}
