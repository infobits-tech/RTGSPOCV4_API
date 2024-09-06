using HBL_MLDV_API.DBAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Areas.Approval.Models
{
    public class pool_amt_map
	{
		public int map_sk { get; set; }
		public int pool_sk { get; set; }
		public int min_amt { get; set; }
		public int max_amt { get; set; }
		public int created_by { get; set; }
		public DateTime? create_dt_tme { get; set; }
		public DateTime? update_dt_tme { get; set; }
		public int updated_by { get; set; }
		public short record_status { get; set; }
		[Ignore]
		[Skip]
		public string state { get; set; }
		[Ignore]
		[Skip]
		public int row_version { get; set; }
	}
}