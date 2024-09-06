using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.UserManagement.Models
{
    public class vu_user_br_mapping_aprv
    {
        [Key]
        [Skip]
        [Ignore]
        public int mapping_sk { get; set; }
        public int br_code { get; set; }
        public int user_sk { get; set; }
        public bool is_default { get; set; }
        public int record_status { get; set; }
        public int row_version { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        public int updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
    }
}