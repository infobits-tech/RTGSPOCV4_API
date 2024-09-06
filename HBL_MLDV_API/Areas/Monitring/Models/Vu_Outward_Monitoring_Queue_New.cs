using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Monitring.Models
{
    public class Vu_Outward_Monitoring_Queue_New
    {
        public int DOC_SK { get; set; }
        public string MTRef { get; set; }
        public string Refno { get; set; }
        public string MsgType { get; set; }
        public string ValDate { get; set; }
        public string Amount { get; set; }
        public string OrderingCustomer { get; set; }
        public string BeneficaryName { get; set; }
        public string BeneficiaryA_C { get; set; }
        public string BeneficiaryBank { get; set; }
        public string time_of_lodgement { get; set; }
        public string msg_time { get; set; }
        public string Status { get; set; }
        public string Remit_INFO { get; set; }
        public string Remarks { get; set; }
    }
}