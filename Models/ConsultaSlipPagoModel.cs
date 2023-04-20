using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class ConsultaSlipPagoModel
    {
    }


    public class FilterSlipPago
    {
        [Required]
        public int tipoBusqueda { get; set; }
        [Required]
        [DisplayName("Criterio")]
        public string busqueda { get; set; }
    }


    public class ResponseBusquedaEmpleado
    {        
        [DisplayName("Código Empleado")]
        public int codigoEmpleado { get; set; }
        [DisplayName("Nombre Empleaado")]
        public string nombreEmpleado { get; set; }
    }


}