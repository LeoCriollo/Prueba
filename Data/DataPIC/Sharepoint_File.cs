namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sharepoint_File
    {
        public int Id { get; set; }

        public int? IdDet { get; set; }

        [StringLength(50)]
        public string Formulario { get; set; }

        [Required]
        public byte[] Archive { get; set; }

        [Required]
        [StringLength(200)]
        public string ContentType { get; set; }

        [Required]
        [StringLength(200)]
        public string NameFile { get; set; }

        [Required]
        [StringLength(50)]
        public string Extension { get; set; }
    }
}
