using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Approval.Models
{
    public class pools
    {
        public int pool_sk { get; set; }
        public string pool_nme { get; set; }
        public string pool_desc { get; set; }
        public string aprv_act { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        public int? updated_by { get; set; }
        public DateTime? update_dt_tme { get; set; }
        [Ignore]
        [Skip]
        public int record_status { get; set; }
        [Ignore]
        [Skip]
        public string Action { get; set; }
        [Ignore]
        [Skip]
        public string Users { get; set; }
        [Ignore]
        [Skip]
        public string branches { get; set; }
        [Ignore]
        [Skip]
        public int min_amt { get; set; }
        [Ignore]
        [Skip]
        public int max_amt { get; set; }
        [Ignore]
        [Skip]
        public string categories { get; set; }
        [Ignore]
        [Skip]
        public List<pool_users_map> lst_users { get; set; }
        [Ignore]
        [Skip]
        public List<pool_cat_map> lst_cat { get; set; }
        [Ignore]
        [Skip]
        public List<pool_branch_map> lst_branch { get; set; }
        [Ignore]
        [Skip]
        public List<pool_amt_map> amt { get; set; }
        [Ignore]
        [Skip]
        public string is_inactive_s { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        [Ignore]
        [Skip]
        public int row_version { get; set; }
    }
}