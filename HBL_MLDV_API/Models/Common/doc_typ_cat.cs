using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Common
{
    public class doc_typ_cat
    {
        public int doc_typ_cat_sk { get; set; }
        public string doc_typ_cat_desc { get; set; }
        public int is_indv { get; set; }
        public int is_corp { get; set; }
        public int sorting { get; set; }
    }
}