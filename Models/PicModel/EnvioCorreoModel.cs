using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class EnvioCorreoModel
    {
        public int Id { get; set; }
        public string NombreSolicitante { get; set; }

        public string CorreoSolicitante { get; set; }
        public string Formulario { get; set; }
        public int secuen { get; set; }
        public string CorreoAprobador { get; set; }

        public string Link { get; set; }

        public string NombreAprobador { get; set; }

        public string TipoReporte { get; set; }
    }
}