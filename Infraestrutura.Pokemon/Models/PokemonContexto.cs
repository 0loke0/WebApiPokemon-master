namespace Infraestrutura.Pokemon.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PokemonContexto : DbContext
    {
        public PokemonContexto()
            : base("name=PokemonContexto")
        {
        }

        public virtual DbSet<Habilidad> Habilidad { get; set; }
        public virtual DbSet<Movimiento> Movimiento { get; set; }
        public virtual DbSet<Pokemon> Pokemon { get; set; }
        public virtual DbSet<Stat> Stat { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habilidad>()
                .Property(e => e.NombreHabilidad)
                .IsUnicode(false);

            modelBuilder.Entity<Habilidad>()
                .HasMany(e => e.Pokemon)
                .WithMany(e => e.Habilidad)
                .Map(m => m.ToTable("DirectorioHabilidades").MapLeftKey("IdHabilidad").MapRightKey("IdPokemon"));

            modelBuilder.Entity<Movimiento>()
                .Property(e => e.NombreMobimiento)
                .IsUnicode(false);

            modelBuilder.Entity<Movimiento>()
                .HasMany(e => e.Pokemon)
                .WithMany(e => e.Movimiento)
                .Map(m => m.ToTable("DirectorioMovimiento").MapLeftKey("IdMovimiento").MapRightKey("IdPokemon"));

            modelBuilder.Entity<Pokemon>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Pokemon>()
                .HasMany(e => e.Stat)
                .WithMany(e => e.Pokemon)
                .Map(m => m.ToTable("DirectorioStat").MapLeftKey("IdPokemon").MapRightKey("IdStat"));

            modelBuilder.Entity<Pokemon>()
                .HasMany(e => e.Tipo)
                .WithMany(e => e.Pokemon)
                .Map(m => m.ToTable("DirectorioTipo").MapLeftKey("IdPokemon").MapRightKey("IdTipo"));

            modelBuilder.Entity<Tipo>()
                .Property(e => e.NombreTipo)
                .IsUnicode(false);
        }
    }
}
