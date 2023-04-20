namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TC_EstadoCuenta
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string IdEstadoCuenta { get; set; }

        [StringLength(50)]
        public string Identificador { get; set; }

        public DateTime FechaConsumo { get; set; }

        [StringLength(50)]
        public string Cedula { get; set; }

        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        public decimal? Valor { get; set; }

        public int Corte { get; set; }

        public int Documento { get; set; }

        public int NumeroTC { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int Estado { get; set; }
    }
}
