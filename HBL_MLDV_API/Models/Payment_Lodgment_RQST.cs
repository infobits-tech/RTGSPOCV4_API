using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class Payment_Lodgment_RQST
    {  [Key]
        public int Id { get; set; }
        public int doc_sk { get; set; }
        public string doc_type_sk { get; set; }
        public DateTime Created_DateTime { get; set; }
        public string AccountNo { get; set; }
        public int Unique_identifier_No { get; set; }
        public int Request_No { get; set; }
        public double Amount { get; set; }
    }
}