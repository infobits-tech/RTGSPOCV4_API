using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Configuration;
using System.Text;
using System.Data.SqlClient;
using HBL_MLDV_API.Areas.Approval.Models;

namespace HBL_MLDV_API.Repository
{
    public class UniversalRepository : IDisposable
    {
        public int Draft = 1;
        public int Approved = 2;
        public int Proceed = 6;
        public int Rejected = 7;
        public int Pending = 8;
        public int Invalid = 9;
      
        DbContextHelper _ctx = new DbContextHelper();
        
        public DateTime GetDateTimeForTimeZone()
        {
            DateTime UAE = DateTime.Now;

            if (WebConfigurationManager.AppSettings["timezone"] != null)
            {
                string timezone = WebConfigurationManager.AppSettings["timezone"].ToString();

                if (!string.IsNullOrWhiteSpace(timezone))
                {
                    TimeZoneInfo UAETimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    DateTime utc = DateTime.UtcNow;
                    UAE = TimeZoneInfo.ConvertTimeFromUtc(utc, UAETimeZone);
                }
            }

            return UAE;
        }
        
        public void WriteException(string error, string process, [CallerMemberName] string method = "")
        {
            if (!Directory.Exists(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\")))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\");
            }

            string filelocation = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ErrorLog\\" + DateTime.Now.ToString("ddMMyyy") + "_" + process + ".txt";

            if (!Directory.Exists(Path.GetDirectoryName(filelocation)))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ErrorLog\\");
            }

            if (!File.Exists(filelocation))
            {
                using (StreamWriter sw = File.CreateText(filelocation))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(method.ToString());
                    sw.WriteLine("------------------------------");
                    sw.WriteLine(error);
                    sw.WriteLine("------------------------------");

                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filelocation))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(method.ToString());
                    sw.WriteLine("------------------------------");
                    sw.WriteLine(error);
                    sw.WriteLine("------------------------------");
                    sw.Close();
                }
            }
        }

        public void WriteProcess(string message, string process, [CallerMemberName] string method = "")
        {
            if (!Directory.Exists(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\")))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\");
            }

            string filelocation = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Process\\" + DateTime.Now.ToString("ddMMyyy") + "_" + process + ".txt";

            if (!Directory.Exists(Path.GetDirectoryName(filelocation)))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Process\\");
            }

            if (!File.Exists(filelocation))
            {
                using (StreamWriter sw = File.CreateText(filelocation))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(method.ToString());
                    sw.WriteLine("------------------------------");
                    sw.WriteLine(message);
                    sw.WriteLine("------------------------------");

                    sw.Close();
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filelocation))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                    sw.WriteLine(method.ToString());
                    sw.WriteLine("------------------------------");
                    sw.WriteLine(message);
                    sw.WriteLine("------------------------------");
                    sw.Close();
                }
            }
        }
        // Instantiate random number generator.  
        private readonly Random _random = new Random();

        // Generates a random string with a given size.    
        public string RandomString(int size = 10, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public string GetDocNbr(string doctyp)
        {
            DateTime UAE = DateTime.Now;

            if (WebConfigurationManager.AppSettings["timezone"] != null)
            {
                string timezone = WebConfigurationManager.AppSettings["timezone"].ToString();

                if (!string.IsNullOrWhiteSpace(timezone))
                {
                    TimeZoneInfo UAETimeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    DateTime utc = DateTime.UtcNow;
                    UAE = TimeZoneInfo.ConvertTimeFromUtc(utc, UAETimeZone);
                }
            }

            return "";
        }
        public void Dispose()
        {

        }
        public void Translogs(string schema, TransLogs translogs)
        {
            try
            {
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges(schema, translogs);

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void InvalidLogin(string errormsg,string IP, string machinename,string url = "Login")
        {
            try
            {
                InvalidLogin Log = new InvalidLogin
                {
                    ERRORMSG = errormsg,
                    IPADDRESS = IP,
                    MACHINE_NAME = machinename,
                    URL= url,
                    Timestamp = DateTime.Now
                };
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("security", Log);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void GenerateSetupLog(UserSetupLog Log)
        {
            try 
            {                
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("security", Log);
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void NavigationLog(int USR_ID, string URL,string DynamicUrl)
        {
            try
            {
                UserNavLog Log = new UserNavLog
                {
                    USER_ID = USR_ID,
                    PageURL = URL,
                    DYNMC_URL=DynamicUrl,
                    Timestamp = DateTime.Now
                };
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("security", Log);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void InvalidNavigationLog(int USR_ID, string URL, string DynamicUrl)
        {
            try
            {
                InvalidUserNavLog Log = new InvalidUserNavLog
                {
                    USER_ID = USR_ID,
                    PageURL = URL,
                    DYNMC_URL = DynamicUrl,
                    Timestamp = DateTime.Now
                };
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("security", Log);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public CustomObject CreatePoolRecord(int br_code, int user_sk, string doc_no, DateTime created_on, int created_by, string doc_link, int doc_typ_sk, int aprv_cat_sk)
        {
            vu_pool_docs poolModel = new vu_pool_docs();
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable("select * from setup.vu_pool_docs where doc_typ_sk = " + doc_typ_sk + " and doc_mst_sk= " + user_sk + " and approval_status_sk = 8",con);
                if (dt != null && dt.Rows.Count > 0)
                {
                    poolModel = db.ConvertDataTable<vu_pool_docs>(dt)[0];
                    poolModel.doc_dte = created_on;
                    poolModel.state = "Changed";
                }
                else
                {
                    poolModel = new vu_pool_docs();
                    poolModel.br_sk = br_code;
                    poolModel.doc_no = doc_no;
                    poolModel.doc_typ_sk = doc_typ_sk;
                    poolModel.doc_mst_sk = user_sk;
                    poolModel.doc_dte = created_on;
                    poolModel.aprv_cat_sk = aprv_cat_sk;
                    poolModel.doc_link = doc_link;
                    poolModel.created_by = created_by;
                    poolModel.create_dt_tme = GetDateTimeForTimeZone();
                    poolModel.approval_status = "Pending";
                    poolModel.approval_status_sk = 8;
                    poolModel.txn_amt = 1;
                    poolModel.row_version = 0;
                    poolModel.record_status = 0;
                }

                // Save pool model

                CustomObject obj = db.SaveChanges("setup", poolModel);
                return obj;
            }
        }
    }
}