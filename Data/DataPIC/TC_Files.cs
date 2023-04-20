namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TC_Files
    {
        public int Id { get; set; }

        public int IdJustificacion { get; set; }

        [Required]
        public byte[] Archivo { get; set; }

        [Required]
        [StringLength(50)]
        public string Extension { get; set; }
    }
}
