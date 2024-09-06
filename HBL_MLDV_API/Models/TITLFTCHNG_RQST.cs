using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class TITLFTCHNG_RQST
    {
        public int RQST_ID { get; set; }
        public int DOC_SK { get; set; }
        public int DOC_TYP_SK { get; set; }
        public string CUST_ACCT_NBR { get; set; }
        public DateTime CRETD_ON { get; set; }
        public int CRETD_BY { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }

    }
}