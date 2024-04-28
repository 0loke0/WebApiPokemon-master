namespace InfraestruraPokemon.Migrations
{
    using InfraestruraPokemon.Migrations.Seeds;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Hosting;

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
    public static class SqlFileTrigger
    {
        public static string GetRawSql(string fileName)
        {
            var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations/BD/SPs"); // your default migrations folder
            var filePath = Path.Combine(baseDir, fileName);
            return File.ReadAllText(filePath);
        }

        public static string MapPath(string seedFile)
        {
            if (HttpContext.Current != null)
                return HostingEnvironment.MapPath(seedFile);

            var absolutePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath; //was AbsolutePath but didn't work with spaces according to comments
            var directoryName = Path.GetDirectoryName(absolutePath);
            var path = Path.Combine(directoryName, ".." + seedFile.TrimStart('~').Replace('/', '\\'));

            return path;
        }
    }
}
