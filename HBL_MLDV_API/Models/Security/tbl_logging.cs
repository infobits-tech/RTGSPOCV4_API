using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Security
{
    public class tbl_logging
    {
        public int log_id { get; set; }
        public int doc_typ_sk { get; set; }
        public string script_log { get; set; }

    }
}