using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class msg_ctff_tme_hdr
    {
        public int SWFT_MSG_ID { get; set; }
        public string Message_Type { get; set; }
        public bool Authorization { get; set; }
        public int MinimumAmount { get; set; }
        public int MaximumAmount { get; set; }
        public bool ManageEmailingOption { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Group_Mail { get; set; }
        [Ignore]
        [Skip]
        public string state { get; set; }
    }
}