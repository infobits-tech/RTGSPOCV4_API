using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class GET_RJCT_TXN
    {
        public string STS_CDE { get; set; }
        public string Outward_Senders_Ref { get; set; }
        public int DOC_SK { get; set; }
        public string Cbuae_MSG_TYP { get; set; }
        public string SNDers_REF { get; set; }
        public string VAL_DTE { get; set; }
        public string AMT_32A { get; set; }
        public string sts_desc { get; set; }

    }
}