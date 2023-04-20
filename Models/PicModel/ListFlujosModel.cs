using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class ListFlujosModel
    {
        public int Id { get; set; }
        [Display(Name = "Secuencial")]
        public string secuencial { get; set; }
        public string sltipo { get; set; }
        [Display(Name = "Motivo")]
        public string motivogasto { get; set; }
        [Display(Name = "Departamento")]
        public string area { get; set; }
        [Display(Name = "Estado")]
        public string estado { get; set; }
        [Display(Name = "Compañía")]
        public string cia { get; set; }
        [Display(Name = "Codigo")]
        public string codigoEmpleado { get; set; }
        [Display(Name = "Empleado")]
        public string empleado { get; set; }
        [Display(Name = "Fecha Solicitud")]
        public string fechasolicitud { get; set; }
        [Display(Name = "Destino")]
        public string destino { get; set; }

        [Display(Name = "Comentario")]
        public string comentario { get; set; }

        public string form { get; set; }
    }
}