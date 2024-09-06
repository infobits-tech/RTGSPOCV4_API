using HBL_MLDV_API.Areas.Creator.Models;
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

    [RoutePrefix("api/Creator/TitleFetching")]
    public class TitleFetchingController : ApiController
    {
        [HttpPost]
        [Route("GETTrasByID")]
        public IHttpActionResult GETTrasByID(TITLFTCHNG_RQST tf)
        {
            try
            {
                using (TitleFetchingServices obj = new TitleFetchingServices())
                {
                    return Ok(obj.GETTrasByID(tf));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //[HttpGet]
        //[Route("GetDataById/{mst_id}")]
        //public IHttpActionResult GetDataById(int mst_id)
        //{
        //    try
        //    {
        //        using (TitleFetchingServices obj = new TitleFetchingServices())
        //        {

        //            return Ok(obj.GetDataById(mst_id));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

    }
}
