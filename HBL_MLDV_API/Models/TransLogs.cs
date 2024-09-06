using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class TransLogs
    {
        [Key]
        public int Id { get; set; }
        public string doc_type { get; set; }

        public string doc_mst_sk { get; set; }

        public string Controller { get; set; }

        public DateTime Created_DateTime { get; set; }

        public string functionName { get; set; }

        public string Rspns_Status_code { get; set; }

        public string rspns_result { get; set; }
        [Ignore]
        public string state { get; set; }


    }
}