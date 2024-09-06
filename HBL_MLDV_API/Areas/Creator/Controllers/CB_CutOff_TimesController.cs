using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Providers.Creator;
using HBL_MLDV_API.Providers.Creator.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    
    [RoutePrefix("api/Creator/CB_CutOff_Times")]
    public class CB_CutOffTimesController : ApiController
    {

        [HttpPost]
        [Route("SaveData/{UserId}")]
        public IHttpActionResult SaveData(CB_CUTOFF_TIMES mod, int UserId)
        {
            try
            {

                using (CB_CutOffTimesServices obj = new CB_CutOffTimesServices())
                {
                    return Ok(obj.SaveData(mod, UserId));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        [Route("GetData")]
        public IHttpActionResult GetData()
        {
            try
            {
                using (CB_CutOffTimesServices obj = new CB_CutOffTimesServices())
                {
                    return Ok(obj.GetData());
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetDataById/{CUTOFF_ID}")]
        public IHttpActionResult GetDataById(int CUTOFF_ID)
        {
            try
            {
                using (CB_CutOffTimesServices obj = new CB_CutOffTimesServices())
                {
                    return Ok(obj.GetDataById(CUTOFF_ID));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
