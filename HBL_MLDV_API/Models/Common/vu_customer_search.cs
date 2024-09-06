using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Common
{
    public class vu_customer_search
    {
        public int remtr_sk { get; set; }
        public string frst_nme { get; set; }
        public string mid_nme { get; set; }
        public string last_nme { get; set; }
        public string remtr_typ { get; set; }
        public string member_id { get; set; }
        public string doc_br { get; set; }
        public string addr { get; set; }
        public string mobile_cde { get; set; }
        public string mobile_nbr { get; set; }
        public string email_addr { get; set; }
        public string ref_cde { get; set; }
        public string country_desc { get; set; }
        public string img_url { get; set; }
        public string city_desc { get; set; }
        public string custmr_status { get; set; }
        public string custmr_rmrks { get; set; }
        public int country_sk { get; set; }
    }
}