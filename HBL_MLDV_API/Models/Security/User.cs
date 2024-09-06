using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.Areas.UserManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using HBL_MLDV_API.DBAttribute;

namespace HBL_MLDV_API.Models.Security
{
    public class user
    {
        public long user_sk { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int role_id { get; set; }
        public DateTime? last_login { get; set; }
        public DateTime? last_update { get; set; }
        public DateTime? created { get; set; }
        public int mt_code { get; set; }
        public string user_ful_name { get; set; }
        public string creat_appr_flg { get; set; }
        public int usr_creatd_id { get; set; }
        public DateTime? del_dte { get; set; }
        public string remarks { get; set; }
        public DateTime? pswrd_expiry_dte { get; set; }
        public int row_version { get; set; }
        public int record_status { get; set; }
        public int status_sk { get; set; }
        public string sender { get; set; }
        public string mobile_cntry_cde { get; set; }
        [Display(Name = "Mobile No.")]
        public string mobile_nbr { get; set; }
        [IgnoreAttribute]
        [SkipAttribute]
        public string state { get; set; }
        [IgnoreAttribute]
        [SkipAttribute]
        public int br_code { get; set; }
        [IgnoreAttribute]
        [SkipAttribute]
        public string br_desc { get; set; }
        public string status { get; set; }
        [Skip]
        [Ignore]
        public bool is_login { get; set; }
    }

    public class vu_users
    {
        public int user_sk { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime? last_login { get; set; }
        public DateTime? updated_on { get; set; }
        public int? updated_by { get; set; }
        public DateTime? created_on { get; set; }
        public int mt_code { get; set; }
        public string user_full_name { get; set; }
        public string creat_appr_flg { get; set; }
        public int created_by { get; set; }
        public DateTime? del_dte { get; set; }
        public string remarks { get; set; }
        public DateTime? pswrd_expiry_dte { get; set; }
        public int row_version { get; set; }
        public int record_status { get; set; }
        public int status_sk { get; set; }
        public string mobile_cntry_cde { get; set; }
        public string mobile_nbr { get; set; }
        public int br_code { get; set; }
        public string br_desc { get; set; }
        [Skip]
        [Ignore]
        public string machine_name { get; set; }
        [Skip]
        [Ignore]
        public string IP { get; set; }
        [Skip]
        [Ignore]
        public string url { get; set; }
        public string status { get; set; }
        [Skip]
        [Ignore]
        public List<vu_user_lvl_can_do> cando { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
        [Skip]
        [Ignore]
        public string logintyp { get; set; }
        [Skip]
        [Ignore]
        public string dynmc_url { get; set; }
        [Skip]
        [Ignore]
        public bool is_login { get; set; }
        [Skip]
        [Ignore]
        public DateTime Login_DTE_TME { get; set; }
    }

    public class vu_users_aprv
    {
        [Key]
        public long user_sk { get; set; }
        public string username { get; set; }
        public string user_full_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string mobile_cntry_cde { get; set; }
        public string mobile_nbr { get; set; }
        public DateTime? last_login { get; set; }
        public string creat_appr_flg { get; set; }
        public string Crtd_by_IP { get; set; }
        public string updtd_by_IP { get; set; }
        public int mt_code { get; set; }
        public DateTime? del_dte { get; set; }
        public string remarks { get; set; }
        public DateTime? pswrd_expiry_dte { get; set; }
        public int status_sk { get; set; }
        public int created_by { get; set; }
        public DateTime created_on { get; set; }
        public int? updated_by { get; set; }
        public DateTime? updated_on { get; set; }
        public int row_version { get; set; }
        public int record_status { get; set; }
        public string status { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
        [Skip]
        [Ignore]
        public List<vu_user_lvl_can_do_aprv> cando { get; set; }
        [Skip]
        [Ignore]
        public bool is_login { get; set; }
    }
}