using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Areas.Monitring.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Monitring
{
    public class OutwardMntringServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        public List<Vu_Outward_Monitoring_Queue_New> GetData(string msg_type, string inOrOut, string FromDate, string ToDate, string BankCode)
        {
            try
            {
                DbContextHelper dx = new DbContextHelper();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<Vu_Outward_Monitoring_Queue_New> lst = new List<Vu_Outward_Monitoring_Queue_New>();
                    DataTable dt = new DataTable();
                    // Get user aprv dt

                    if(inOrOut.ToLower() == "inward")
                    {
                        msg_type = msg_type.Remove(0,2);
                    }
                    if (BankCode != "0")
                    {
                        dt = db.getDataTable("SELECT * FROM TR_APP.Vu_" + inOrOut + "_Monitoring_Queue_New where MSGTYPe='" + msg_type + "' AND (convert(varchar, cast(msg_time as date), 110) between '" + FromDate + "' and '" +  ToDate + "') AND BeneficiaryBankCode='" + BankCode + "'", con);
                    }
                    else
                    {
                        string query = "SELECT * FROM TR_APP.Vu_" + inOrOut + "_Monitoring_Queue_New where MSGTYPe = '" + msg_type + "' AND (convert(varchar, cast(msg_time as date), 110) between '" + FromDate + "' and '" + ToDate  + "')";
                        dt = db.getDataTable(query, con);
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<Vu_Outward_Monitoring_Queue_New>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }

        public List<Vu_Log_UserDeatils> GetLogData(string reportId, string FromDate, string ToDate)
        {
            try
            {
                DbContextHelper dx = new DbContextHelper();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<Vu_Log_UserDeatils> lst = new List<Vu_Log_UserDeatils>();

                    DataTable dt = new DataTable();
                    string query = "";
                    if (reportId == "1")
                    {
                        query = "SELECT * FROM Security.Vu_Log_UserDeatils "; //where MSGTYPe = '" + msg_type + "' AND (convert(varchar, cast(msg_time as date), 110) between '" + FromDate + "' and '" + ToDate + "')
                    }
                    else if (reportId == "2")
                    {
                        query = "SELECT * FROM Security.Vu_Log_User_Login_History where (convert(varchar, cast(User_Login_DateTime as date), 110) between '" + FromDate + "' and '" + ToDate + "') order by User_Login_DateTime desc";
                    }
                    else if (reportId == "3")
                    {
                        query = "SELECT * FROM Security.Vu_Log_InvalidAccess where (convert(varchar, cast(Access_Datetime as date), 110) between '" + FromDate + "' and '" + ToDate + "') order by Access_Datetime desc";
                    }
                    else if (reportId == "4")
                    {
                        query = "SELECT * FROM Security.Vu_Log_UserNavLog where (convert(varchar, cast(Nav_Datetime as date), 110) between '" + FromDate + "' and '" + ToDate + "') order by Nav_Datetime desc";
                    }
                    dt = db.getDataTable(query, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        var aprv = dx.ConvertDataTable<Vu_Log_UserDeatils>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }
        public List<MessageReport> GetDataMessage(string doc_sk)
        {
            try
            {
                DbContextHelper dx = new DbContextHelper();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<MessageReport> lst = new List<MessageReport>();

                    // Get user aprv dt
                    DataTable dt = db.GetProcedureRecord("TR_APP", "Doc_Wise_Report", "@Doc_sk" , Convert.ToInt32(doc_sk)); //15139

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<MessageReport>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataMessage()");
                return null;
            }
        }

        public List<MessageReport> GetDataInward(string doc_sk)
        {
            try
            {
                DbContextHelper dx = new DbContextHelper();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<MessageReport> lst = new List<MessageReport>();

                    // Get user aprv dt
                    DataTable dt = db.GetProcedureRecord("TR_APP", "Doc_Wise_Report_inbound", "@Doc_sk", Convert.ToInt32(doc_sk)); //15139

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<MessageReport>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataInward()");
                return null;
            }
        }

        public List<vu_Reports_In_Out> GetReportName(string reportid)
        {
            try
            {
                DbContextHelper dx = new DbContextHelper();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<vu_Reports_In_Out> lst = new List<vu_Reports_In_Out>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.vu_Reports_In_Out where Report_Id="+ reportid + "", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<vu_Reports_In_Out>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReportName()");
                return null;
            }
        }

        public List<vu_Reports_In_Out> GetLogReportName(string reportid)
        {
            try
            {
                DbContextHelper dx = new DbContextHelper();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<vu_Reports_In_Out> lst = new List<vu_Reports_In_Out>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.vu_Reports_Logs where Report_Id=" + reportid + "", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<vu_Reports_In_Out>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReportName()");
                return null;
            }
        }
        public void Dispose()
        {
        }
    }
}