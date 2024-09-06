using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using HBL_MLDV_API.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Creator
{
    public class ACHInwardServices : IDisposable
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
                    List<ACH_MST_Inward> lst = new List<ACH_MST_Inward>();

                    // Get user aprv dt                    
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.VU_ACH_MST_Inward", con);

                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    // Convert to user aprv model

                    //    var aprv = dx.ConvertDataTable<ACH_MST>(dt);

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
        public CustomObject SaveData(ACH_MST_Inward model, int user_sk)
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
                            doc_mst_sk = resp.Data.Rows[0]["ACH_MST_id"].ToString();
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
        public int updateCancle(int ACH_MST_Id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    var id = db.doInsertUpdate("update TR_APP.ACH_MST_Inward set IS_Deleted = 1 where ACH_MST_Id =" + ACH_MST_Id, con);


                    return id;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public ACH_MST_Inward GetDataById(int mst_id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    ACH_MST_Inward aprv = new ACH_MST_Inward();
                    ACH_DTL_Inward dtl = new ACH_DTL_Inward();
                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("SELECT * FROM TR_APP.ACH_MST WHERE ACH_MST_Id = " + mst_id, con);
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_MST_Inward WHERE ACH_MST_Id = " + mst_id, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<ACH_MST_Inward>(dt)[0];
                        //dt = db.getDataTable("SELECT * FROM TR_APP.ACH_dtl WHERE ACH_MST_Id = " + mst_id, con);
                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_dtl_Inward WHERE ACH_MST_Id = " + mst_id, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aprv.dtl = new List<ACH_DTL_Inward>();
                            aprv.dtl = dx.ConvertDataTable<ACH_DTL_Inward>(dt);
                        }
                        foreach (var item in aprv.dtl)
                        {
                            item.Title_Fetching_RSPNS = new TITLFTCHNG_RSPNS();
                            dt = db.getDataTable("SELECT * FROM TR_APP.Vu_TITLFTCHNG_RSPNS WHERE IS_ACTV = 1 and DOC_SK in (" + item.Id+ ")", con);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                item.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt)[0];
                            }
                        }
                        //aprv.Title_Fetching_RSPNS = new List<TITLFTCHNG_RSPNS>();
                        //string a = "";
                        //foreach (var i in aprv.dtl)
                        //{
                        //    a += i.Id + ",";
                        //}
                        //dt = db.getDataTable("SELECT * FROM TR_APP.Vu_TITLFTCHNG_RSPNS WHERE IS_ACTV = 1 and DOC_SK in ( " + a.ToString().TrimEnd(',') + ")", con);
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                        //
                        //    aprv.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt);
                        //}
                        //dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_dtl_Inward dtl left join TR_APP.Vu_TITLFTCHNG_RSPNS rspns on dtl.id=rspns.doc_sk WHERE dtl.ACH_MST_Id =" + mst_id, con);
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                        //    aprv.Title_Fetching_RSPNS = new TITLFTCHNG_RSPNS();
                        //    aprv.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt)[0];
                        //}
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

        public DataTable GetCurrencyACH(int mst_id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //ACH_MST_Inward aprv = new ACH_MST_Inward();
                    //ACH_DTL_Inward dtl = new ACH_DTL_Inward();
                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("SELECT * FROM TR_APP.ACH_MST WHERE ACH_MST_Id = " + mst_id, con);
                    DataTable dt = db.getDataTable("select * from TR_APP.ACH_Mst_Inward where ach_mst_Id= " + mst_id, con);

                    //if (dt != null && dt.Rows.Count > 0)
                    //{ 
                    //    // Convert to user aprv model
                    //    aprv = dx.ConvertDataTable<ACH_MST_Inward>(dt)[0];
                    //}
                    return dt;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataById()");
                return null;
            }
        }

        public ACH_MST_Inward GetDataForEdit(int mst_id, int req_ID)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    ACH_MST_Inward aprv = new ACH_MST_Inward();
                    ACH_dtl dtl = new ACH_dtl();
                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("SELECT * FROM TR_APP.ACH_MST WHERE ACH_MST_Id = " + mst_id, con);
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_MST_Inward WHERE ACH_MST_Id = " + mst_id, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<ACH_MST_Inward>(dt)[0];
                        //dt = db.getDataTable("SELECT * FROM TR_APP.ACH_dtl WHERE ACH_MST_Id = " + mst_id, con);
                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_ACH_dtl_Inward WHERE ACH_MST_Id = " + mst_id, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            aprv.dtl = new List<ACH_DTL_Inward>();
                            aprv.dtl = dx.ConvertDataTable<ACH_DTL_Inward>(dt);
                        }

                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_TITLFTCHNG_RSPNS WHERE IS_ACTV = true and DOC_SK = " + mst_id + "and RQST_ID =" + req_ID, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //aprv.Title_Fetching_RSPNS = new TITLFTCHNG_RSPNS();
                            //aprv.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt)[0];
                        }
                    }


                    return aprv;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Dispose()
        {

        }
    }
}