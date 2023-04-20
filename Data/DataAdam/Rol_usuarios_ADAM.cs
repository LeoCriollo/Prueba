namespace DoleEcIntranet.Data.DataAdam
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rol_usuarios_ADAM
    {
        [StringLength(4)]
        public string cod_cia { get; set; }

        [StringLength(60)]
        public string nom_cia { get; set; }

        [StringLength(2)]
        public string clase_nomina { get; set; }

        [StringLength(40)]
        public string desc_cl_nomina { get; set; }

        [Key]
        [StringLength(10)]
        public string num_personal { get; set; }

        [StringLength(100)]
        public string nombre { get; set; }

        [Required]
        [StringLength(25)]
        public string ced_identidad { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fh_nacimiento { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? fh_priming { get; set; }

        [StringLength(4)]
        public string cod_cargo { get; set; }

        [StringLength(40)]
        public string nom_cargo { get; set; }

        [StringLength(20)]
        public string extension { get; set; }

        [StringLength(10)]
        public string cod_grpnomina { get; set; }

        [StringLength(40)]
        public string des_grpnomina { get; set; }

        [StringLength(10)]
        public string cod_departamento { get; set; }

        [StringLength(40)]
        public string des_departamento { get; set; }

        [StringLength(10)]
        public string cod_ccosto { get; set; }

        [StringLength(40)]
        public string des_ccosto { get; set; }

        [StringLength(10)]
        public string cod_zonageo { get; set; }

        [StringLength(40)]
        public string des_zonageo { get; set; }

        [StringLength(10)]
        public string num_jefe { get; set; }

        [StringLength(100)]
        public string nombre_jefe { get; set; }

        [StringLength(4)]
        public string cod_cargojefe { get; set; }

        [StringLength(40)]
        public string des_cargojefe { get; set; }

        [StringLength(1)]
        public string status { get; set; }

        public DateTime? fh_carga { get; set; }

        [StringLength(4)]
        public string sistema_origen { get; set; }

        [StringLength(10)]
        public string clave { get; set; }

        [StringLength(20)]
        public string desc_banco { get; set; }

        [StringLength(10)]
        public string cuenta_banco { get; set; }

        [StringLength(2)]
        public string tipo_cta { get; set; }
    }
}
