using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class MyReportModel
    {
        public int indice { get; set; }
        public int id { get; set; }
        [Display(Name = "Motivos del gasto")]
        public string motivogasto { get; set; }
        [Display(Name = "Secuencial")]
        public string secuencial { get; set; }
        public string formulario { get; set; }
        [Display(Name = "Tipo reporte")]
        public string slctTipo { get; set; }
        public string nivelaprob { get; set; }
        [Display(Name = "Fecha de solicitud")]
        public string fechasolicitud { get; set; }
        [Display(Name = "Estado")]
        public string estado { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
    }
}