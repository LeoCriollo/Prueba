using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class CargasFamiliaresModel
    {
        [DisplayName("COMPAÑIA:")]
        public string nomCia { get; set; }
        [DisplayName("NOMBRE:")]
        public string nomEmpleado { get; set; }
        [DisplayName("AREA:")]
        public string area { get; set; }
        [DisplayName("SEG.MEDICO:")]
        public string segMedico { get; set; }
        [DisplayName("ACTIVO:")]
        public string activo { get; set; }
        public List<CargasFamiliaresDet> detalle { get; set; }
    }


    public class CargasFamiliaresDet
    {
        public string relacion { get; set; }
        public string codigo { get; set; }
        public string nombreCarga { get; set; }
        public string sexo { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? fechaNacimiento { get; set; }
        public string edad { get; set; }
    }
}