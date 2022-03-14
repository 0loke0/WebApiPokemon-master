using Autofac;
using InfraestructuraPokemon.Modelos;
using InfraestruraPokemon.Migrations.Utilidades;
using InfraestruraPokemon.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestruraPokemon.Migrations.Seeds
{
    class SeedMovimientos : ISeedMovimientos
    {
        
        public IList<Movimientos> DataMovimientos { get; set; }
        static int min = 0;
        static int max = 100;
        


        public readonly List<string> movimientos = new List<string> {
            { "Aligerar"  },
            { "Anclaje" },
            { "Anegar" },
            { "Azote torrencial" },
            { "Aguijón letal" },
            { "Auxilio" },
            { "Amplificador" },
            { "Ataque fulgor" },
            { "Ascuas" },
            { "Anillo ígneo" },
            { "Arrumaco sideral" },
            { "Alud" },
            { "Anticipo"},           
            { "Acupresión" },
            { "Afilar"},
            { "Amago" },
            { "Asalto estelar" },
            { "Alquitranazo"},
            { "Avalancha" },
            { "Aerochorro" },
        };
        public  SeedMovimientos()
        {
            var contenedorUtilidades = ConfigContenedorUtilidades.Configure();
            using (var rango = contenedorUtilidades.BeginLifetimeScope())
            {
                var UtilidadesNumeros = rango.Resolve<IUtilidadesNumeros>();
                IList<Movimientos> LSeedMivimientos = new List<Movimientos>();

                foreach (string movimiento in movimientos)
                {
                    LSeedMivimientos.Add(new Movimientos()
                    {
                        NombreMovimiento = movimiento,
                        Valor = UtilidadesNumeros.NumeroRamdom(min, max)
                    });
                }
                DataMovimientos = LSeedMivimientos;
            }

           
        }
    }
}
