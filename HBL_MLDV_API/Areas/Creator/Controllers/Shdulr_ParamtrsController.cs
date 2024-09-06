using HBL_MLDV_API.Areas.Creator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Creator.Controller
{
    [RoutePrefix("api/Creator/Shdulr_Paramtrs")]
    public class Shdulr_ParamtrsController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpPost]
        [Route("SaveData/{UserId}")]
        public IHttpActionResult SaveData(msg_ctff_tme_hdr sp, int UserId)
        {
            try
            {
               
                using (Shdulr_ParamtrsService obj = new Shdulr_ParamtrsService())
                {
                    return Ok(obj.SaveData(sp, UserId));
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
