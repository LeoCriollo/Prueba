using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Tools
{
    
    public class Enum
    {
        
        public enum Categorias
        {
            Noticias_Dole = 1,
            Noticias_Externas = 2,
            Noticias_Dale = 3,
            Reclutamiento_Interno = 4

        }
              

        public enum Estados
        {
            Creado = 1,
            Publicado = 2

        }

        public enum Transacciones
        {
            Debitos_1 = 1,
            Creditos_1 = 2,
            Debitos_2 = 4
        }

        public enum GastosGenralesSecuencias
        {
            Primera = 1,
            Segunda = 2
        }

        //claims
        public const string ClaimCodigoEmpleado = "CodigoEmpleado";
        public const string ClaimNombreEmpleado = "NombreEmpleado";


        //roles
        public const string RolAdmin = "ADMIN";
        public const string RolUser = "USERNORMAL";
        public const string RolAdminSlipPago = "ADMINSLIPPAGOVIP";
        public const string RolAdminSlipPagoPuerto = "ADMINSLIPPAGOPUERTO";
        public const string RolAdminContenido = "ADMINCONTENIDO";
        public const string RolFactPeru = "FACTPERU";
        public const string RolEnvioCorreo = "ENVIOCORREOS";
        public const string RolReportesNaportec = "REPORTNAPORTEC";
        public const string RolAdminBalanaza = "ADMINBALANZA";
        public const string ReportBalanza = "REPORTBALANZA";
        public const string ReporteCompras = "REPORTECOMPRAS";



        //Constante de Aritulos a mostrar por seccion (evento)
        public const int articulosporSeccion = 5;





    }
}