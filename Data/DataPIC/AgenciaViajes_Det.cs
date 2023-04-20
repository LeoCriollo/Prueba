namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AgenciaViajes_Det
    {
        public int Id { get; set; }

        public int IdAgenciaViajes { get; set; }

        [Required]
        [StringLength(100)]
        public string Correo { get; set; }
    }
}
