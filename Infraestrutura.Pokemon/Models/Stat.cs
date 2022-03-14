namespace Infraestrutura.Pokemon.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stat")]
    public partial class Stat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Stat()
        {
            Pokemon = new HashSet<Pokemon>();
        }

        [Key]
        public int IdStat { get; set; }

        public int? Ataque { get; set; }

        public int? Defensa { get; set; }

        public int? EspecialAtaque { get; set; }

        public int? EspecialDefensa { get; set; }

        public int? Velocidad { get; set; }

        public int? Vida { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pokemon> Pokemon { get; set; }
    }
}
