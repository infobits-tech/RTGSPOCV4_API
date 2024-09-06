using AutoMapper;
using HBL_MLDV_API.Areas.Approval.Models;
using HBL_MLDV_API.Areas.UserManagement.Models;
using HBL_MLDV_API.Areas.UserManagement.Models.Role;
using HBL_MLDV_API.Models.Common;
using HBL_MLDV_API.Models.Security;
using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Providers.Approval;
using HBL_MLDV_API.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HBL_MLDV_API.Areas.Approval.Controllers
{
    [RoutePrefix("api/Approval/TxnReprocess")]
    public class TxnReprocessController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        //[HttpPost]
        //[Route("SavePoolDocs/{is_inward}")]
        //public IHttpActionResult SavePoolDocs(vu_pool_docs model, bool is_inward)
        //{
        //    try
        //    {

        //        //var configuration = new MapperConfiguration(cfg =>
        //        //{
        //        //    cfg.CreateMap<vu_pool_docs, vu_pool_docs_history>();
        //        //});
        //        //var mapper = configuration.CreateMapper();
        //        //
        //        //vu_pool_docs_history modelhistory = mapper.Map<vu_pool_docs, vu_pool_docs_history>(model);

        //        string param = model.br_sk.ToString() + "|" + model.doc_typ_sk.ToString() + "|" + model.doc_mst_sk.ToString() + "|" + model.doc_no + "|" + model.aprv_cat_sk.ToString() + "|" + model.doc_link + "|" + model.created_by + "|" + model.recallReason + "|" + model.txn_amt.ToString() + "|" + is_inward.ToString();
        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            if (obj.CheckIsExist(model) == false)
        //            {
        //                return Ok(obj.Savepooldoc(param));
        //            }
        //            else
        //            {
        //                CustomObject cust_obj = new CustomObject();
        //                cust_obj.Data = null;
        //                cust_obj.Message = "Pool Already Exist !";
        //                return Ok(cust_obj);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "SavePoolDocs()");
        //        return null;
        //    }
        //}
        //[HttpPost]
        //[Route("SubmitApproval")]
        //public IHttpActionResult SubmitApproval(vu_pool_docs model)
        //{
        //    try
        //    {

        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            return Ok(obj.SubmitApproval(model));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "SubmitApproval()");
        //        return null;
        //    }
        //}
        //[HttpPost]
        //[Route("PoolForward")]
        //public IHttpActionResult PoolForward(List<vu_pool_docs> model)
        //{
        //    try
        //    {

        //        using (PoolManagementServices serv = new PoolManagementServices())
        //        {
        //            return Ok(serv.PoolForward(model));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "PoolForward()");
        //        return null;
        //    }
        //}

        //[HttpPost]
        //[Route("PoolMapping")]
        //public IHttpActionResult PoolMapping(pools model)
        //{
        //    try
        //    {
        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            CustomObject customObj = null;
        //            customObj = obj.Save(model);
        //            return Ok(customObj);


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "PoolMapping()");
        //        return null;
        //    }
        //}
        //[HttpGet]
        //[Route("CheckRange/{max}/{min}")]
        //public IHttpActionResult CheckRange(int max, int min)
        //{
        //    try
        //    {
        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            return Ok(obj.CheckRange(max, min));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "CheckRange()");
        //        return null;
        //    }
        //}
        //[HttpGet]
        //[Route("GetReviewLog/{doc_typ_sk}/{doc_mst_sk}/{pool_doc_sk}")]
        //public IHttpActionResult GetReviewLog(int doc_typ_sk, int doc_mst_sk, int pool_doc_sk)
        //{
        //    try
        //    {
        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            return Ok(obj.GetReviewLog(doc_typ_sk, doc_mst_sk, pool_doc_sk));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "GetReviewLog()");
        //        return null;
        //    }
        //}
        //[HttpPost]
        //[Route("Bindetails")]
        //public IHttpActionResult Bindetails(pools model)
        //{
        //    try
        //    {
        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            return Ok(obj.Bindetails(model.pool_sk));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "Bindetails()");
        //        return null;
        //    }
        //}

        //[HttpGet]
        //[Route("AdvancePoolSearch")]
        //public IHttpActionResult Getdata()
        //{
        //    try
        //    {
        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            return Ok(obj.Getdata());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "Getdata()");
        //        return null;
        //    }
        //}

        [HttpGet]
        [Route("GetOutwardTxn")]
        public IHttpActionResult GetOutwardTxn()
        {
            try
            {
                using (TxnReprocessServices obj = new TxnReprocessServices())
                {
                    return Ok(obj.GetOutwardTxn());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetOutwardTxn()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetInwardTxn")]
        public IHttpActionResult GetInwardTxn()
        {
            try
            {
                using (TxnReprocessServices obj = new TxnReprocessServices())
                {
                    return Ok(obj.GetInwardTxn());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetInwardTxn()");
                return null;
            }
        }

        [HttpGet]
        [Route("getFprspns/{id}")]
        public IHttpActionResult getFprspns(int id)
        {
            try
            {
                using (TxnReprocessServices obj = new TxnReprocessServices())
                {
                    return Ok(obj.getFprspns(id));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [Route("GetHoldCode/{Doc_sk}")]
        public IHttpActionResult GetHoldCode(int Doc_sk)
        {
            try
            {
                using (TxnReprocessServices obj = new TxnReprocessServices())
                {
                    return Ok(obj.GetHoldCode(Doc_sk));
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //[HttpPost]
        //[Route("GenerateReviewLog")]
        //public IHttpActionResult GenerateReviewLog(vu_pool_docs_rvw_log model)
        //{
        //    try
        //    {
        //        using (PoolManagementServices obj = new PoolManagementServices())
        //        {
        //            return Ok(obj.GenerateReviewLog(model));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "GenerateReviewLog()");
        //        return null;
        //    }
        //}

    }
}
