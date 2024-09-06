using HBL_MLDV_API.Providers.Monitring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HBL_MLDV_API.Areas.Monitring.Controllers
{
    [RoutePrefix("api/Monitring/InOutQueues")]
    public class InOutQueuesController : ApiController
    {
        [HttpGet]
        [Route("GetOutward/{userId}")]
        public IHttpActionResult GetOutward(int userId)
        {
            try
            {
                using (InOutService obj = new InOutService())
                {
                    return Ok(obj.GetOutward(userId));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("GetInward/{userId}")]
        public IHttpActionResult GetInward(int userId)
        {
            try
            {
                using (InOutService obj = new InOutService())
                {
                    return Ok(obj.GetInward(userId));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("getFprspns/{id}")]
        public IHttpActionResult getFprspns(int id)
        {
            try
            {
                using (InOutService obj = new InOutService())
                {
                    return Ok(obj.getFprspns(id));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
