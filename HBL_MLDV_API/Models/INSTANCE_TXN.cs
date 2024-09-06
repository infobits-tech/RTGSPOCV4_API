using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class INSTANCE_TXN
    {
        public int id { get; set; }
        public long DOC_INSTC_ID { get; set; }
        public int DOC_SK { get; set; }
        public int DOC_TYP_SK { get; set; }
        public string SNDER_REF { get; set; }
        public DateTime INSTC_DTE_TME { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
    }
}