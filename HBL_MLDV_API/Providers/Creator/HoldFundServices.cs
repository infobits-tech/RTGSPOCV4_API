using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using HBL_MLDV_API.Areas.Creator.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using RestSharp;
using System.Configuration;
using HBL_MLDV_API.Repository;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace HBL_MLDV_API.Providers.Creator
{
    public class HoldFundServices : IDisposable
    {
        DbContextHelper dx = new DbContextHelper();
        UniversalRepository un = new UniversalRepository();

        public Hold_RSPNS GETTrasByID(Hold_RQST tf)
        {
            try
            {
                string rspns;
                // request Misys Title Fetch API 
                if (ConfigurationManager.AppSettings["IS_TTL_FTCH"].ToString() == "N")
                {
                    using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Holdjson.json"))
                    {
                        rspns = sr.ReadToEnd();
                    }
                }
                else
                {
                    rspns = rqst_api(tf);
                }
                //un.WriteException(rspns, "GETTrasByID(HoldFunds)");
                Hold_RSPNS aprv = new Hold_RSPNS();
                //un.WriteException(rspns, "GETTrasByID(HoldFunds)");
                //rspns = @"{{"REPCODE": "R","REPMESSAGE": "","REPHOLDCODE": "001","UNQREFERENCE1": "0123456789"}}";
                aprv = JsonConvert.DeserializeObject<Hold_RSPNS>(rspns);
                if (aprv.Data != null)
                {
                    aprv.REPCODE = aprv.Data.responseCode; //aprv.da.accountNo;
                    aprv.REPMESSAGE = aprv.Data.responseDescription; //aprv.data.accountTitle;
                    aprv.REPHOLDCODE = aprv.Data.repHoldCode; //aprv.data.adviceAllCredit;
                    aprv.UNQREFERENCE1 = aprv.Data.uniqueReferenceId; //aprv.data.adviceAllDebit;
                }
                aprv.API_STS_DESC = aprv.message;
                aprv.Api_type = tf.Api_type;
                aprv.Stream = rspns;
                return aprv;
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GETTrasByID(HoldFunds)");
                return null;
            }
        }

        public string rqst_api(Hold_RQST model)
        {
            #region API Code
            var body = @"{
                " +
                    @"    ""accountNo"": ""{ACCOUNTNO}"",
                " +
                    @"    ""transactionAmount"": ""{TRXAMOUNT}"",
                " +
                    @"    ""narration1"": ""{NARLINE1}"",
                " +
                    @"    ""narration2"": ""-"",
                " +
                    @"    ""narration3"": ""-"",
                " +
                    @"    ""narration4"": ""-"",
                " +
                    @"    ""branch"": ""{branch}""
" + @"}";
            string response = "";
            try
            {
                //body = body.Replace("{UNQREFERENCE}", model.UNQREFERENCE);
                body = body.Replace("{ACCOUNTNO}", model.ACCOUNTNO.Replace("/", ""));
                body = body.Replace("{TRXAMOUNT}", model.TRXAMOUNT);
                body = body.Replace("{branch}", ConfigurationManager.AppSettings["HoldBranchCode"].ToString());
                body = body.Replace("{NARLINE1}", "MT103_Outward_HoldFunds");
                //un.WriteException(body, "GETTrasByID(HoldFunds)");
                //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                Process process = new Process();
                process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["HOLDFUNDS_API_LOC"].ToString(); // Replace with the actual console application name
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.GetDirectoryName(ConfigurationManager.AppSettings["HOLDFUNDS_API_LOC"].ToString());
                // Start the process
                process.Start();

                // Write input to the console application
                //string input = "2050007000469801";
                process.StandardInput.WriteLine(body);
                process.StandardInput.Flush();
                process.StandardInput.Close();

                // Read the response from the console application
                response = process.StandardOutput.ReadToEnd();

                // Wait for the process to exit
                process.WaitForExit();
                // Close the process
                process.Close();

                return response.ToString();
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "HoldFunds");
                return ex.ToString();
            }

            #endregion

        }

        public void Dispose()
        {

        }
    }
}