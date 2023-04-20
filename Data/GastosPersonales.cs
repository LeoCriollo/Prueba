namespace DoleEcIntranet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GastosPersonales
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        public int CodigoEmpleado { get; set; }

        public int Secuencia { get; set; }

        public int Year { get; set; }

        [Required]
        [StringLength(13)]
        public string Cedula { get; set; }

        public DateTime FechaEntrega { get; set; }

        [Required]
        [StringLength(100)]
        public string NombresApellidos { get; set; }

        [Column("_103")]
        public decimal C_103 { get; set; }

        [Column("_104")]
        public decimal C_104 { get; set; }

        [Column("_105")]
        public decimal C_105 { get; set; }

        [Column("_106")]
        public decimal C_106 { get; set; }

        [Column("_107")]
        public decimal C_107 { get; set; }

        [Column("_108")]
        public decimal C_108 { get; set; }

        [Column("_109")]
        public decimal C_109 { get; set; }

        [Column("_110")]
        public decimal C_110 { get; set; }

        [Column("_111")]
        public decimal C_111 { get; set; }

        [Required]
        [StringLength(13)]
        public string RucEmpleador { get; set; }

        [Required]
        [StringLength(80)]
        public string RazonSocial { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public int? UsuarioActualizacion { get; set; }

        [Required]
        [StringLength(1)]
        public string Estado { get; set; }

        [Column("_112")]
        public decimal C_112 { get; set; }

        [Column("_113")]
        public decimal? C_113 { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
