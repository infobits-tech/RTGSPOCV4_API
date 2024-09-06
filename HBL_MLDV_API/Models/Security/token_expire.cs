using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Security
{
    public class token_expire : IDisposable
    {
        [Key]
        [IgnoreAttribute]
        [SkipAttribute]
        public int row_sk { get; set; }
        public int user_sk { get; set; }
        public string token_payload { get; set; }
        public DateTime token_issue_dtme { get; set; }
        public int is_token_exp { get; set; }
        [IgnoreAttribute]
        [SkipAttribute]
        public int token_expired_by { get; set; }
        [IgnoreAttribute]
        [SkipAttribute]
        public DateTime token_mark_invalid_dtme { get; set; }
        [IgnoreAttribute]
        [SkipAttribute]
        public string remarks { get; set; }
        public int row_version { get; set; }
        [IgnoreAttribute]
        [Skip]
        public string state { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}