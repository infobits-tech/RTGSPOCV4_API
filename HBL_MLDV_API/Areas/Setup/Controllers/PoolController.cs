using HBL_MLDV_API.Areas.Setup.Models;
using HBL_MLDV_API.Providers.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.Setup.Controllers
{

    [RoutePrefix("api/setup/pool")]
    public class PoolController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpPost]
        [Route("save")]
        public IHttpActionResult save(Pools model)
        {
            try
            {
                using (PoolServices obj = new PoolServices())
                {
                    return Ok(obj.Save(model));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "save()");
                return null;
            }
        }
        [HttpGet]
        [Route("Getdata")]
        public IHttpActionResult Getdata()
        {
            try
            {
                using (PoolServices obj = new PoolServices())
                {
                    return Ok(obj.GetData());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Getdata()");
                return null;
            }
        }
        [HttpGet]
        [Route("Getdatabyid/{id}")]
        public IHttpActionResult Getdatabyid(int id)
        {
            try
            {
                using (PoolServices obj = new PoolServices())
                {
                    return Ok(obj.GetDatabyid(id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Getdatabyid()");
                return null;
            }
        }
    }
}
