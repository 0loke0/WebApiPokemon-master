namespace InfraestruraPokemon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Web.Hosting;

    public partial class ImplementacionSeed : DbMigration
    {
        public override void Up()
        {
            Sql($@"IF EXISTS (SELECT * FROM sysobjects WHERE name='GetSeccionPokemones') BEGIN
	DROP PROCEDURE [dbo].[GetSeccionPokemones]
END
GO

CREATE PROCEDURE [dbo].[GetSeccionPokemones]
	 @UbicacionPagina INT
	,@CantidadRegistros INT
AS

BEGIN
	SET NOCOUNT ON

	DECLARE @TMPSeccion 
	TABLE 
	(IdPokemon INT NOT NULL)
	
	INSERT INTO  @TMPSeccion
	SELECT IdPokemon 
	FROM DBO.Pokemones 
	ORDER BY IdPokemon OFFSET @UbicacionPagina ROWS FETCH NEXT @CantidadRegistros ROWS ONLY
	
	SELECT 
	 P.IdPokemon AS Id
	,P.Nombre AS Nombre
	,P.Ataque AS Ataque
	,P.Defensa AS Defensa
	,P.EspecialAtaque AS EspecialAtaque
	,P.EspecialDefensa AS EspecialDefensa
	,P.Velocidad AS Velocidad
	,P.Vida AS Vida
	,P.NombreImagen AS NombreImagen
	,P.RutaImagen AS RutaImagen
	,P.Rareza AS Rareza
	,P.Detalle AS Detalle
	FROM dbo.Pokemones AS P
	INNER JOIN @TMPSeccion AS TPM ON P.IdPokemon = TPM.IdPokemon

	
	SELECT 
	 P.IdPokemon AS IdTemporalPokemon	
	,M.IdMovimiento AS IdMovimiento
	,M.NombreMovimiento AS NombreMovimiento
	,M.Valor AS Valor
	FROM dbo.Pokemones AS P
	INNER JOIN dbo.DirectorioMovimientos AS DM ON P.IdPokemon = DM.IdPokemon
	INNER JOIN dbo.Movimientos AS M ON DM.IdMovimiento = M.IdMovimiento
	INNER JOIN @TMPSeccion AS TPM ON P.IdPokemon = TPM.IdPokemon
	
	SELECT  
	 P.IdPokemon AS IdTemporalPokemon		
	,T.IdTipo AS IdTipo
	,T.NombreTipo AS NombreTipo	
	FROM DBO.Pokemones AS P
	INNER JOIN dbo.DirectorioTipos AS DT ON P.IdPokemon = DT.IdPokemon
	INNER JOIN dbo.Tipos AS T ON DT.IdTipo = T.IdTipo
	INNER JOIN @TMPSeccion AS TPM ON P.IdPokemon = TPM.IdPokemon

END ");
        }

        public override void Down()
        {
        }
    }
}
