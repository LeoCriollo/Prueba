using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class ReporteGastosModel
    {

        [DisplayName("Fecha Solicitud")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string FechaSolicitud { get; set; }
 
        [DisplayName("Estatus")]
        public string Estatus { get; set; }

        [DisplayName("Datos el empleado")]
        public int DatosEmpleado { get; set; }
        [DisplayName("Centro de costo cargado a:")]
        public string CentroCostoCargado { get; set; }

        [DisplayName("Centro de costo cargado a:")]
        public bool CheckCentroCostoCargado { get; set; }


        [DisplayName("Autoridad Financiera")]
        public string AutoFinanciera { get; set; }

        [DisplayName("Autoridad Financiera")]
        public bool CheckAutoFinanciera { get; set; }

        [DisplayName("Destino")]
        public string Destino{ get; set; }

        [DisplayName("Tipo")]
        public string Tipo { get; set; }

        [DisplayName("Motivo del Gasto")]
        public string MotivoGasto { get; set; }

        [DisplayName("Desde:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaDesde { get; set; }
        [DisplayName("Hasta:")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaHasta { get; set; }
        [DisplayName("Consumo Nacionales")]
        public string ConsumosNacionales { get; set; }
        [DisplayName("Consumo Exterior")]
        public string ConsumosExterior { get; set; }
        [DisplayName("Consumo Combustible")]
        public string ConsumosCombustible { get; set; }
        [DisplayName("Servicios Financieros")]
        public string ConsumosServicioFinanciero { get; set; }

        [DisplayName("Valor Reportado")]
        public string ValorReportado { get; set; }
        [DisplayName("Saldo a Favor /(en contra)")]
        public string SaldoFavorContra { get; set; }
        [DisplayName("Comentarios")]
        public string Comentario { get; set; }
        [DisplayName("Nivel1")]
        public string Nivel1 { get; set; }
        [DisplayName("Nivel2")]
        public string Nivel2 { get; set; }
        [DisplayName("Nivel3")]
        public string Nivel3 { get; set; }
        public string secuencial { get; set; }



    }

    //public class Invitados
    //{
       
    //    public string Id { get; set; }
    //    public string IdReportGasto { get; set; }
    //    public string Nombre { get; set; }
    //    public string Empresa { get; set; }
    //    public string Cargo { get; set; }
    //}

}