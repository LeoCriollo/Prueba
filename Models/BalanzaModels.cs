using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class BalanzaModels
    {

    }

    public class TipoEmpaqueModels
    {

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(200)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(20)]
        public string Pais { get; set; }

        [Required]
        public decimal PesoLibraIni { get; set; }

        [Required]
        public decimal PesoLibraFin { get; set; }

        public decimal? PesoCaja { get; set; }

    }

    public class ConsultaCupo{

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar una fecha válida.")]
        public DateTime fechaConsulta { get; set; }
        public int idFincaEmpacadora { get; set; }
    }

    public class DataConsultaCupo
    {
        public string finca { get; set; }
        public string tipoEmpaque { get; set; }
        public int cajasProcesadas { get; set; }
        public int cupo { get; set; }
    }
}