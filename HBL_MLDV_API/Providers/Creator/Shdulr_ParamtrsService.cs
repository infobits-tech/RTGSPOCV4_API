using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models.Common;
using HBL_MLDV_API.Providers;
using System;
using System.Data.SqlClient;

namespace HBL_MLDV_API.Areas.Creator.Controller
{
    internal class Shdulr_ParamtrsService : IDisposable
    {
        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public CustomObject SaveData(msg_ctff_tme_hdr model, int user_sk)
        {
            using (ADOConnection db = new ADOConnection())
            {
                SqlConnection con = db.getDatabaseConnection();
                // Save model
                var resp = db.SaveChanges("Stp_Eng", model);

                if (resp.Message == "Record has been saved successfully")
                {

                }
                return resp;
            }
        }
    }
}