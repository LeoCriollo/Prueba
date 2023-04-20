using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class RpgAllModel
    {
        public int Id { get; set; }

        public int? nivel { get; set; }
        public string codigoEmpleado { get; set; }
        public string comentario { get; set; }
        public string secuencial { get; set; }
        public string estado { get; set; }
        public string nombre { get; set; }
        public string status { get; set; }
        public string departamento { get; set; }
        public string razonsocial { get; set; }
        public string centrocosto { get; set; }
        public string fechadesde { get; set; }
        public string fechasolicitud { get; set; }
        public string fechahasta { get; set; }
        public int checkDirector { get; set; }
        public int checkIn1 { get; set; }


        public string Nivel1 { get; set; }
        public bool aprobador { get; set; }
        public string Nivel2 { get; set; }
        public string Nivel3 { get; set; }
        public string area { get; set; }
        public string centrocostocargadoa { get; set; }
        public string autoridadFinanciera { get; set; }

        public string director { get; set; }


        public string motivogasto { get; set; }
        public string destino { get; set; }
        public string slctTipo { get; set; }

        public List<ConsumoViewModel2> DetallConsumo { get; set; }
        public List<File> Img { get; set; }
        public List<FlujoApro> FlujoApro { get; set;  }
       
    }


    public class File
    {

        public int Id { get; set; }
        public int IdCab { get; set; }
        public string file { get; set; }
        public string name { get; set; }
        public string extension { get; set; }

    }

    public class FlujoApro
    {
        public int? Nivel { get; set; }
        public string Estado { get; set; }
        public string Aprobador { get; set; }
        public DateTime FechaCreacion { get; set; }


    }
        public class ConsumoViewModel2
    {
        public int id { get; set; }
        public int idFile { get; set; }
        public int idDetFile { get; set; }
        public string tipoconsumo { get; set; }
        public string moneda { get; set; }
        public string Archive { get; set; }
        public string namefile { get; set; }
        public string file { get; set; }
        public string inputfile { get; set; }
        public int? idtipoconsumo { get; set; }
        public int? idmoneda { get; set; }
        public string destino { get; set; }
        public string tcambio { get; set; }
        public string monto { get; set; }
        public string valor { get; set; }
        public string destinoD { get; set; }
        public string invitados { get; set; }
        public string tipoproveedor { get; set; }
        public int? idtipoproveedor { get; set; }
        public string rucproveedor { get; set; }
        public string razonsocial { get; set; }
        public string tipofactura { get; set; }
        public string idtipofactura { get; set; }
        public string numerofactura { get; set; }
        public string fechafactura { get; set; }
        public string descripcionGastos { get; set; }
        public string AtencionNegocios { get; set; }
        public List<Invitadoss> ArrayInvitados { get; set; }
        //public List<Filess> ArrayFiles { get; set; }
    
        public string filename { get; set; }
        public int IdFile { get; set; }

    }
    public class Invitadoss
    {

        public int Id { get; set; }
        public string nombre { get; set; }
        public string cargo { get; set; }
        public string empresa { get; set; }

    }
    public class Filess
    {

        public int Id { get; set; }

        public int? IdDet { get; set; }

        public int IdCab { get; set; }

    
        public string Archive { get; set; }

        
        public string ContentType { get; set; }

        public string NameFile { get; set; }

        public string Extension { get; set; }

    }

    public class ActualizarAF
    {
        public string ids { get; set; }
        public string idGerente { get; set; }
        public string idDirector { get; set; }

    }
}