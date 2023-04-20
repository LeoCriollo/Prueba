using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class AutoridadModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Usuario")]
        public string NombreTrab { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Área")]
        public string AreaDesc { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Cargo")]
        public string Cargo { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Código")]
        public string CodTrabajador { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Compania")]
        public string NombreCia { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Puesto")]
        public string NombrePuesto { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Departamento")]
        public string Depto_Desc { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Centro de costo")]
        public string Centr_costo { get; set; }

        [Required]
        [StringLength(20)]
        [DisplayName("Código compania")]
        public string CodigoCia { get; set; }
    }
}