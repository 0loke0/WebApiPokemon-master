using Autofac;
using InfraestructuraPokemon.Modelos;
using InfraestruraPokemon.Migrations.Seeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InfraestruraPokemon
{
    public class ConfigContenedorSeed
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
                      
            builder.RegisterType<SeedMovimientos>().As<ISeedMovimientos>();
            builder.RegisterType<SeedTipo>().As<ISeedTipo>();
            //builder.RegisterType<SeedTipo>().As<ISeedTipo>().WithParameter(new TypedParameter(typeof(List<Tipos>), LSeedTipos));

            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(InfraestruraPokemon)))
            //    .Where(t => t.Namespace.Contains("Utilities"))
            //    .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}
