using HBL_MLDV_API.Providers.Monitring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Monitring.Controllers
{
    [RoutePrefix("api/Monitring/Logs")]
    public class LogsController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpGet]
        [Route("GetLogData/{Reportid}/{FromDate}/{ToDate}")]
        public IHttpActionResult GetLogData(string Reportid, string FromDate, string ToDate)
        {
            try
            {
                using (OutwardMntringServices obj = new OutwardMntringServices())
                {
                    if (Reportid == "1")
                    {
                        return Ok(obj.GetLogData(Reportid, "", ""));
                    }
                    else
                    {
                        return Ok(obj.GetLogData(Reportid, FromDate, ToDate));
                    }
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetLogReportName/{Report_id}")]
        public IHttpActionResult GetLogReportName(string Report_id)
        {
            try
            {
                using (OutwardMntringServices obj = new OutwardMntringServices())
                {
                    return Ok(obj.GetLogReportName(Report_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReportName()");
                return null;
            }
        }
    }
}