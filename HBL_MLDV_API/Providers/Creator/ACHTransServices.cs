using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Repository;
using HBL_MLDV_API.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Creator
{
    public class ACHTransServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        DbContextHelper dx = new DbContextHelper();
        public DataTable GetData(int usr_id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<ACH_mst> lst = new List<ACH_mst>();

                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("SELECT * FROM TR_APP.ACH_mst where IS_Deleted = 0", con);
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_mst where IS_Deleted = 0 and CRETD_BY = "+usr_id+"", con);

                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    // Convert to user aprv model

                    //    var aprv = dx.ConvertDataTable<ACH_mst>(dt);

                    //    lst.AddRange(aprv);
                    //}
                    return dt;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }
        public CustomObject SaveData(ACH_mst model, int user_sk)
        {
            try
            {
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    model.CRETD_BY = user_sk;
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("TR_APP", model, true);


                    if (resp.status)
                    {
                        //TransactionsLog Insert
                        string doc_mst_sk = "0";
                        if (resp.Data != null && resp.Data.Rows.Count > 0)
                        {
                            doc_mst_sk = resp.Data.Rows[0]["ach_mst_id"].ToString();
                        }

                        using (UniversalRepository rep = new UniversalRepository())
                        {
                            //string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                            //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                            //TransLogs trans = new TransLogs();
                            //trans.doc_type = "1001";
                            //trans.doc_mst_sk = doc_mst_sk;
                            //trans.Controller = "ACHTrans";
                            //trans.Created_DateTime = model.CRETD_ON;
                            //trans.functionName = "SaveData";
                            //trans.Rspns_Status_code = resp.status == true ? "200" : "400";
                            //trans.rspns_result = resp.Message;

                            //rep.Translogs("TR_APP", trans);

                        }
                        //
                    }

                    return resp;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData()");
                return null;
            }
        }
        public CustomObject SaveDatafp(Transaction_Posting model, int user_sk)
        {
            try
            {
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("TR_APP", model);


                    if (resp.status)
                    {
                        //TransactionsLog Insert



                        using (UniversalRepository rep = new UniversalRepository())
                        {
                            //string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                            //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                            TransLogs trans = new TransLogs();
                            trans.doc_type = "1004";
                            trans.doc_mst_sk = model.doc_sk;
                            trans.Controller = "ACHTrans";
                            trans.Created_DateTime = model.Created_DateTime;
                            trans.functionName = "SaveDatafp";
                            trans.Rspns_Status_code = resp.status == true ? "200" : "400";
                            trans.rspns_result = resp.Message;

                            rep.Translogs("TR_APP", trans);

                        }
                        //
                    }

                    return resp;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int updateCancle(int ach_mst_Id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    var id = db.doInsertUpdate("update TR_APP.ACH_mst set IS_Deleted = 1 where ach_mst_Id =" + ach_mst_Id, con);


                    return id;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public ACH_mst GetDataById(int mst_id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    ACH_mst aprv = new ACH_mst();
                    ACH_dtl dtl = new ACH_dtl();
                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("SELECT * FROM TR_APP.ACH_mst WHERE ach_mst_Id = " + mst_id, con);
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_mst WHERE ach_mst_Id = " + mst_id, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<ACH_mst>(dt)[0];
                        //dt = db.getDataTable("SELECT * FROM TR_APP.ACH_dtl WHERE ach_mst_Id = " + mst_id, con);
                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_dtl WHERE ach_mst_Id = " + mst_id, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aprv.dtl = new List<ACH_dtl>();
                            aprv.dtl = dx.ConvertDataTable<ACH_dtl>(dt);
                        }

                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_TITLFTCHNG_RSPNS WHERE IS_ACTV = 1 and DOC_SK = " + mst_id, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aprv.Title_Fetching_RSPNS = new TITLFTCHNG_RSPNS();
                            aprv.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt)[0];
                        }

                        dt = db.getDataTable("select * from TR_APP.Vu_HOLD_RSPNS where RSPNS_ID = (select max(RSPNS_ID) from TR_APP.Vu_HOLD_RSPNS where DOC_SK='" + mst_id + "')", con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aprv.Hold_RSPNS = new Hold_RSPNS();
                            aprv.Hold_RSPNS = dx.ConvertDataTable<Hold_RSPNS>(dt)[0];
                            //arpv.Title_Fetching_RSPNS.
                        }
                    }
                    return aprv;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataById()");
                return null;
            }
        }
        public ACH_mst GetDataForEdit(int mst_id, int req_ID)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    ACH_mst aprv = new ACH_mst();
                    ACH_dtl dtl = new ACH_dtl();
                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("SELECT * FROM TR_APP.ACH_mst WHERE ach_mst_Id = " + mst_id, con);
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_mst WHERE ach_mst_Id = " + mst_id, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<ACH_mst>(dt)[0];
                        //dt = db.getDataTable("SELECT * FROM TR_APP.ACH_dtl WHERE ach_mst_Id = " + mst_id, con);
                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_dtl WHERE ach_mst_Id = " + mst_id, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aprv.dtl = new List<ACH_dtl>();
                            aprv.dtl = dx.ConvertDataTable<ACH_dtl>(dt);
                        }

                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_TITLFTCHNG_RSPNS WHERE IS_ACTV = true and DOC_SK = " + mst_id + "and RQST_ID =" + req_ID, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aprv.Title_Fetching_RSPNS = new TITLFTCHNG_RSPNS();
                            aprv.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt)[0];
                        }
                    }


                    return aprv;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataForEdit()");
                return null;
            }
        }

        public DataTable CurrencyCheck(int mst_id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<ACH_mst> lst = new List<ACH_mst>();

                    DataTable dt = db.getDataTable("select * from Tr_app.Vu_ACH_CurrencyCheck where ach_mst_Id= " + mst_id + "", con);
                    return dt;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }

        public DataTable CheckSenderRef(string sender_ref)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<ACH_mst> lst = new List<ACH_mst>();

                    DataTable dt = db.getDataTable("select ach_mst_Id from TR_APP.VU_ach_mst where STS_CDE not like '115%' and msg_id= \'" + sender_ref + "\'", con);
                    return dt;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }

        public void Dispose()
        {

        }


    }
}