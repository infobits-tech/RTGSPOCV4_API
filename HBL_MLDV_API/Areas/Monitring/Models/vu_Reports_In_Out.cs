using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Monitring.Models
{
    public class vu_Reports_In_Out
    {
        public int Report_Id { get; set; }
        public string Report_Names { get; set; }
        public string Report_Title { get; set; }
        public string Report_Filename { get; set; }
        public string Report_Type { get; set; }
        public string DataSetName { get; set; }
    }
}