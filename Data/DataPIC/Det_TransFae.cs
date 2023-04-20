namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Det_TransFae
    {
        public int Id { get; set; }

        public int IdCab_TransFae { get; set; }

        public int Idtipoconsumo { get; set; }

        public decimal valor { get; set; }

        public decimal total { get; set; }

        [StringLength(200)]
        public string descripcionGastos { get; set; }

        public int? cantInvitados { get; set; }
    }
}
