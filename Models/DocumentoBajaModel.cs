using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models
{
    public class DocumentoBajaModel
    {
        [Required(ErrorMessage = "Debe ingresar un motivo")]
        [DisplayName("Motivo")]
        [StringLength(maximumLength: 100, MinimumLength = 5)]
        public string motivo { get; set; }

        [Required(ErrorMessage = "Debe cargar un documento")]
        [DisplayName("Documento")]
        public HttpPostedFileBase documentFile { get; set; }
    }

    public class Customer
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class RequestWS
    {
        public Customer customer { get; set; }
        public string fileName { get; set; }
        public string fileContent { get; set; }

    }

    public class Response
    {
        public string responseCode { get; set; }
        public string responseContent { get; set; }
        public string ticket { get; set; }

    }


    public class RequestWS2
    {
        public User user { get; set; }
        public string ticket { get; set; }     

    }

    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    public class Response2
    {
        public int codigo { get; set; }
        public string mensaje { get; set; }
        public string statusCode { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
        public string contentFile { get; set; }



    }

}