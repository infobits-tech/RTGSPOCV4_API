using HBL_MLDV_API.Models;
using HBL_MLDV_API.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Controllers
{
    [RoutePrefix("api/MSG_STS")]
    public class MSG_STSController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpPost]
        [Route("SaveData/{user_sk}/{is_ach?}")]
        public IHttpActionResult SaveData(MSG_STS model, int user_sk,bool is_ach = false)
        {
            try
            {
                using (MSG_STSServices obj = new MSG_STSServices())
                {
                    return Ok(obj.SaveData(model, user_sk, is_ach));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData()");
                return null;
            }
        }
        [HttpPost]
        [Route("SaveData_Inward/{user_sk}/{is_ach?}")]
        public IHttpActionResult SaveData_Inward(MSG_STS_INWARD model, int user_sk, bool is_ach = false)
        {
            try
            {
                using (MSG_STSServices obj = new MSG_STSServices())
                {
                    return Ok(obj.SaveData_Inward(model, user_sk, is_ach));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData_Inward()");
                return null;
            }
        }

        [HttpPost]
        [Route("INSERT_INSTANCE/{user_sk}")]
        public IHttpActionResult INSERT_INSTANCE(INSTANCE_TXN model, int user_sk)
        {
            try
            {
                using (MSG_STSServices obj = new MSG_STSServices())
                {
                    return Ok(obj.INSERT_INSTANCE(model, user_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "INSERT_INSTANCE()");
                return null;
            }
        }

        [HttpGet]
        [Route("CheckFP/{doc_id}")]
        public IHttpActionResult GetData(int doc_id)
        {
            try
            {
                using (MSG_STSServices obj = new MSG_STSServices())
                {
                    return Ok(obj.CheckFP(doc_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }

        [HttpGet]
        [Route("CheckHold/{doc_id}")]
        public IHttpActionResult GetHoldData(int doc_id)
        {
            try
            {
                using (MSG_STSServices obj = new MSG_STSServices())
                {
                    return Ok(obj.CheckHold(doc_id));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
