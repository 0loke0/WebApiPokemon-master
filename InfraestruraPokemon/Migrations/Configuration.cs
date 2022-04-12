namespace InfraestruraPokemon.Migrations
{
    using InfraestruraPokemon.Migrations.Seeds;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<InfraestructuraPokemon.ContextoPokemon>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(InfraestructuraPokemon.ContextoPokemon context)
        {
            //TODO: ver nuevamente el funcionamieto de las Seeds
            //var contenedor = ConfigContenedorSeed.Configure();
            //SeedMovimientos seedMovimiento = new SeedMovimientos();

            //context.Movimientos.AddRange(seedMovimiento.DataMovimientos);

            //SeedTipo seedTipo = new SeedTipo();
            //context.Tipos.AddRange(seedTipo.DataTipos);


            //base.Seed(context);
        }
    }
}
