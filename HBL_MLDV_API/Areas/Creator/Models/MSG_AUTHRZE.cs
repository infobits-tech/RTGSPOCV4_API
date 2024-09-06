using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class MSG_AUTHRZE
    {
        public int AUTH_ID { get; set; }
        public string DOC_TYP_SK { get; set; }
        public float MIN_AMT { get; set; }
        public float MAX_AMT { get; set; }
        public int POOL { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
        [Skip]
        [Ignore]
        public List<MSG_AUTHRZE> lstAuthorization { get; set; }
    }
}