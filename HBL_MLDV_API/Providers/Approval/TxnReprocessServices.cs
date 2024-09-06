using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Areas.Approval.Models;
using HBL_MLDV_API.Areas.UserManagement.Models;
using HBL_MLDV_API.Areas.UserManagement.Models.Role;
using HBL_MLDV_API.Models.Common;
using HBL_MLDV_API.Models.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Approval
{
    public class TxnReprocessServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        ADOConnection db = new ADOConnection();
        SqlConnection con = new SqlConnection();
        DbContextHelper dx = new DbContextHelper();

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public DataTable GetOutwardTxn()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM TR_APP.Vu_Get_TXN_DTL_INPUT_REPROCESS", con);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetOutwardTxn()");
                return null;
            }
        }

        public DataTable GetInwardTxn()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM TR_APP.Vu_Get_Inbound_TXN_DTL_REPROCESS", con);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetInwardTxn()");
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

        public Hold_RSPNS GetHoldCode(int Doc_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    Hold_RSPNS lst = new Hold_RSPNS();
                    string qry = "select * from TR_APP.Vu_hold_FP where DOC_SK='" + Doc_sk + "'";
                    // Get user aprv dt
                    DataTable dt = db.getDataTable(qry, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        //lst.Api_type = dt.Rows[0]["Api_type"].ToString();
                        lst.REPHOLDCODE = (int)dt.Rows[0]["REPHOLDCODE"];
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "CheckHold()");
                return null;
            }
        }
    }
}