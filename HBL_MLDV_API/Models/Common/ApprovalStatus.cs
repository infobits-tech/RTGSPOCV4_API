using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Models.Common
{
    enum ApprovalStatus
    {
        Draft = 1,
        Approved = 2,
        PreApproved = 3,
        StockReceived = 4,
        PendingtoReceive = 5,
        Proceed = 6,
        Rejected = 7,
        Pending = 8,
        Invalid = 9,
        Requested = 10,
        Active = 11,
        Inactive = 12,
    }
}