using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class ObjetoViewModel
    {

        public string codigoEmpleado { get; set; }
        public string motivogasto { get; set; }
 
        public string slctTipo { get; set; }
        public string secuencial { get; set; }
        public string secuenExterno { get; set; }


        public string Nivel1 { get; set; }
        public string Nivel2 { get; set; }
        public string Nivel3 { get; set; }

        public string IdAutoridadFinanciera { get; set; }
        public string IdDirector { get; set; }
        public string CodCentroCostoA { get; set; }
        public string CentroCosto { get; set; }
        public string fechadesde { get; set; }
        public string fechahasta { get; set; }
        public string estado { get; set; }
        public List<ConsumoViewModel> DetallConsumo { get; set; }
       
    }

}