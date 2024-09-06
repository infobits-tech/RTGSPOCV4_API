using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Common
{
    public class CustomObject
    {
        public bool status { get; set; }
        public string Message { get; set; }
        public IEnumerable Data { get; set; }
    }
}