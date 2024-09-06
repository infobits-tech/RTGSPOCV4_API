using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Monitring.Models
{
    public class Vu_rptOutbound_TXN
    {
        public string MSG_TYP { get; set; }

        public string TXN_REF { get; set; }

        public string MT_Ref { get; set; }

        public string BNFCY_CUST_59_1 { get; set; }

        public string BNFCY_CUST_59 { get; set; }

        public string RECVrs_CORSPDT_54A { get; set; }

        public string Amount_PKR_32B { get; set; }

        public string VAL_DTE { get; set; }

        public string ORDing_CUST_50A_2 { get; set; }

        public string ORDing_INSTN_52A { get; set; }

        public string ORDERING_CUSTOMER_AC { get; set; }

        public DateTime? TXN_DTE_TME { get; set; }

        public string Remarks { get; set; }

        public string EMAIL_STATUS { get; set; }

    }

}