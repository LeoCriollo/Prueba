namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoMoneda")]
    public partial class TipoMoneda
    {
        public int Id { get; set; }

        public int Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        public int Estado { get; set; }
    }
}
