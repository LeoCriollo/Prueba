namespace DoleEcIntranet.Data.DataAdam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rol_EstCta_ADAM
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string compania { get; set; }

        [StringLength(60)]
        public string nombre_cia { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string trabajador { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fecha_credito { get; set; }

        public short? anio_credito { get; set; }

        public byte? periodo_credito { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fecha_vencimiento { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short concepto { get; set; }

        [StringLength(40)]
        public string desc_concepto { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(10)]
        public string no_prestamo { get; set; }

        public byte? situacion { get; set; }

        public decimal? monto_credito { get; set; }

        public decimal? tasa { get; set; }

        public short? total_periodos { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short no_cuota { get; set; }

        [Key]
        [Column(Order = 5, TypeName = "smalldatetime")]
        public DateTime fecha_venc_pago { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fecha_pago { get; set; }

        public short? anio_pago { get; set; }

        public byte? periodo_pago { get; set; }

        public decimal? saldo { get; set; }

        public decimal? cuota { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime fecha_carga { get; set; }

        [Key]
        [Column(Order = 7)]
        [StringLength(4)]
        public string sistema { get; set; }
    }
}
