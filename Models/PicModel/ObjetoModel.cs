using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoleEcIntranet.Models.PicModel
{
    public class ObjetoModel
    {
        public string DT_RowId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string position { get; set; }
        public string office { get; set; }
        public string start_date { get; set; }
        public string salary { get; set; }
    }
    public class ObjectModel2
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<ObjetoModel> data { get; set; }
    }
}