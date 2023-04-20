using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class FaeViewModel
    {
        public string codigoEmpleado { get; set; }
        public string Id { get; set; }
        public string motivoAnticipo { get; set; }
        public string estado { get; set; }
        public string filename { get; set; }
        public string secuencial { get; set; }
        public string nombre { get; set; }
        public string AutoFinanciera { get; set; }
        public string departamento { get; set; }
        public string destino { get; set; }
        public string razonsocial { get; set; }
        public string centrocosto { get; set; }
        public string CentroCostoCargado { get; set; }
        public string Comentario { get; set; }
        public string MotivoAnticipoEfectivo { get; set; }
        public string AlcanceFae { get; set; }


        public string area { get; set; }
        public string pais { get; set; }
        public HttpPostedFileBase archivoprueba { get; set; }
        public HttpPostedFileBase archivo { get; set; }
        public HttpPostedFileBase fileURL { get; set; }
        public byte[] ArchivoBytes { get; set; }
        public string ArchivoString { get; set; }
        public string ciudad { get; set; }
        public string usaTc { get; set; }
        public string fechadesde { get; set; }

        public string fechahasta { get; set; }
        public string cantInvitados { get; set; }
        public string nivel1 { get; set; }
        public string nivel2 { get; set; }
        public int idFile { get; set; }
        public List<DetalleFae> DetallGasto { get; set; }
        public List<Asist> ArrayAsistente { get; set; }
        public List<FlujoApro> FlujoApro { get; set; }

    }
    //public class FlujoAproFae
    //{
    //    public int? Nivel { get; set; }
    //    public string Estado { get; set; }
    //    public string Aprobador { get; set; }
    //    public DateTime FechaCreacion { get; set; }


    //}

    public class lstCiudades
    {
        public int idPais { get; set; }
        public string Nombre { get; set; }

    }
    public class DetalleFae
    {
        public int id { get; set; }
        public string tipoconsumo { get; set; }
        public string idtipoconsumo { get; set; }

        public string valor { get; set; }
        public string total { get; set; }
        public string descripcionGastos { get; set; }
        public string cantInvitados { get; set; }
 

    }
    public class Asist
    {
        public string nombre { get; set; }
    }
    public class modelIma
    {
        public string idC { get; set; }
        public string form { get; set; }

    }
}