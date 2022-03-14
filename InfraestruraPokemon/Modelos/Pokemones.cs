using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Modelos
{
    public class Pokemones
    {

        public int IdPokemon{get; set;}
        public string Nombre { get; set; }
        public virtual ICollection<DirectorioMovimientos> DirectorioMovimientos { get; set; }
        public virtual ICollection<DirectorioTipos> DirectorioTipos { get; set; }
        public virtual ICollection<Imagenes> Imagenes { get; set; }
        public virtual Stats Stats  { get; set; }


        public static void ConfigurarModelo(DbModelBuilder modelo) {
            var entidad = modelo.Entity<Pokemones>();
            entidad.HasKey(x => x.IdPokemon);

            entidad.Property(x => x.Nombre)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();

            entidad.HasMany(x => x.Imagenes).WithRequired().HasForeignKey(x => x.IdPokemon).WillCascadeOnDelete(false);            
            

        }
       

    }
}
