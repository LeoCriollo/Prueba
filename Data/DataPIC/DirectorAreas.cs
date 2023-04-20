namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DirectorAreas
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string NombreTrab { get; set; }

        [StringLength(100)]
        public string AreaDesc { get; set; }

        [StringLength(50)]
        public string Cargo { get; set; }

        [StringLength(20)]
        public string CodTrabajador { get; set; }

        [StringLength(200)]
        public string NombreCia { get; set; }

        [StringLength(200)]
        public string NombrePuesto { get; set; }

        [StringLength(50)]
        public string Depto_Desc { get; set; }

        [StringLength(200)]
        public string Centr_costo { get; set; }

        [StringLength(20)]
        public string CodigoCia { get; set; }

        [StringLength(10)]
        public string Estado { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        [StringLength(50)]
        public string UsuarioModificacion { get; set; }

        [StringLength(10)]
        public string AreaResp { get; set; }

        [StringLength(50)]
        public string Depto { get; set; }
    }
}
