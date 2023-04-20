namespace DoleEcIntranet.Data.DataBalanza
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CupoDiario")]
    public partial class CupoDiario
    {
        public int Id { get; set; }

        public int IdFinca { get; set; }

        public int IdEmpaque { get; set; }

        public DateTime FechaHora { get; set; }

        public int Cupo { get; set; }

        public DateTime FechaHoraSincronizacion { get; set; }
    }
}
