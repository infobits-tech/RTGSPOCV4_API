using HBL_MLDV_API.Providers.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Approval.Controllers
{
    [RoutePrefix("api/Approval/PoolView")]
    public class PoolViewController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpGet]
        [Route("GetPoolView")]
        public IHttpActionResult GetPoolView()
        {
            try
            {
                using (PoolViewServices obj = new PoolViewServices())
                {
                    return Ok(obj.GetPoolView());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetPoolView()");
                return null;
            }
        }
    }
}
