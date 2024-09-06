using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Providers.Creator;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    [RoutePrefix("api/Creator/ACHInward")]
    public class ACHInwardController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        // GET api/<controller>
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
                using (ACHInwardServices obj = new ACHInwardServices())
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
        [Route("GetDataById/{mst_id}")]
        public IHttpActionResult GetDataById(int mst_id)
        {
            try
            {
                using (ACHInwardServices obj = new ACHInwardServices())
                {

                    return Ok(obj.GetDataById(mst_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataById()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetCurrencyACH/{mst_id}")]
        public IHttpActionResult GetCurrencyACH(int mst_id)
        {
            try
            {
                using (ACHInwardServices obj = new ACHInwardServices())
                {

                    return Ok(obj.GetCurrencyACH(mst_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetCurrencyACH()");
                return null;
            }
        }

        [HttpGet]
        [Route("updateCancle/{mst_id}")]
        public IHttpActionResult updateCancle(int mst_id)
        {
            try
            {
                using (ACHInwardServices obj = new ACHInwardServices())
                {

                    return Ok(obj.updateCancle(mst_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "updateCancle()");
                return null;
            }
        }
        [HttpPost]
        [Route("SaveData/{user_sk}")]
        public IHttpActionResult SaveData(ACH_MST_Inward model, int user_sk)
        {
            try
            {
                using (ACHInwardServices obj = new ACHInwardServices())
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
    }
}