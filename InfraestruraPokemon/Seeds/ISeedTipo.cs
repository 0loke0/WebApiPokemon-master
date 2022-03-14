using System.Collections.Generic;
using InfraestructuraPokemon.Modelos;

namespace InfraestruraPokemon.Migrations.Seeds
{
    public interface ISeedTipo
    {
        IList<Tipos> DataTipos { get; set; }
    }
}