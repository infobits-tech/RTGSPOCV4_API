using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.UserManagement.Models
{
    public class users
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime? last_login { get; set; }
        [Ignore]
        [Skip]
        public DateTime? updated_on { get; set; }
        public int? updated_by { get; set; }

        public DateTime? created { get; set; }
        public int mt_code { get; set; }
        
        public string user_ful_name { get; set; }
        public string creat_appr_flg { get; set; }
        public string Crtd_by_IP { get; set; }
        public string updtd_by_IP { get; set; }
        public int usr_creatd_id { get; set; }
        public DateTime? del_dte { get; set; }
        public string remarks { get; set; }
        public DateTime? pswrd_expiry_dte { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
        public int row_version { get; set; }
        public int record_status { get; set; }
        public int status_sk { get; set; }
        public string mobile_cntry_cde { get; set; }
        public string mobile_nbr { get; set; }

        public string status { get; set; }
    }
}