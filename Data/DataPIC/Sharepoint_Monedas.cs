namespace DoleEcIntranet.Data.DataPIC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sharepoint_Monedas
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Moneda { get; set; }

        [Required]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Pais { get; set; }

       public string Descripcion
        {
            get { return Moneda + "-" + Pais; }
        }
    }
}
