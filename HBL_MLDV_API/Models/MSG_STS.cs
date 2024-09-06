using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class MSG_STS
    {
        public int ID { get; set; }
        public int DOC_ID { get; set; }
        public int DOC_Typ_Sk { get; set; }
        public string SNDER_REF { get; set; }
        public int STS_CDE { get; set; }
        public long DOC_INSTC_ID { get; set; }
        public DateTime STS_DTE_TME { get; set; }
        public string STS_SRC { get; set; }
        public string HASH { get; set; }
        public int IS_INVALID { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }

        public int? txn_hold_sts { get; set; }
    }
    public class MSG_STS_INWARD
    {
        public int ID { get; set; }
        public int DOC_ID { get; set; }
        public int DOC_Typ_Sk { get; set; }
        public string SNDER_REF { get; set; }
        public int STS_CDE { get; set; }
        //public long DOC_INSTC_ID { get; set; }
        public DateTime STS_DTE_TME { get; set; }
        public string STS_SRC { get; set; }
        public int IS_INVALID { get; set; }
        public string HASH { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
    }
    
}