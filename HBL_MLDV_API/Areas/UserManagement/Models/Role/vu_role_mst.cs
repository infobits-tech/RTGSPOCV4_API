using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBL_MLDV_API.DBAttribute;

namespace HBL_MLDV_API.Areas.UserManagement.Models.Role
{
    public class vu_role_mst
    {
        public int role_sk { get; set; }
        public string role_desc { get; set; }
        public int status_sk { get; set; }
        public int record_status { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        public int row_version { get; set; }
        public int? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
    }
}