using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class GastosPersonalesModel
    {
        public int Id { get; set; }

        public int IdUser { get; set; }

        [DisplayName("Código Empleado")]
        public int CodigoEmpleado { get; set; }

        public int Secuencia { get; set; }

        [DisplayName("Año")]
        public int Year { get; set; }
        
        [StringLength(13)]
        [DisplayName("Cedula")]
        public string Cedula { get; set; }

        [DisplayName("Fecha")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; }

       
        [StringLength(100)]
        [DisplayName("Nombres Apellidos")]
        public string NombresApellidos { get; set; }

        [Required(ErrorMessage ="Valor Requerido")]
        [DisplayName("103")]
        //[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        public decimal C_103 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("104")]
        public decimal C_104 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("105")]
        [Range(typeof(decimal), "1", "100000000", ErrorMessage = "Valor incorrecto")]
        public decimal C_105 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("106")]
        public decimal C_106 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("107")]
        public decimal C_107 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("108")]
        public decimal C_108 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("109")]
        public decimal C_109 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("110")]
        public decimal C_110 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("111")]
        public decimal C_111 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("112")]
        [Range(typeof(decimal), "1", "100000000",ErrorMessage ="Valor incorrecto")]
        public decimal C_112 { get; set; }

        [Required(ErrorMessage = "Valor Requerido")]
        [DisplayName("113")]
        public decimal C_113 { get; set; }


        [StringLength(13)]
        [DisplayName("Ruc")]
        public string RucEmpleador { get; set; }

        
        [StringLength(80)]
        [DisplayName("Razon Social")]
        public string RazonSocial { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int UsuarioCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public int? UsuarioActualizacion { get; set; }

        
        [StringLength(1)]
        public string Estado { get; set; }

        public string classReadOnly { get; set; }

        public string endPeriod { get; set; }
        public string topeG { get; set; }

    }


    public class GastosPersonalesResponse1
    {
        public decimal fraccionBasica { get; set; }
        public decimal topeGp { get; set; }
    }


    public class GastosPersonalesResponse2
    {
        public string codCia { get; set; }
        public string ruc { get; set; }
        public string nomCia { get; set; }
        public string nombre { get; set; }
        public string cedula { get; set; }
        public decimal topAnual { get; set; }
        public DateTime fechaIngreso { get; set; }
    }

}