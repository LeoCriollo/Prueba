namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoConsumo")]
    public partial class TipoConsumo
    {
        public int Id { get; set; }

        public int Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }
    }
}
