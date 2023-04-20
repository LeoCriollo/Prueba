namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Fae_Asistentes
    {
        public int Id { get; set; }

        public int Id_Cab_TransFae { get; set; }

        [StringLength(50)]
        public string Nombre { get; set; }
    }
}
