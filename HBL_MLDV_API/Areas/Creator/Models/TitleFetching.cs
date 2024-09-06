using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class TitleFetching
    {
        [Key]
        public int Id { get; set; }
        public string doc_type { get; set; }

        public string msg_typ { get; set; }

        public int doc_sk { get; set; }

        public DateTime Created_DateTime { get; set; }

        public string Creadted_by { get; set; }
        public string AccountNo_param { get; set; }
        public int identifier_No_param { get; set; }
        public int Request_No_param { get; set; }
        public string Account_of_Title_resp { get; set; }
        public string Account_Status_resp { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
    }
}