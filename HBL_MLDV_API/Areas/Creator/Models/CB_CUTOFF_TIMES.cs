using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Models
{
    public class CB_CUTOFF_TIMES
    {
        public int CUTOFF_ID { get; set; }
        public string DOC_TYP_SK { get; set; }
        public string DY_NM { get; set; }
        public TimeSpan CB_FR_TME { get; set; }
        public TimeSpan CB_TO_TME { get; set; }
        public bool HLYDY_FLG { get; set; }
        [Skip]
        [Ignore]
        public string state { get; set; }
        [Skip]
        [Ignore]
        public List<CB_CUTOFF_TIMES> lstCB_CUTOFF_TIMES { get; set; }
    }
}