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
    public class Stats
    {
        public int IdPokemon { get; set; }
        public int Ataque { get; set; }
        public int Defensa { get; set; }
        public int EspecialAtaque { get; set; }
        public int EspecialDefensa { get; set; }
        public int Velocidad { get; set; }
        public int Vida { get; set; }
        public Pokemones Pokemones { get; set; }
        
        public static void ConfigurarModelo(DbModelBuilder Modelo)
        {
            var entidad = Modelo.Entity<Stats>();
            entidad.HasKey(x => x.IdPokemon)
                .Property(x => x.IdPokemon)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //entidad.HasRequired(x => x.Pokemones).WithRequiredDependent(x => x.Stats).WillCascadeOnDelete(false);
        }
    }
}
