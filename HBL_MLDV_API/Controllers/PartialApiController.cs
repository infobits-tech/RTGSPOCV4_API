using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Models;

namespace HBL_MLDV_API.Controllers
{
   [RoutePrefix("api/PartialApi")]
    public class PartialApiController : ApiController
    {
        [HttpPost]
        [Route("TitleFetching_Save/{user_sk}")]
        public IHttpActionResult TitleFetching_Save(TITLFTCHNG_RSPNS model, int user_sk)
        {
            try
            {
                using (TitleFetching_saveServices obj = new TitleFetching_saveServices())
                {
                    return Ok(obj.TitleFetching_Save(model, user_sk));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("TitleFetching_SaveRQST/{user_sk}")]
        public IHttpActionResult TitleFetching_SaveRQST(TITLFTCHNG_RQST model, int user_sk)
        {
            try
            {
                using (TitleFetching_saveServices obj = new TitleFetching_saveServices())
                {
                    return Ok(obj.TitleFetching_SaveRQST(model, user_sk));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("TitleFetching_update/{IS_ACTV}")]
        public IHttpActionResult TitleFetching_update(int IS_ACTV)
        {
            try
            {
                using (TitleFetching_saveServices obj = new TitleFetching_saveServices())
                {

                    return Ok(obj.updateData(IS_ACTV));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //[HttpPost]
        //[Route("PaymentLoadgment_Save/{user_sk}")]
        //public IHttpActionResult PaymentLoadgment_Save(Payment_Lodgment_RSPNS model, int user_sk)
        //{
        //    //try
        //    //{
        //    //    using (PaymentLoadgment_SaveServices obj = new PaymentLoadgment_SaveServices())
        //    //    {
        //    //        return Ok(obj.PaymentLoadgment_Save(model, user_sk));
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    return null;
        //    //}
        //}

        [HttpPost]
        [Route("HoldFunds_Save/{user_sk}")]
        public IHttpActionResult HoldFunds_Save(Hold_RSPNS model, int user_sk)
        {
            try
            {
                using (HoldFunds_saveServices obj = new HoldFunds_saveServices())
                {
                    return Ok(obj.HoldFunds_Save(model, user_sk));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost]
        [Route("HoldFunds_SaveRQST/{user_sk}")]
        public IHttpActionResult HoldFunds_SaveRQST(Hold_RQST model, int user_sk)
        {
            try
            {
                using (HoldFunds_saveServices obj = new HoldFunds_saveServices())
                {
                    return Ok(obj.HoldFunds_SaveRQST(model, user_sk));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
