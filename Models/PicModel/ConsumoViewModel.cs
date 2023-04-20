using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class ConsumoViewModel
    {
        
        public string Archive { get; set; }
    
        public List<Invitadoss> ArrayInvitados { get; set; }

        public string AtencionNegocios { get; set; }
        public string descripcionGastos { get; set; }

        public string destino { get; set; }
        public string destinoD { get; set; }

    
        public string fechafactura { get; set; }
        public int idFile { get; set; }
        public int id { get; set; }



        public string idmoneda { get; set; }

        public string idtipoconsumo { get; set; }

        public string idtipofactura { get; set; }

        public string idtipoproveedor { get; set; }

        public string inputfile { get; set; }

        public string invitados { get; set; }
        public string moneda { get; set; }
        public string monto { get; set; }

        public string namefile { get; set; }

        public string numerofactura { get; set; }

        public string razonsocial { get; set; }
        public string rucproveedor { get; set; }
        public string tcambio { get; set; }

        public string tipoconsumo { get; set; }

        public string tipofactura { get; set; }
        public string tipoproveedor { get; set; }

        public string valor { get; set; }
        public HttpPostedFileBase archivoprueba { get; set; }
        public HttpPostedFileBase archivo { get; set; }
        public HttpPostedFileBase fileURL { get; set; }
        public byte[] ArchivoBytes { get; set; }
        public string ArchivoString { get; set; }

    }

    public class OpcionesModel
    {
        public int Id { get; set; }
        public string Desc { get; set; }
    }
    public class Invitadoss{

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }

    }
    public class Filess
    {

        public int Id { get; set; }

        public int? IdDet { get; set; }

        public int IdCab { get; set; }


        //public List<entero> Archive { get; set; }


        public string ContentType { get; set; }

        public string NameFile { get; set; }

        public string Extension { get; set; }

    }
    public class entero
    {
        public List<int> a { get; set; }
    }
}