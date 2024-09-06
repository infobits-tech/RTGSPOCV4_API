using HBL_MLDV_API.Areas.Approval.Models;
using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Setup.Models
{
    public class Pools
    {
        public int pool_sk { get; set; }
        public string pool_nme { get; set; }
        public string pool_desc { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        public int? updated_by { get; set; }
        public DateTime? update_dt_tme { get; set; }
        public int record_status { get; set; }
        [Ignore]
        [Skip]
        public string Users { get; set; }
        [Ignore]
        [Skip]
        public string branches { get; set; }
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
        public int param { get; set; }

    }
    public class pool_vu
    {
        public int pool_sk { get; set; }
        public string pool_nme { get; set; }
        public string pool_desc { get; set; }

        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        public int? updated_by { get; set; }
        public DateTime? update_dt_tme { get; set; }
        public int record_status { get; set; }

    }
    public class Vu_Pools
    {
        public int pool_sk { get; set; }
        public string pool_nme { get; set; }
        public string pool_desc { get; set; }

        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime? create_dt_tme { get; set; }
        public int? updated_by { get; set; }
        public DateTime? update_dt_tme { get; set; }
        public int record_status { get; set; }


    }
}