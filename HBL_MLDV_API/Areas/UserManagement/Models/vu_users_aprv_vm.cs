using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.Areas.UserManagement.Models.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace HBL_MLDV_API.Areas.UserManagement.Models
{
    public class vu_users_aprv_vm
    {
        
        public long user_sk { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int[] role_sk { get; set; }
        public DateTime? last_login { get; set; }
        public DateTime? updated_on { get; set; }
        public int? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public int mt_code { get; set; }
        public string user_full_name { get; set; }
        public string creat_appr_flg { get; set; }
        public string Crtd_by_IP { get; set; }
        public string updtd_by_IP { get; set; }
        public int created_by { get; set; }
        public DateTime? del_dte { get; set; }
        public string remarks { get; set; }
        public DateTime? pswrd_expiry_dte { get; set; }
        public string state { get; set; }
        public int row_version { get; set; }
        public int record_status { get; set; }
        public string mobile_cntry_cde { get; set; }
        public string mobile_nbr { get; set; }
        public int[] br_code { get; set; }
        public int status_sk { get; set; }
        public string sender { get; set; }
        public string status { get; set; }
        public List<vu_user_lvl_can_do_aprv> cando { get; set; }
        public List<vu_role_lvl_can_do_aprv> role { get; set; }
        [Ignore]
        [Skip]
        public string doc_link { get; set; }
    }
    
}