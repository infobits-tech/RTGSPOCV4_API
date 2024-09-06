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

    [RoutePrefix("api/Creator/CB_CutOff_Times")]
    public class CB_CutOffTimesController : ApiController
    {



        UniversalRepository un = new UniversalRepository();
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
                un.WriteException(ex.ToString(), "SaveData()");
                return null;
                }
            }
        //[HttpPost]
        //[Route("Update/{UserId}")]
        //public IHttpActionResult UpdateData(CB_CUTOFF_TIMES mod, int UserId)
        //{
        //    try
        //    {

        //        using (CB_CutOffTimesServices obj = new CB_CutOffTimesServices())
        //        {
        //            return Ok(obj.SaveData(mod, UserId));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}


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
                un.WriteException(ex.ToString(), "GetData()");
                return null;
                }
            }

            [HttpGet]
            [Route("GetDataById/{DOC_TYP_SK}")]
            public IHttpActionResult GetDataById(string DOC_TYP_SK)
            {
                try
                {
                    using (CB_CutOffTimesServices obj = new CB_CutOffTimesServices())
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

