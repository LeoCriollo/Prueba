using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class AnticipoEfectivoModel
    {

        [DisplayName("Fecha Solicitud")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaSolicitud { get; set; }
        public int? codigoempleado { get; set; }
        public string nombre { get; set; }
        public string status { get; set; }
        public string IdAutoridadFinanciera { get; set; }
        
        public string departamento { get; set; }
        public string razonsocial { get; set; }
        public string area   { get; set; }
        public string centrocosto { get; set; }
        public string fechadesde { get; set; }
        public string fechasolicitud { get; set; }
        public string fechahasta { get; set; }
        public int checkDirector { get; set; }
        public int checkIn1 { get; set; }
        public int id { get; set; }

        [DisplayName("Estatus")]
        public string Estatus { get; set; }

        [DisplayName("Datos el empleado")]
        public int DatosEmpleado { get; set; }
        [DisplayName("Centro de costo cargado a:")]
        public string CentroCostoCargado { get; set; }
        public string Secuencial { get; set; }

        [DisplayName("Centro de costo cargado a:")]
        public bool CheckCentroCostoCargado { get; set; }


        [DisplayName("Autoridad Financiera")]
        public string AutoFinanciera { get; set; }

        [DisplayName("Autoridad Financiera")]
        public bool CheckAutoFinanciera { get; set; }

        [DisplayName("Destino")]
        public string Destino { get; set; }

        [DisplayName("Motivo del anticipo de efectivo")]
        public string MotivoAnticipoEfectivo { get; set; }

        [DisplayName("Pais")]
        public string Pais { get; set; }

        [DisplayName("Ciudad")]
        public string Ciudad { get; set; }

        [DisplayName("Desde:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaDesde { get; set; }    
        [DisplayName("Hasta:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaHasta { get; set; }


        [DisplayName("Asistentes")]
        public string Asistentes { get; set; }


        [DisplayName("Usara TC corporativa:")]
        public string UsaraTCcorporativa { get; set; }



        [DisplayName("Aprobacion")]
        public string Aprobacion { get; set; }


        [DisplayName("Comentarios")]
        public string Comentarios { get; set; }



      
        [DisplayName("Nivel1")]
        public string Nivel1 { get; set; }
        [DisplayName("Nivel2")]
        public string Nivel2 { get; set; }
 



    }
}