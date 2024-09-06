using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Monitring.Models
{
    public class Vu_Log_UserDeatils
    {
        public string UserID { get; set; }
        public string User_Account_Status { get; set; }
        public string User_Created_By { get; set; }
        public DateTime User_Created_Date { get; set; }
        public DateTime User_Last_Login_Date { get; set; }
        public string User_Modified_By { get; set; }
        public DateTime User_Last_Modification_Date { get; set; }
        public int User_Inactivity_Days { get; set; }



        public DateTime User_Login_DateTime { get; set; }
        public DateTime User_Logout_DateTime { get; set; }
        public string MachineName { get; set; }
        public string IPAddress { get; set; }


        public string AccessMessage { get; set; }
        public DateTime Access_Datetime { get; set; }

        public string PageUrl { get; set; }
        public DateTime Nav_Datetime { get; set; }
    }
}