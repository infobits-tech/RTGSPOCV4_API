using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Approval.Models
{
    public class vu_pool_docs
    {
        public int doc_pool_sk { get; set; }
        public int doc_typ_sk { get; set; }
        public int doc_mst_sk { get; set; }
        public string doc_no { get; set; }
        public DateTime doc_dte { get; set; }
        public int aprv_cat_sk { get; set; }
        [Ignore]
        [Skip]
        public int pool_sk { get; set; }
        public string doc_link { get; set; }
        public int br_sk { get; set; }
        public string approval_status { get; set; }
        public int approval_status_sk { get; set; }
        public string rmks { get; set; }

        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int? updated_by { get; set; }
        [Ignore]
        [Skip]
        public DateTime? update_dt_tme { get; set; }

        public int record_status { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        [Ignore]
        [Skip]
        public string Pool_doc_sk_arr { get; set; }
        public string recallReason { get; set; }
        public decimal txn_amt { get; set; }
    }
    public class vu_pool_docs_forward
    {
        public int doc_pool_sk { get; set; }
        public int doc_typ_sk { get; set; }
        public int doc_mst_sk { get; set; }
        public string doc_no { get; set; }
        public DateTime doc_dte { get; set; }
        public int aprv_cat_sk { get; set; }
        public int pool_sk { get; set; }
        public string doc_link { get; set; }
        public int br_sk { get; set; }
        public string approval_status { get; set; }
        public int approval_status_sk { get; set; }
        public string rmks { get; set; }
        [Ignore]
        [Skip]
        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int? updated_by { get; set; }
        [Ignore]
        [Skip]
        public DateTime? update_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int record_status { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        public string recallReason { get; set; }
    }
    public class vu_pool_docs_disabled
    {
        public int doc_pool_sk { get; set; }
        public int doc_typ_sk { get; set; }
        public int doc_mst_sk { get; set; }
        public string doc_no { get; set; }
        public DateTime doc_dte { get; set; }
        public int aprv_cat_sk { get; set; }
        public int pool_sk { get; set; }
        public string doc_link { get; set; }
        public int br_sk { get; set; }
        public string approval_status { get; set; }
        public int approval_status_sk { get; set; }
        public string rmks { get; set; }
        [Ignore]
        [Skip]
        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int? updated_by { get; set; }
        [Ignore]
        [Skip]
        public DateTime? update_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int record_status { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        public string recallReason { get; set; }
    }
    public class vu_pool_docs_history
    {
        public int doc_pool_sk { get; set; }
        public int doc_typ_sk { get; set; }
        public int doc_mst_sk { get; set; }
        public string doc_no { get; set; }
        public DateTime doc_dte { get; set; }
        public int aprv_cat_sk { get; set; }
        public int pool_sk { get; set; }
        public string doc_link { get; set; }
        public int br_sk { get; set; }
        public string approval_status { get; set; }
        public int approval_status_sk { get; set; }
        public string rmks { get; set; }
        [Ignore]
        [Skip]
        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int? updated_by { get; set; }
        [Ignore]
        [Skip]
        public DateTime? update_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int record_status { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }

        public int scrn_rslt_sk { get; set; }
        public string recallReason { get; set; }
        public decimal txn_amt { get; set; }
    }
}