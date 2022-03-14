using InfraestructuraPokemon.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructuraPokemon
{
   
    public class ContextoPokemon : DbContext
    {

       
        public ContextoPokemon() : base("name=DbPokemones") { }
        public DbSet<DirectorioMovimientos> DirectorioMovimientos { get; set; }       
        public DbSet<DirectorioTipos> DirectorioTipos { get; set; }
        public DbSet<Imagenes> Imagenes { get; set; }
        public DbSet<Movimientos> Movimientos { get; set; }
        public DbSet<Pokemones> Pokemones { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<Tipos> Tipos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            
            Modelos.Imagenes.ConfigurarModelo(modelBuilder);
            Modelos.Pokemones.ConfigurarModelo(modelBuilder);
            Modelos.DirectorioMovimientos.ConfigurarModelo(modelBuilder);
            Modelos.DirectorioTipos.ConfigurarModelo(modelBuilder);
            Modelos.Movimientos.ConfigurarModelo(modelBuilder);
            Modelos.Stats.ConfigurarModelo(modelBuilder);
            Modelos.Tipos.ConfigurarModelo(modelBuilder);
        }
    }

   

}
