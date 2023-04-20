namespace DoleEcIntranet.Data.DataBalanza
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TiposEmpaques
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string Pais { get; set; }

        public decimal PesoLibraIni { get; set; }

        public decimal PesoLibraFin { get; set; }

        public decimal? PesoCaja { get; set; }

        [Required]
        [StringLength(20)]
        public string UsuarioCreador { get; set; }

        public DateTime FechaCreacion { get; set; }

        [StringLength(20)]
        public string UsuarioActualizador { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
