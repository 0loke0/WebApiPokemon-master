using Autofac;
using InfraestruraPokemon.Migrations.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestruraPokemon.Utilidades
{
    public class ConfigContenedorUtilidades
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UtilidadesNumeros>().As<IUtilidadesNumeros>();
            builder.RegisterType<UtilidadesString>().As<IUtilidadesString>();
            

            return builder.Build();
        }
    }
}
