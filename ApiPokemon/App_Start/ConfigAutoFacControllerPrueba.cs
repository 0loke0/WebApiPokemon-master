using Autofac;
using Autofac.Integration.WebApi;
using InfraestructuraPokemon;
using InfraestructuraPokemon.Repositorios;
using ServiciosPokemon;
using System.Reflection;
using System.Web.Http;


namespace ApiPokemon
{
    public static class ConfigAutoFacControllerPrueba
    {
        public static void Configure() {
            var builder = new ContainerBuilder();
            //Controladores
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Servicios
            
            builder.RegisterType<ServicioPokemon>().As<IServicioPokemon>();
            builder.RegisterType<ServicioTipos>().As<IServicioTipos>();
            builder.RegisterType<ServicioMovimientos>().As<IServicioMovimientos>();
            //builder.RegisterType<ServicioStats>().As<IServicioStats>();
            //builder.RegisterType<ServicioDirectorioTipo>().As<IServicioDirectorioTipo>();
            //builder.RegisterType<ServicioImagenes>().As<IServicioImagenes>();

            //Repositorio
            builder.RegisterType<RepositorioPokemon>().As<IRepositorioPokemon>();
            builder.RegisterType<RepositorioTipos>().As<IRepositorioTipo>();
            builder.RegisterType<RepositorioMovimientos>().As<IRepositorioMovimientos>();
            builder.RegisterType<RepositorioStats>().As<IRepositorioStats>();
            builder.RegisterType<RepositorioDirectorioTipos>().As<IRepositorioDirectorioTipos>();
            builder.RegisterType<RepositorioDirectorioMovimientos>().As<IRepositorioDirectorioMovimientos>();
            builder.RegisterType<RepositorioImagenes>().As<IRepositorioImagenes>();



            //Contexto
            builder.RegisterType<ContextoPokemon>();
            IContainer container = builder.Build();

           
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}

