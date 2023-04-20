using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class ViewTCModel
    {
        public string Corte { get; set; }
        public int SumTC { get; set; }
        public List<Tc> Tc { get; set; }
    }
    public class Tc
    {
        public int? Tcredito { get; set; }
        public decimal? SumaValor { get; set; }
         public int SumItem { get; set; }
        public List<Consumo> Consumos { get; set; }
    }
    public class Consumo
    {
        public int Id { get; set; }
        public int Estado{ get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Documento { get; set; }
        public string Valor { get; set; }
        public string Fecha { get; set; }

    }
}