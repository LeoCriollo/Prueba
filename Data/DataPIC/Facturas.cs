namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Facturas
    {
        public int Id { get; set; }

        public int Id_Det { get; set; }

        [StringLength(10)]
        public string TipoFactura { get; set; }

        [StringLength(50)]
        public string NumFactura { get; set; }

        public DateTime FechaFactura { get; set; }
    }
}
