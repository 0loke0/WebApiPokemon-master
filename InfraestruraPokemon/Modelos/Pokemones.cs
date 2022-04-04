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
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
        public string Rareza { get; set; }
        public string NombreImagen { get; set; }
        public string ArchivoImagen { get; set; }
        public string RutaImagen { get; set; }
        public string Detalle { get; set; }



        public virtual ICollection<DirectorioMovimientos> DirectorioMovimientos { get; set; }
        public virtual ICollection<DirectorioTipos> DirectorioTipos { get; set; }
    


        public static void ConfigurarModelo(DbModelBuilder modelo) {
            var entidad = modelo.Entity<Pokemones>();
            entidad.HasKey(x => x.IdPokemon);

            entidad.Property(x => x.Nombre)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();


            //entidad.HasMany(x => x.Imagenes).WithRequired().HasForeignKey(x => x.IdPokemon).WillCascadeOnDelete(false);            
            

        }
       

    }
}
