using HBL_MLDV_API.Models;
using HBL_MLDV_API.Providers;
using System;
using System.Collections.Generic;
using System.Web.Http;


namespace HBL_MLDV_API.Controllers
{
    [RoutePrefix("api/DropDown")]
    public class DropDownController : ApiController
    {
        HBL_MLDV_API.Repository.UniversalRepository un = new Repository.UniversalRepository();
        [HttpGet]
        [Route("GetStatusMasterList")]
        public IHttpActionResult GetStatusMasterList()

        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {

                    return Ok(obj.GetStatusMasterList());

                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetStatusMasterList()");
                return null;
            }
        }
        [HttpGet]
        [Route("pool_action")]
        public IHttpActionResult pool_action()
        {
            try
            {
                using (DropDownService _repo = new DropDownService())
                {
                    //List<DropDownModel> lst = _repo.GetActivePools();
                    return Ok(_repo.pool_action());

                }
            }

            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "pool_action()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetReference/{msg_typ}")]
        public IHttpActionResult GetReference(string msg_typ)
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetReference(msg_typ));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReference()");
                return null;
            }
        }
        [HttpGet]
        [Route("Get53")]
        public IHttpActionResult Get53()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.Get53());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Get53()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetActivePools")]
        public IHttpActionResult GetActivePools()
        {
            try
            {
                using (DropDownService _repo = new DropDownService())
                {
                    //List<DropDownModel> lst = _repo.GetActivePools();
                    return Ok(_repo.GetActivePools());

                }
            }

            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetActivePools()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetActiveCountries")]
        public IHttpActionResult GetActiveCountries()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    var dt = obj.GetActiveCountries();
                    return Ok(dt);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetActiveCountries()");
                return null;
            }
        }
        [HttpPost]
        [Route("CheckTransactionalStatus")]
        public IHttpActionResult CheckTransactionalStatus(transactional_sk_model model)
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.CheckTransactionalStatus(model));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "CheckTransactionalStatus()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetUserRoles")]
        public IHttpActionResult GetUserRoles()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetUserRoles());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetUserRoles()");
                return null;
            }
        }

        [HttpGet]
        [Route("Users")]
        public IHttpActionResult Users()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetUsers());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Users()");
                return null;
            }
        }
        [HttpGet]
        [Route("Branches")]
        public IHttpActionResult Branches()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetBranches());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Branches()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetRcivrlst")]
        public IHttpActionResult GetRcivrlst()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetRcivrlst());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetRcivrlst()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetBankCode")]
        public IHttpActionResult GetBankCode()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetBankCode());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetBankCode()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetReportType")]
        public IHttpActionResult GetReportType()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetReportType());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReportType()");
                return null;
            }
        }
        
        [HttpGet]
        [Route("GetReserve/{doc_typ_sk}")]
        public IHttpActionResult GetReserve(int doc_typ_sk)
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetReserve(doc_typ_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReserve()");
                return null;
            }
        }
        [HttpGet]
        [Route("MT202_Dbt_Act")]
        public IHttpActionResult MT202_Dbt_Act()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.MT202_Dbt_Act());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "MT202_Dbt_Act()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetAccount/{BIC}")]
        public IHttpActionResult GetAccount(string BIC)
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetAccount(BIC));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetAccount()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetBankingPrioritylst")]
        public IHttpActionResult GetBankingPrioritylst()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetBankingPrioritylst());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetBankingPrioritylst()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetTxnCodelst/{Dr_Cr}/{Msg_Flw}/{Doc_typ_SK}")]
        public IHttpActionResult GetTxnCodelst(char Dr_Cr,char Msg_Flw, int Doc_typ_SK)
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetTxnCodelst(Dr_Cr,Msg_Flw,Doc_typ_SK));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetTxnCodelst()");
                return null;
            }
        }

        [HttpGet]
        [Route("Getcrncycde")]
        public IHttpActionResult Getcrncycde()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.Getcrncycde());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Getcrncycde()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetChart")]
        public IHttpActionResult GetChart()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.getchart());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetChart()");
                return null;
            }
        }        

        [HttpGet]
        [Route("Getsndrtorecvlst")]
        public IHttpActionResult Getsndrtorecvlst()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.Getsndrtorecvlst());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Getsndrtorecvlst()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetdoctypcatList")]
        public IHttpActionResult GetdoctypcatList()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetdoctypcatList());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetdoctypcatList()");
                return null;
            }
        }

        [HttpGet]
        [Route("GetPoolDocsByDocTypSk/{doc_typ_sk}/{mst_sk}")]
        public IHttpActionResult GetPoolDocsByDocTypSk(int doc_typ_sk, int mst_sk)
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetPoolDocsByDocTypSk(doc_typ_sk, mst_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetPoolDocsByDocTypSk()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetTransDocsByDocTypSk/{doc_typ_sk}/{mst_sk}")]
        public IHttpActionResult GetTransDocsByDocTypSk(int doc_typ_sk, int mst_sk)
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetTransDocsByDocTypSk(doc_typ_sk, mst_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetTransDocsByDocTypSk()");
                return null;
            }
        }
        [HttpGet]
        [Route("Pool_Category")]
        public IHttpActionResult Pool_Category()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetPool_Category());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Pool_Category()");
                return null;
            }
        }
        [HttpGet]
        [Route("GetLogReportType")]
        public IHttpActionResult GetLogReportType()
        {
            try
            {
                using (DropDownService obj = new DropDownService())
                {
                    return Ok(obj.GetLogReportType());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetLogReportType()");
                return null;
            }
        }
    }
}
