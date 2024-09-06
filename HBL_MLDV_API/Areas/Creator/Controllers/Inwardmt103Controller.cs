using HBL_MLDV_API.Areas.Creator.Models;
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
    [RoutePrefix("api/Creator/Inwardmt103")]
    //Inward MT103
    public class Inwardmt103Controller : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpGet]
        [Route("GetData/{user_sk}")]
        public IHttpActionResult GetData(int user_sk)
        {
            try
            {
                using (InwardServices obj = new InwardServices())
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

        [HttpPost]
        [Route("SaveData/{user_sk}")]
        public IHttpActionResult SaveData(VU_INBOUND_TXN_DTL model, int user_sk)
        {
            try
            {
                using (InwardServices obj = new InwardServices())
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
        [HttpGet]
        [Route("GetDataById/{TXN_ID}")]
        public IHttpActionResult GetDataById(int TXN_ID)
        {
            try
            {
                using (InwardServices obj = new InwardServices())
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
