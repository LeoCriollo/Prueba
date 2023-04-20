namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cab_transFae
    {
        public int Id { get; set; }

        public int? codEmpleado { get; set; }

        public int Secuencial { get; set; }

        [StringLength(10)]
        public string Alcance { get; set; }

        public DateTime FechaSolicitud { get; set; }

        public int? IdAutoridadFinanciera { get; set; }

        [StringLength(200)]
        public string motivoAnticipo { get; set; }

        [StringLength(50)]
        public string inputFile { get; set; }

        [StringLength(50)]
        public string nameFile { get; set; }

        [StringLength(50)]
        public string UsuarioCreacion { get; set; }

        public int? destino { get; set; }

        public int? pais { get; set; }

        public int? ciudad { get; set; }

        public int? usaTc { get; set; }

        public DateTime fechadesde { get; set; }

        public DateTime fechahasta { get; set; }

        public int? cantInvitados { get; set; }

        [StringLength(200)]
        public string nivel1 { get; set; }

        [StringLength(200)]
        public string nivel2 { get; set; }

        public DateTime? fechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        [StringLength(200)]
        public string Comentarios { get; set; }
    }
}
