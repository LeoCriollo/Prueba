using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.Interfaces
{
    public class InterfaceTransaccion
    {
               
        int Secuencial { get; set; }

        public string Nivel1 { get; set; }

     
        public string Nivel2 { get; set; }


        public string Nivel3 { get; set; }

        public string Estado { get; set; }
    }

}