using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class Act_logs
    {
        public int TRANS_ID { get; set; }
        public string DOC_TYP { get; set; }
        public string DOC_MST_SK { get; set; }
        public string DOC_DTL_SK { get; set; }
        public string CNTLR { get; set; }
        public string FCN_NME { get; set; }
        public string PREV_REC { get; set; }
        public int mtr_dtl { get; set; }
        //public string RSPNS_STS_CDE { get; set; }
        //public string RSPNS_RSLT { get; set; }
        public DateTime CRETD_DTTM { get; set; }
        public int CRETD_BY { get; set; }
        //[Skip]
        //[Ignore]
        //public bool Iseditable { get; set; }
        //[Skip]
        //[Ignore]
        //public string state { get; set; }
    }
}