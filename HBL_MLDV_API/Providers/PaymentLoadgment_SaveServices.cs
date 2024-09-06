using HBL_MLDV_API.Models;
using HBL_MLDV_API.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Providers
{
    public class PaymentLoadgment_SaveServices : IDisposable
    {

   
        public CustomObject PaymentLoadgment_Save(Payment_Lodgment_RSPNS model, int user_sk)
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
                            trans.doc_type = "1004";//
                            trans.doc_mst_sk = model.doc_sk.ToString();
                            trans.Controller = "ACHTrans";
                            trans.Created_DateTime = model.Created_DateTime;
                            trans.functionName = "PaymentLoadgment_Save";
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
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}