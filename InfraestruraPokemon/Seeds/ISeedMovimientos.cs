using System.Collections.Generic;
using InfraestructuraPokemon.Modelos;

namespace InfraestruraPokemon.Migrations.Seeds
{
    interface ISeedMovimientos
    {
        IList<Movimientos> DataMovimientos { get; set; }
    }
}