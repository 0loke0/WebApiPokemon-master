using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon.Modelos
{
    public class Tipos
    {
       

        public int IdTipo {get;set;}
        public string NombreTipo { get; set; }
        public ICollection<DirectorioTipos> DirectorioTipos { get;set;}

        public static void ConfigurarModelo(DbModelBuilder Modelo)
        {
            var entidad = Modelo.Entity<Tipos>();

            entidad.HasKey(x => x.IdTipo);

            entidad.Property(x => x.NombreTipo)                
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();                        
        }
    }
}
