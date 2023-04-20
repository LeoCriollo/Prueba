namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TC_Invitados
    {
        public int Id { get; set; }

        public int IdJustificacion { get; set; }

        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Empresa { get; set; }

        [StringLength(200)]
        public string Cargo { get; set; }
    }
}
