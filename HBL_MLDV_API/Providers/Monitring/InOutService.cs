using HBL_MLDV_API.Areas.Monitring.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Providers.Monitring
{
    public class InOutService : IDisposable
    {
        DbContextHelper dx = new DbContextHelper();
        public DataTable GetOutward(int userId)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("select * from TR_APP.Vu_Outbound_Monitoring_Queue", con);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }   
        public DataTable GetInward(int userId)
        {
            try
            {
                using (Connection db = new Connection())
                { 
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("select * from TR_APP.Vu_Inbound_Monitoring_Queue", con);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<API_FP_RSPNS> getFprspns(int id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<API_FP_RSPNS> lst = new List<API_FP_RSPNS>();
                    string qry = "select max(RESP_ID) as RESP_ID,max(resp_desc) as RESP_DESC,max(DOC_SK) as DOC_SK from tr_app.Vu_FP_UnSuccessfull where DOC_SK = '" + id + "'";

                    DataTable dt = db.getDataTable(qry, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<API_FP_RSPNS>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
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