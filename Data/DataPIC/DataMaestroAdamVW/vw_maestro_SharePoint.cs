namespace DoleEcIntranet.Data.DataPIC.DataMaestroAdamVW
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vw_maestro_SharePoint
    {
        [Key]
        [StringLength(4)]
        public string compania { get; set; }

        [StringLength(60)]
        public string nombre_cia { get; set; }

        [StringLength(10)]
        public string trabajador { get; set; }

        [StringLength(8000)]
        public string nombre_trab { get; set; }

        [StringLength(10)]
        public string depto { get; set; }

        [StringLength(40)]
        public string depto_des { get; set; }

        [StringLength(10)]
        public string centr_costo { get; set; }

        [StringLength(40)]
        public string centro_costo_des { get; set; }

        [StringLength(10)]
        public string area_resp { get; set; }

        [StringLength(40)]
        public string area_resp_des { get; set; }

        [StringLength(4)]
        public string puesto { get; set; }

        [StringLength(40)]
        public string nombre_puesto { get; set; }

        [StringLength(10)]
        public string div_pais { get; set; }

        [StringLength(40)]
        public string div_pais_des { get; set; }

        [StringLength(100)]
        public string e_mail { get; set; }

        [StringLength(10)]
        public string codigo_jefe { get; set; }

        [StringLength(8000)]
        public string nombre_jefe { get; set; }

        [StringLength(25)]
        public string cedula { get; set; }
    }
}
