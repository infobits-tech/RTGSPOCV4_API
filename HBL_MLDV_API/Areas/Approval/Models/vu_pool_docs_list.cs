using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Approval.Models
{
    public class vu_pool_docs_list
    {
        public int doc_pool_sk { get; set; }
        public int doc_typ_sk { get; set; }
        public int doc_mst_sk { get; set; }
        public string doc_nme { get; set; }
        public string doc_no { get; set; }
        public DateTime doc_dte { get; set; }
        public int pool_sk { get; set; }
        public string pool_nme { get; set; }
        public int aprv_cat_sk { get; set; }//Approval category
        public string cate_desc { get; set; }//Approval category
        public string cat_dtl_desc { get; set; }
        public string doc_link { get; set; }
        public int br_sk { get; set; }
        public string br_desc { get; set; }
        public string approval_status { get; set; }
        public int approval_status_sk { get; set; }
        public string rmks { get; set; }
        public int row_version { get; set; }
        public int created_by { get; set; }
        public string user_ful_name { get; set; }
        public DateTime? create_dt_tme { get; set; }
        public int? updated_by { get; set; }
        public DateTime? update_dt_tme { get; set; }
        public int record_status { get; set; }
        public string Action { get; set; }
        public string state { get; set; }
        public string cat_desc { get; set; }
        public string Forwarded { get; set; }
        public int IsForwarded { get; set; }
        public string Forwarded_name { get; set; }
        public DateTime? Forwarded_datetime { get; set; }
    }
}