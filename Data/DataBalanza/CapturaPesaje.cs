namespace DoleEcIntranet.Data.DataBalanza
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CapturaPesaje")]
    public partial class CapturaPesaje
    {
        public int Id { get; set; }

        public int IdFinca { get; set; }

        public int IdEmpaque { get; set; }

        public int NumEmpacador { get; set; }

        public DateTime FechaHoraCaptura { get; set; }

        public double PesoLibras { get; set; }

        public double PesoKilos { get; set; }

        public DateTime FechaSincronizacion { get; set; }
    }
}
