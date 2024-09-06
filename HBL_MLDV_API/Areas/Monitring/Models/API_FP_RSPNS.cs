using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Monitring.Models
{
    public class API_FP_RSPNS
    {
        public int RESP_ID { get; set; }
        public int DOC_SK { get; set; }
        public string DOC_TYP_SK { get; set; }
        public DateTime CRETD_DTTM { get; set; }
        public string RESP_DESC { get; set; }
        public string RESP_CDE { get; set; }
        public string RESP_MSG { get; set; }
        public Int64? INFB_ID { get; set; }
        public int? RQST_ID { get; set; }
    }
}