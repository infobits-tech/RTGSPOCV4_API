using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using HBL_MLDV_API.Providers.Creator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    [RoutePrefix("api/Creator/HoldFunds")]

    public class HoldFundsController : ApiController
    {
        [HttpPost]
        [Route("GETTrasByID")]
        public IHttpActionResult GETTrasByID(Hold_RQST tf)
        {
            try
            {
                using (HoldFundServices obj = new HoldFundServices())
                {
                    return Ok(obj.GETTrasByID(tf));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}