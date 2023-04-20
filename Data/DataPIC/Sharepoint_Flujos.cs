namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sharepoint_Flujos
    {
        public int Id { get; set; }

        public int Secuencial { get; set; }

        [StringLength(50)]
        public string Formulario { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        public int? Nivel { get; set; }

        [StringLength(50)]
        public string Aprobador { get; set; }

        [StringLength(400)]
        public string Comentario { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
