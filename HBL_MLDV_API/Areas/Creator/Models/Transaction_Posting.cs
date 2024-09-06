using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class Transaction_Posting
    {
        [Key]
        public int Id { get; set; }
        public string doc_type { get; set; }

        public string msg_typ { get; set; }

        public string doc_sk { get; set; }

        public DateTime Created_DateTime { get; set; }

        public string pf_rspns { get; set; }

        public string Creadted_by { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }


    }
}