using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models
{
    public class Vu_BANKS_CODE_MAPPING
    {
        public string BIC { get; set; }
        public string BANK_NAME { get; set; }
        public string AC_NO { get; set; }
        public string BANK_CODE { get; set; }
        public string CONV_BIC { get; set; }
        public int? Interval { get; set; }
        public DateTime? LastDateTime { get; set; }
    }
}