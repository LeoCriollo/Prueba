using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class ModelFile
    {
        public int Id { get; set; }

        public int IdCab { get; set; }

      
        public byte[] Archive { get; set; }

     
        public string ContentType { get; set; }

     
        public string NameFile { get; set; }

     
        public string Extension { get; set; }
      
    }
}