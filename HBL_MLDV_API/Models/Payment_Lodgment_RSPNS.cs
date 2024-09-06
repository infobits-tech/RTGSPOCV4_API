using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class Payment_Lodgment_RSPNS
    {
        public int Id { get; set; }
        public int doc_sk { get; set; }
        public string doc_type_sk { get; set; }
        public DateTime Created_DateTime { get; set; }
        public string Payment_Lodgment_RefNo { get; set; }
        public string Reponse_Status { get; set; }
        public string Reponse_Code { get; set; }
        public string Reponse_Message { get; set; }
        public bool Api_Status { get; set; }
        public int req_Id { get; set; }
    }
}