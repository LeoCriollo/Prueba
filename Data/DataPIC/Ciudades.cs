namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ciudades
    {
        public int Id { get; set; }

        public int IdPais { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public virtual Paises Paises { get; set; }
    }
}
