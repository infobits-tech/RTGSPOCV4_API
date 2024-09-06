using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.DBAttribute;

namespace HBL_MLDV_API.Areas.UserManagement.Models.Role
{
    public class vu_role_lvl_can_do
    {
        [Key]
        [Skip]
        [Ignore]
        public int role_cando_sk { get; set; }
        public int activityid { get; set; }
        public int can_view { get; set; }
        public int can_add { get; set; }
        public int can_edit { get; set; }
        public int can_del { get; set; }
        public int role_sk { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        public int record_status { get; set; }
        public int row_version { get; set; }
        public int? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
    }
}