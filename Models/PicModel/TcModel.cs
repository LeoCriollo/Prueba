using DoleEcIntranet.Data.DataPIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class EstadCuentaTcViewModel
    {
        public EstadCuentaModel EstadCuenta { get; set; }
        public TcModel Tc { get; set; }
    }

    public class TcModel
    {
        public int Id { get; set; }
        public string Tcambio { get; set; }
        public string valorFactura { get; set; }
        public string slMoneda { get; set; }
        public string tipoconsumo { get; set; }
        public string numfactura { get; set; }
        public string tipofactura { get; set; }
        public string tipoproveedor { get; set; }
        public string fecha { get; set; }
        public string comentario { get; set; }
        public string rucproveedor { get; set; }
        public string razonsocial { get; set; }
        public HttpPostedFileBase archivo { get; set; }
        public byte[] ArchivoBytes { get; set; }
        public string ArchivoString { get; set; }
        public List<InvitadoTc> invitados { get; set; }
        public TcModel()
        {
            invitados = new List<InvitadoTc>();
        }
    }
 

    public class InvitadoTc
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string empresa { get; set; }
        public string cargo { get; set; }
    }
}