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
    public class HoldFunds_saveServices : IDisposable
    {
        public CustomObject HoldFunds_Save(Hold_RSPNS model, int user_sk)
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

        public CustomObject HoldFunds_SaveRQST(Hold_RQST model, int user_sk)//HoldFunds_SaveRQST
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

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}