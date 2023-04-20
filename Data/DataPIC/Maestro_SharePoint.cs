namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Maestro_SharePoint
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string compania { get; set; }

        [StringLength(60)]
        public string nombre_cia { get; set; }

        [StringLength(50)]
        public string trabajador { get; set; }

        [StringLength(200)]
        public string nombre_trab { get; set; }

        [StringLength(50)]
        public string depto { get; set; }

        [StringLength(50)]
        public string depto_des { get; set; }

        [StringLength(50)]
        public string centr_costo { get; set; }

        [StringLength(50)]
        public string centro_costo_des { get; set; }

        [StringLength(50)]
        public string area_resp { get; set; }

        [StringLength(200)]
        public string area_resp_des { get; set; }

        [StringLength(50)]
        public string puesto { get; set; }

        [StringLength(200)]
        public string nombre_puesto { get; set; }

        [StringLength(50)]
        public string div_pais { get; set; }

        [StringLength(50)]
        public string div_pais_des { get; set; }

        [StringLength(50)]
        public string e_mail { get; set; }

        [StringLength(50)]
        public string codigo_jefe { get; set; }

        [StringLength(200)]
        public string nombre_jefe { get; set; }

        [StringLength(15)]
        public string cedula { get; set; }
    }
}
