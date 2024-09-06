using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Providers.Creator;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    [RoutePrefix("api/Creator/mt202trns")]
    public class mt202trnsController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        /// <summary>
        /// Get Data from Database View for Index Page
        /// </summary>
        /// <returns>TXN_DTL_VU Model</returns>
        [HttpGet]
        [Route("GetData/{user_sk}")]
        public IHttpActionResult GetData(int user_sk)
        {
            try
            {
                using (mt202transService obj = new mt202transService())
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

        /// <summary>
        /// Get Data from Application and Save in to table tr_app.txn_dtl_vu
        /// </summary>
        /// <returns>CustomObject Model</returns>
        [HttpPost]
        [Route("SaveData/{user_sk}")]
        public IHttpActionResult SaveData(VU_TXN_DTL_INPUT model, int user_sk)
        {
            try
            {
                using (mt202transService obj = new mt202transService())
                {
                    return Ok(obj.SaveData(model, user_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData()");
                return null;
            }
        }
        /// <summary>
        /// Get Data from Database View for Edit Page
        /// </summary>
        /// <returns>TXN_DTL_VU Model</returns>
        [HttpGet]
        [Route("GetDataById/{TXN_ID}")]
        public IHttpActionResult GetDataById(int TXN_ID)
        {
            try
            {
                using (mt202transService obj = new mt202transService())
                {
                    return Ok(obj.GetDataById(TXN_ID));
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
