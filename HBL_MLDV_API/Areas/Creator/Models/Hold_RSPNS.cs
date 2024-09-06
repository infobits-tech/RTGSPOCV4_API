using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class Hold_RSPNS
    {
        public int RSPNS_ID { get; set; }

        public int RQST_ID { get; set; }

        public int DOC_SK { get; set; }

        public int DOC_TYP_SK { get; set; }
        [Skip]
        [Ignore]
        public string message { get; set; }

        //public string ACCOUNTNO { get; set; }

        //public int TRXAMOUNT { get; set; }

        public string REPCODE { get; set; }

        public string REPMESSAGE { get; set; }

        public int REPHOLDCODE { get; set; }

        public string UNQREFERENCE1 { get; set; }

        public string API_STS_CD { get; set; }

        public string API_STS_DESC { get; set; }

        public DateTime CRETD_ON { get; set; }

        public int CRETD_BY { get; set; }

        public string Stream { get; set; }

        public string API_Sts { get; set; }

        public string Api_type { get; set; }

        [Ignore]
        [Skip]
        public Data Data { get; set; }

        [Ignore]
        [Skip]
        public string state { get; set; }

        [Ignore]
        [Skip]
        public Error error { get; set; }
    }
    public class Data
    {
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public int repHoldCode { get; set; }
        public string uniqueReferenceId { get; set; }
    }
    public class DeveloperMessage
    {
        public string uniqueReferenceId { get; set; }
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public int repHoldCode { get; set; }
    }

    public class Error
    {
        public string reqId { get; set; }
        public string code { get; set; }
        public string type { get; set; }
        public DeveloperMessage developerMessage { get; set; }
    }
}