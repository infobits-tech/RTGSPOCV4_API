using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class InvalidLogin
    {
        public int ID { get; set; }
        public string ERRORMSG { get; set; }

        public string IPADDRESS { get; set; }

        public string MACHINE_NAME { get; set; }
        public string URL { get; set; }

        public DateTime Timestamp { get; set; }
        [Ignore]
        public string state { get; set; }
    }
    public class UserNavLog
    {
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public string PageURL { get; set; }
        public string DYNMC_URL { get; set; }
        public DateTime Timestamp { get; set; }
        [Ignore]
        public string state { get; set; }
    }
    public class InvalidUserNavLog
    {
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public string PageURL { get; set; }
        public string DYNMC_URL { get; set; }
        public DateTime Timestamp { get; set; }
        [Ignore]
        public string state { get; set; }
    }
    public class UserSetupLog
    {
        public int ID { get; set; }
        public int USER_ID { get; set; }
        public int USR_AFCTD { get; set; }
        public string Event { get; set; }
        public DateTime TimeStamp { get; set; }
        public string DYNMC_URL { get; set; }
        [Ignore]
        public string state { get; set; }
    }
}