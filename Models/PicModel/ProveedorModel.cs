using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class ProveedorModel
    {

        [Required]
        [StringLength(13)]
        public string rucModal { get; set; }
        [Required]
        [StringLength(200)]
        public string razonsocialModal { get; set; }
        [Required]
        [StringLength(200)]
        public string TipoProveedor1 { get; set; }

    }
}