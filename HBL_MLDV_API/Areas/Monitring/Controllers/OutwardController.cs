using HBL_MLDV_API.Providers.Monitring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Monitring.Controllers
{
    [RoutePrefix("api/Monitring/Outward")]
    public class OutwardController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpGet]
        [Route("GetData/{msg_type}/{inOrOut}/{FromDate}/{ToDate}/{BankCode}")]
        public IHttpActionResult GetData(string msg_type, string inOrOut, string FromDate, string ToDate, string BankCode)
        {
            try
            {
                using (OutwardMntringServices obj = new OutwardMntringServices())
                {
                    return Ok(obj.GetData(msg_type, inOrOut, FromDate, ToDate, BankCode));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetReportName/{Report_id}")]
        public IHttpActionResult GetReportName(string Report_id)
        {
            try
            {
                using (OutwardMntringServices obj = new OutwardMntringServices())
                {
                    return Ok(obj.GetReportName(Report_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReportName()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetDataMessage/{Doc_sk}")]
        public IHttpActionResult GetDataMessage(string doc_sk)
        {
            try
            {
                using (OutwardMntringServices obj = new OutwardMntringServices())
                {
                    return Ok(obj.GetDataMessage(doc_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataMessage()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetDataInward/{Doc_sk}")]
        public IHttpActionResult GetDataInward(string doc_sk)
        {
            try
            {
                using (OutwardMntringServices obj = new OutwardMntringServices())
                {
                    return Ok(obj.GetDataInward(doc_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataInward()");
                return null;
            }
        }
    }
}
