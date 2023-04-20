namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Flujo")]
    public partial class Flujo
    {
        public int Id { get; set; }

        public int Secuencial { get; set; }

        [StringLength(50)]
        public string Formulario { get; set; }

        public int? Nivel { get; set; }

        [StringLength(10)]
        public string Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string Revisor { get; set; }

        [StringLength(100)]
        public string Correo_Revisor { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
