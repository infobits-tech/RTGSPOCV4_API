using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class Hold_RQST
    {
        public int RQST_ID { get; set; }

        public int DOC_SK { get; set; }

        public int DOC_TYP_SK { get; set; }

        public DateTime CRETD_ON { get; set; }

        public int CRETD_BY { get; set; }

        public string UNQREFERENCE { get; set; }

        public string ACCOUNTNO { get; set; }

        public string TRXAMOUNT { get; set; }

        public string Api_type { get; set; }

        public string HOLDCODE { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
    }
}