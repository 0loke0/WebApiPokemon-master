namespace InfraestruraPokemon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PokeMigracion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DirectorioMovimientos",
                c => new
                    {
                        IdPokemon = c.Int(nullable: false),
                        IdMovimiento = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPokemon, t.IdMovimiento })
                .ForeignKey("dbo.Movimientos", t => t.IdMovimiento, cascadeDelete: true)
                .ForeignKey("dbo.Pokemones", t => t.IdPokemon, cascadeDelete: true)
                .Index(t => t.IdPokemon)
                .Index(t => t.IdMovimiento);
            
            CreateTable(
                "dbo.DirectorioTipos",
                c => new
                    {
                        IdPokemon = c.Int(nullable: false),
                        IdTipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdPokemon, t.IdTipo })
                .ForeignKey("dbo.Pokemones", t => t.IdPokemon, cascadeDelete: true)
                .ForeignKey("dbo.Tipos", t => t.IdTipo, cascadeDelete: true)
                .Index(t => t.IdPokemon)
                .Index(t => t.IdTipo);
            
            CreateTable(
                "dbo.Imagenes",
                c => new
                    {
                        IdImagen = c.Int(nullable: false, identity: true),
                        IdPokemon = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                        ArchivoImagen = c.String(nullable: false),
                        RutaImagen = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.IdImagen);
            
            CreateTable(
                "dbo.Movimientos",
                c => new
                    {
                        IdMovimiento = c.Int(nullable: false, identity: true),
                        NombreMovimiento = c.String(nullable: false, maxLength: 100, unicode: false),
                        Valor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdMovimiento);
            
            CreateTable(
                "dbo.Pokemones",
                c => new
                    {
                        IdPokemon = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100, unicode: false),
                        Ataque = c.Int(nullable: false),
                        Defensa = c.Int(nullable: false),
                        EspecialAtaque = c.Int(nullable: false),
                        EspecialDefensa = c.Int(nullable: false),
                        Velocidad = c.Int(nullable: false),
                        Vida = c.Int(nullable: false),
                        Rareza = c.String(),
                        NombreImagen = c.String(),
                        ArchivoImagen = c.String(),
                        RutaImagen = c.String(),
                    })
                .PrimaryKey(t => t.IdPokemon);
            
            CreateTable(
                "dbo.Tipos",
                c => new
                    {
                        IdTipo = c.Int(nullable: false, identity: true),
                        NombreTipo = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.IdTipo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DirectorioTipos", "IdTipo", "dbo.Tipos");
            DropForeignKey("dbo.DirectorioTipos", "IdPokemon", "dbo.Pokemones");
            DropForeignKey("dbo.DirectorioMovimientos", "IdPokemon", "dbo.Pokemones");
            DropForeignKey("dbo.DirectorioMovimientos", "IdMovimiento", "dbo.Movimientos");
            DropIndex("dbo.DirectorioTipos", new[] { "IdTipo" });
            DropIndex("dbo.DirectorioTipos", new[] { "IdPokemon" });
            DropIndex("dbo.DirectorioMovimientos", new[] { "IdMovimiento" });
            DropIndex("dbo.DirectorioMovimientos", new[] { "IdPokemon" });
            DropTable("dbo.Tipos");
            DropTable("dbo.Pokemones");
            DropTable("dbo.Movimientos");
            DropTable("dbo.Imagenes");
            DropTable("dbo.DirectorioTipos");
            DropTable("dbo.DirectorioMovimientos");
        }
    }
}
