namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Det_Trans
    {
        public int Id { get; set; }

        public int Id_Cab { get; set; }

        public int? destinoD { get; set; }

        [StringLength(100)]
        public string namefile { get; set; }

        [StringLength(50)]
        public string inputfile { get; set; }

        public int? IdProvee { get; set; }

        public int? IdTipoConsumo { get; set; }

        public int? IdMoneda { get; set; }

        public int? AtencionNegocios { get; set; }

        public decimal? Tcambio { get; set; }

        public decimal? Monto { get; set; }

        public decimal? Valor { get; set; }

        public int? IdProveedor { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }
    }
}
