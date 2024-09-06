using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Security
{
    public partial class vu_user_urls
    {
        public int activityid { get; set; }
        public long id { get; set; }
        public string activitytitle { get; set; }
        public string activityurl { get; set; }
        public Nullable<int> activityparentid { get; set; }
        public string activitydisc { get; set; }
        public string activiticon { get; set; }
        public string module_nme { get; set; }
        public string status { get; set; }
        public Nullable<long> employee_id { get; set; }
        public string Employee_Code { get; set; }
    }
}