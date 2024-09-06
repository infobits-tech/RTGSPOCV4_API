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
    [RoutePrefix("api/Creator/Authorization")]
    public class AuthorizationController : ApiController
    {

        UniversalRepository un = new UniversalRepository();
        [HttpPost]
        [Route("SaveData/{UserId}")]
        public IHttpActionResult SaveData(MSG_AUTHRZE mod, int UserId)
        {
            try
            {

                using (AuthorizationServices obj = new AuthorizationServices())
                {
                    return Ok(obj.SaveData(mod, UserId));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData()");
                return null;
            }
        }
            [HttpGet]
        [Route("GetData")]
        public IHttpActionResult GetData()
        {
            try
            {
                using (AuthorizationServices obj = new AuthorizationServices())
                {
                    return Ok(obj.GetData());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetDataById/{MSG_TYP}")]
        public IHttpActionResult GetDataById(string DOC_TYP_SK)
        {
            try
            {
                using (AuthorizationServices obj = new AuthorizationServices())
                {
                    return Ok(obj.GetDataById(DOC_TYP_SK));
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
