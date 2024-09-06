using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using HBL_MLDV_API.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Providers
{
    public class TitleFetching_saveServices : IDisposable
    {

        public CustomObject TitleFetching_Save(TITLFTCHNG_RSPNS model, int user_sk)
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



                        //using (UniversalRepository rep = new UniversalRepository())
                        //{
                        //    //string ControllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                        //    //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                        //    TransLogs trans = new TransLogs();
                        //    trans.doc_type = "1004";//
                        //    //trans.doc_mst_sk = model.DOC_SK.ToString();
                        //    trans.Controller = "ACHTrans";
                        //    //trans.Created_DateTime = model.CRETD_ON;
                        //    trans.functionName = "TitleFetching_Save";
                        //    trans.Rspns_Status_code = resp.status == true ? "200" : "400";
                        //    trans.rspns_result = resp.Message;
                        //
                        //    rep.Translogs("TR_APP", trans);
                        //
                        //}
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
        public CustomObject TitleFetching_SaveRQST(TITLFTCHNG_RQST model, int user_sk)//TitleFetching_SaveRQST
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

                    }

                    return resp;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int updateData(int IS_ACTV)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //var id = db.doInsertUpdate("update TR_APP.TITLFTCHNG_RSPNS set IS_ACTV = 0 where DOC_SK =" + IS_ACTV, con);


                    return IS_ACTV;
                }
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}