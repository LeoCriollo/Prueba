using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class FichaPersonalViewModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Id Personal")]
        public string IdPersonal { get; set; }

        [DisplayName("Nombre")]
        public string Nombre { get; set; }

        [DisplayName("Empresa")]
        public string Empresa { get; set; }

        [DisplayName("Zona Geografica")]
        public string ZonaGeo { get; set; }

        [DisplayName("Area")]
        public string Area { get; set; }

        [DisplayName("Cargo")]
        public string Cargo { get; set; }

        [DisplayName("Jefe Inmediato")]
        public string JefeInmediato { get; set; }

        [DisplayName("Fecha de Ingreso")]
        public DateTime? FechaDeIngreso { get; set; }

        [DisplayName("Fecha de Antiguedad")]
        public DateTime? FechaDeAntiguedad { get; set; }

        [DisplayName("Grupo de Nomina")]
        public string GrupoDeNomina { get; set; }

        [DisplayName("Centro de Costo")]
        public string CentroDeCosto { get; set; }



    }


    public class ColaboradoresIngresoMes
    {
        public string codEmpleado { get; set; }
        
        [DisplayName("Trabajador")]
        public string trabajador { get; set; }


        [DisplayName("Fecha ingreso a Dole")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime fechaIngreso { get; set; }

        [DisplayName("Fecha inicio en el cargo")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime fechaInicio { get; set; }


        [DisplayName("Compañia")]
        public string nombreCia { get; set; }
    }
}