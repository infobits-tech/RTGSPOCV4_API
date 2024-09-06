using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.UserManagement.Models
{
    public class vu_user_role_mapping_aprv
    {
        [Key]
        [Skip]
        [Ignore]
        public int usr_rol_mapping_sk { get; set; }
        public int user_sk { get; set; }
        public int role_sk { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
        public int row_version { get; set; }
    }
}