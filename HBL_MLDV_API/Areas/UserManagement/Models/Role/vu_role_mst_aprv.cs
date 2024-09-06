using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBL_MLDV_API.DBAttribute;
using System.ComponentModel.DataAnnotations;

namespace HBL_MLDV_API.Areas.UserManagement.Models.Role
{
    public class vu_role_mst_aprv
    {
        public int role_sk { get; set; }
        public string role_desc { get; set; }
        [Skip]
        [Ignore]
        public string status { get; set; }
        public int status_sk { get; set; }
        public int record_status { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        public int row_version { get; set; }
        public int? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        [Skip]
        [Ignore]
        public List<vu_role_lvl_can_do_aprv> cando { get; set; }
        [Skip]
        [Ignore]
        public string sender { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
        [Skip]
        [Ignore]
        public string doc_link { get; set; }
    }
}