using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Security
{
    public class login_history
    {
        public int lgn_hst_sk { get; set; }

        public int usr_sk { get; set; }
        public int cntr { get; set; }
        public string dynmc_url { get; set; }

        public int row_version { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        public DateTime Login_DTE_TME { get; set; }
        public DateTime? Logout_DTE_TME { get; set; }
    }
}