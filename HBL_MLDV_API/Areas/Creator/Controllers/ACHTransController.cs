using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Providers.Creator;
using HBL_MLDV_API.Repository;
namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    [RoutePrefix("api/Creator/ACHTrans")]
    public class ACHTransController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        /// <summary>
        /// Get Data from Database View for Index Page
        /// </summary>
        /// <returns>TXN_DTL_VU Model</returns>
        [HttpGet]
        [Route("GetData/{user_sk}")]
        public IHttpActionResult GetData(int user_sk)
        {
            try
            {
                using (ACHTransServices obj = new ACHTransServices())
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

        [HttpGet]
        [Route("GetDataById/{mst_id}")]
        public IHttpActionResult GetDataById(int mst_id)
        {
            try
            {
                using (ACHTransServices obj = new ACHTransServices())
                {

                    return Ok(obj.GetDataById(mst_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataById()");
                return null;
            }
        }

        [HttpGet]
        [Route("updateCancle/{mst_id}")]
        public IHttpActionResult updateCancle(int mst_id)
        {
            try
            {
                using (ACHTransServices obj = new ACHTransServices())
                {

                    return Ok(obj.updateCancle(mst_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "updateCancle()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetDataForEdit/{mst_id}")]
        public IHttpActionResult GetDataForEdit(int mst_id, int req_ID)
        {
            try
            {
                using (ACHTransServices obj = new ACHTransServices())
                {

                    return Ok(obj.GetDataForEdit(mst_id, req_ID));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataForEdit()");
                return null;
            }
        }
        
        [HttpGet]
        [Route("CurrencyCheck/{mst_id}")]
        public IHttpActionResult CurrencyCheck(int mst_id)
        {
            try
            {
                using (ACHTransServices obj = new ACHTransServices())
                {

                    return Ok(obj.CurrencyCheck(mst_id));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "CurrencyCheck()");
                return null;
            }
        }

        [HttpGet]
        [Route("CheckSenderRef/{sender_ref}")]
        public IHttpActionResult CheckSenderRef(string sender_ref)
        {
            try
            {
                using (ACHTransServices obj = new ACHTransServices())
                {

                    return Ok(obj.CheckSenderRef(sender_ref));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "CheckSenderRef()");
                return null;
            }
        }

        [HttpPost]
        [Route("SaveData/{user_sk}")]
        public IHttpActionResult SaveData(ACH_mst model, int user_sk)
        {
            try
            {
                using (ACHTransServices obj = new ACHTransServices())
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

        ///// <summary>
        ///// Get Data from Database View for Edit Page
        ///// </summary>
        ///// <returns>TXN_DTL_VU Model</returns>
        //[HttpGet]
        //[Route("GetDataById/{TXN_ID}")]
        //public IHttpActionResult GetDataById(int TXN_ID)
        //{
        //    try
        //    {
        //        using (ACHTransController obj = new ACHTransController())
        //        {
        //            return Ok(obj.GetDataById(TXN_ID));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}
