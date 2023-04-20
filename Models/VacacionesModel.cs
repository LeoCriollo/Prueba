using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class VacacionesModel
    {
      
        [DisplayName("Subordinado")]
        public string subordinado { get; set; }
        [DisplayName("Puesto")]
        public string puesto { get; set; }
        [DisplayName("Ciclo Laboral")]
        public string cicloLaboral { get; set; }
        [DisplayName("Vac. por Ciclo")]
        public decimal vacXCiclo { get; set; }
        [DisplayName("Vac. Disfrutadas")]
        public decimal vacDisfrutadas { get; set; }
        [DisplayName("Vac. Programadas")]
        public int vacProgramadas { get; set; }
    }





}