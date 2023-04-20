namespace DoleEcIntranet.Data.DataAdam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rol_ImpRenta_ADAM
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

        [StringLength(4)]
        public string cargo_trabajador { get; set; }

        [StringLength(40)]
        public string desc_carg_trabajador { get; set; }

        [StringLength(25)]
        public string cedula { get; set; }

        [StringLength(10)]
        public string centro_costo { get; set; }

        [StringLength(40)]
        public string desc_ccosto { get; set; }

        [StringLength(10)]
        public string grupo_nomina { get; set; }

        [StringLength(40)]
        public string desc_gnomina { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string clase_nomina { get; set; }

        [StringLength(40)]
        public string desc_clase_nomina { get; set; }

        public short? anio { get; set; }

        public byte? periodo { get; set; }

        public byte? sec_anio { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fecha_inicio_periodo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fecha_fin_periodo { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short concepto { get; set; }

        [StringLength(40)]
        public string desc_concepto { get; set; }

        public byte? tipo_transaccion { get; set; }

        [StringLength(1)]
        public string tipo_movimiento { get; set; }

        public decimal? tiempo { get; set; }

        public decimal? importe { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime fecha_carga { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(4)]
        public string sistema { get; set; }
    }
}
