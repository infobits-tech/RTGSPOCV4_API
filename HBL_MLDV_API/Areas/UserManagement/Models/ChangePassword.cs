using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.UserManagement.Models
{
    public class ChangePassword
    {
        public long? userid { get; set; }
        public string OldPassword { get; set; }
        public string OldPasswordEncrpt { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPasswordEncrpt { get; set; }
        public string username { get; set; }
    }
}