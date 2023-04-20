namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invitados
    {
        public int Id { get; set; }

        public int Id_Det_Trans { get; set; }

        [Required]
        [StringLength(200)]
        public string Cargo { get; set; }

        [Required]
        [StringLength(200)]
        public string Empresa { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }
    }
}
