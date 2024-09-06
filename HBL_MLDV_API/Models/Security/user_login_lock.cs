using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Security
{
    public class user_login_lock
    {
        public int lgn_lck_sk { get; set; }
        public int usr_sk { get; set; }
        public DateTime lck_dt_tm { get; set; }

        public int row_version { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
    }
}