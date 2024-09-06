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
    public class PaymentLoadgmentController : ApiController
    {


        [HttpPost]
        [Route("GETTrasByID")]
        public IHttpActionResult GETTrasByID(Payment_Lodgment_RQST pl)
        {
            try
            {
                using (PaymentLoadgmenrServices obj = new PaymentLoadgmenrServices())
                {
                    return Ok(obj.GETTrasByID(pl));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
