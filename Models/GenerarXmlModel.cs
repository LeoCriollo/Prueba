using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class GenerarXmlModel
    {
        public string cod_trans { get; set; }
       

        public string Ruc { get; set; }

        //[Required(ErrorMessage = "Debe ingresar una fecha de vencimiento.")]
        //[DisplayName("Fecha")]
        //[DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public string Fecha { get; set; }

        public string autOld { get; set; }
     

        public string autNew { get; set; }
        

        public List<GenerarXmlsDet> listaCias{ get; set; }


    }

    public class GenerarXmlsDet
    {
        public string Cod_Doc { get; set; }
        public string Estab { get; set; }
        public string Pto_Emi { get; set; }
        public string Fin_Old { get; set; }
        public string Ini_New { get; set; }
        public int pe { get; set; }

    }

}