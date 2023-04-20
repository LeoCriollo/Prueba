namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TC_Justificacion
    {
        public int Id { get; set; }

        public int IdEC { get; set; }

        public int TipoConsumo { get; set; }

        public int IdTipoMoneda { get; set; }

        public decimal TipoCambio { get; set; }

        public decimal ValorFactura { get; set; }

        [StringLength(200)]
        public string ComentariosConsumo { get; set; }

        [Required]
        [StringLength(50)]
        public string NumFactura { get; set; }

        public int TipoFactura { get; set; }

        public DateTime FechaFactura { get; set; }

        public int TipoProveedor { get; set; }

        [Required]
        [StringLength(20)]
        public string Ruc { get; set; }

        [Required]
        [StringLength(200)]
        public string RazonSocial { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Required]
        [StringLength(100)]
        public string UsuarioCreacion { get; set; }

        [StringLength(100)]
        public string UsuarioModificacion { get; set; }
    }
}
