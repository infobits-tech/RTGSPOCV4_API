using HBL_MLDV_API.Providers.Creator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    [RoutePrefix("api/Creator/inward_rejected_txn")]
    public class inward_rejected_txnController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpGet]
        [Route("GetData/{user_sk}")]
        public IHttpActionResult GetData(int user_sk)
        {
            try
            {
                using (inward_rejected_txn_Services obj = new inward_rejected_txn_Services())
                {
                    return Ok(obj.GetData(user_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetDataById/{TXN_ID}")]
        public IHttpActionResult GetDataById(int TXN_ID)
        {
            try
            {
                using (inward_rejected_txn_Services obj = new inward_rejected_txn_Services())
                {
                    return Ok(obj.CreateRefund(TXN_ID));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataById()");
                return null;
            }
        }

    }
}