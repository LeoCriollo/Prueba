using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class FilterFlujosModel
    {
        public DateTime? fdesde { get; set; }
        public DateTime? fhasta { get; set; }
        [Required]
        public string Estados { get; set; }
    }
}