namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Proveedores
    {
        public int Id { get; set; }

        [StringLength(13)]
        public string Ruc { get; set; }

        [StringLength(200)]
        public string RazonSocial { get; set; }

        public int Tipo { get; set; }
    }
}
