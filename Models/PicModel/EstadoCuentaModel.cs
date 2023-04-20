using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class EstadoCuentaModel
    {
        public int IdEstadoCuenta { get; set; }


        public string Identificador { get; set; }

        public DateTime? FechaConsumo { get; set; }


        public string Cedula { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal? Valor { get; set; }

        public int? Corte { get; set; }

        public int? Documento { get; set; }

        public int? NumeroTC { get; set; }


        public DateTime FechaCreacion { get; set; }

        public int Estado { get; set; }
    }
}