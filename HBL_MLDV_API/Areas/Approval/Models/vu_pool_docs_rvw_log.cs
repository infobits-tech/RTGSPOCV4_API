using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Approval.Models
{
    public class vu_pool_docs_rvw_log
    {
        public int log_mst_sk { get; set; }
        public int doc_typ_sk { get; set; }
        public int doc_mst_sk { get; set; }
        public int pool_doc_sk { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
    }
}