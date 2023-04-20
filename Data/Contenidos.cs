namespace DoleEcIntranet.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contenidos
    {
        public int Id { get; set; }

        public int IdCategoria { get; set; }

        public int IdEstado { get; set; }

        [Required]
        [StringLength(500)]
        public string TituloArticulo { get; set; }

        [Required]
        [StringLength(800)]
        public string Contenido1 { get; set; }

        [Required]
        public string Contenido2 { get; set; }

        [Column(TypeName = "image")]
        public byte[] ContenidoImg { get; set; }

        public long Orden { get; set; }

        public DateTime? FechaPublicacion { get; set; }

        public DateTime? FechaExpiracion { get; set; }

        [Required]
        [StringLength(50)]
        public string UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        [StringLength(50)]
        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Column(TypeName = "image")]
        public byte[] ContenidoImg1 { get; set; }

        public virtual Categorias Categorias { get; set; }

        public virtual Estados Estados { get; set; }
    }
}
