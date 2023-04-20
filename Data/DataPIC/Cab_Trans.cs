namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cab_Trans
    {
        public int Id { get; set; }

        public int Secuencial { get; set; }

        public string SecuenExterno { get; set; }

        public DateTime FechaSolicitud { get; set; }

        [StringLength(10)]
        public string SlDestino { get; set; }

        [Required]
        [StringLength(50)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string SlTipoConsumo { get; set; }

        [StringLength(200)]
        public string MotivoGasto { get; set; }

        [StringLength(100)]
        public string CentroCosto { get; set; }

        [StringLength(50)]
        public string CodCentroCostoA { get; set; }

        [StringLength(50)]
        public string IdDirector { get; set; }

        [StringLength(50)]
        public string IdAutoridadFinanciera { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        [Required]
        [StringLength(50)]
        public string UsuarioCreacion { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        [StringLength(50)]
        public string FechaDesde { get; set; }

        [StringLength(50)]
        public string FechaHasta { get; set; }

        [StringLength(400)]
        public string Nivel1 { get; set; }

        [StringLength(400)]
        public string Nivel2 { get; set; }

        [StringLength(400)]
        public string Nivel3 { get; set; }
    }
}
