namespace InfraestruraPokemon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PokeMigracion2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pokemones", "Detalle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pokemones", "Detalle");
        }
    }
}
