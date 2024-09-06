using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.UserManagement.Models
{
    public class vu_user_lvl_can_do_aprv
    {
        [Key]
        [Skip]
        [Ignore]
        public int user_cando_sk { get; set; }
        public int user_sk { get; set; }
        public int activityid { get; set; }
        [Skip]
        [Ignore]
        public int activityparentid { get; set; }
        [Skip]
        [Ignore]
        public string activitytitle { get; set; }
        public int can_view { get; set; }
        public int can_add { get; set; }
        public int can_edit { get; set; }
        public int can_del { get; set; }
        [Skip]
        [Ignore]
        public int can_total { get; set; }
        public int row_version { get; set; }
        public int record_status { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        public int? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        [Skip]
        [Ignore]
        public int role_sk { get; set; }
        [Skip]
        [Ignore]
        public string role_name { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
    }
}