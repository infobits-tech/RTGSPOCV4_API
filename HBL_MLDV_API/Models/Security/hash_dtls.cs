using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Security
{
    public class hash_dtls
    {
        [Key]
        public int dtl_row_sk { get; set; }
        public int doc_typ_sk { get; set; }
        public DateTime hash_dt_tme { get; set; }
        public string hash_data { get; set; }
        public int user_id { get; set; }
        public string remote_ip { get; set; }
        public string remote_pc_mac { get; set; }
        public string src { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
    }
}