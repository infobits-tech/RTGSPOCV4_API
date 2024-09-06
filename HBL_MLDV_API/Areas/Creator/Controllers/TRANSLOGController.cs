using HBL_MLDV_API.Areas.Creator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    [RoutePrefix("api/Creator/TRANSLOG")]
    public class TRANSLOGController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
       // internal IEnumerable<object> lstTRANSLOG;

        [HttpPost]
        [Route("SaveData")]
        public IHttpActionResult SaveData(List<Act_logs> model)
        {
            try
            {
                using (TRANSLOGServices obj = new TRANSLOGServices())

                {
                    return Ok(obj.SaveData(model));
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

