using InfraestructuraPokemon.Modelos;
using InfraestruraPokemon.Migrations.Utilidades;
using System.Collections.Generic;
using InfraestruraPokemon.Utilidades;
using Autofac;

namespace InfraestruraPokemon.Migrations.Seeds
{
    public class SeedTipo : ISeedTipo
    {
        public IList<Tipos> DataTipos { get; set; }
        IList<Tipos> LSeedTipos = new List<Tipos>();
        UtilidadesString utilidadesString = new UtilidadesString();
        List<string> listaNombresTipos = new List<string>() {
            "acero" ,
            "agua",
            "Bicho",
            "dragón",
            "eléctrico",
            "fantasma",
            "fuego",
            "hada",
            "hielo",
            "lucha",
            "Normal",
            "planta",
            "psíquico",
            "roca",
            "Siniestro",
            "Tierra",
            "Veneno",
            "Volador",
        };
        public  SeedTipo() {
           
            var contenedorUtilidades = ConfigContenedorUtilidades.Configure();
            using (var rango = contenedorUtilidades.BeginLifetimeScope())
            {
                var utilidadesString = rango.Resolve<IUtilidadesString>();
                foreach (string nombre in listaNombresTipos)
                {
                    LSeedTipos.Add(new Tipos() { NombreTipo = utilidadesString.PrimeraMayuscula(nombre) });
                }
                DataTipos = LSeedTipos;
            }
            
        }

    }
}
