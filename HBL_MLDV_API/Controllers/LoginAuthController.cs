using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Providers;

namespace HBL_MLDV_API.Controllers
{
    [RoutePrefix("LoginAuth")]
    public class LoginAuthController : ApiController
    {
        [Route("GetAccess/{user_id}")]
        public IHttpActionResult GenerateLink(string user_id)
        {
            //if (data != null)
            //{
                return Ok();
            //}
            //else
            //{
            //    return Ok("error");
            //}
        }
    }
}
