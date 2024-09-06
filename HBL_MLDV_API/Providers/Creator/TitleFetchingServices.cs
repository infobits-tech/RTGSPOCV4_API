using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
using System.Diagnostics;

namespace HBL_MLDV_API.Providers.Creator
{
    public class TitleFetchingServices : IDisposable
    {
        DbContextHelper dx = new DbContextHelper();
        public TITLFTCHNG_RSPNS GETTrasByID(TITLFTCHNG_RQST tf)
        {
            try
            {

                // request Misys Title Fetch API 
                string rspns = rqst_api(tf);
                TITLFTCHNG_RSPNS aprv = new TITLFTCHNG_RSPNS();
                aprv = JsonConvert.DeserializeObject<TITLFTCHNG_RSPNS>(rspns);
                if (aprv.data != null)
                {
                    aprv.accountNo = aprv.data.accountNo;
                    aprv.accountTitle = aprv.data.accountTitle;
                    aprv.adviceAllCredit = aprv.data.SC011_ADVICEALLCREDIT;
                    aprv.adviceAllDebit = aprv.data.SC012_ADVICEALLDEBIT;
                    aprv.accountCurrency = aprv.data.accountCurrency;
                    aprv.availableBalance = Convert.ToInt64(aprv.data.availableBalance);
                    aprv.email1 = aprv.data.email1;
                    aprv.email2 = aprv.data.email2;
                    aprv.customerAddress1 = aprv.data.customerAddress1;
                    aprv.customerAddress2 = aprv.data.customerAddress2;
                    aprv.customerAddress3 = aprv.data.customerAddress3;
                    aprv.API_STS_CD = aprv.data.responseCode;
                }
                    aprv.API_STS_DESC = aprv.message;
                aprv.RQST_ID = tf.RQST_ID;
                aprv.stream = rspns;
                return aprv;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public string rqst_api(TITLFTCHNG_RQST model)
        {
            var ACC = model.CUST_ACCT_NBR.Trim();
            var rqst_id = DateTime.Now.ToString("yyMMddHHmmssfff");
            if (ConfigurationManager.AppSettings["IS_TTL_FTCH"].ToString() == "N")
            {
                //following code will be removed on api implemetation
                #region Demo Code
                string response = "";
                rqst_id = DateTime.Now.ToString("yyMMddHHmmssfff");
                if (ACC == "123456789")
                {
                    using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "json.json"))
                    {
                        response = sr.ReadToEnd();
                    }
                }
                else if (ACC == "987654321")
                {
                    using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "json1.json"))
                    {
                        response = sr.ReadToEnd();
                    }
                }
                else
                {

                    using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "error1.json"))
                    {
                        response = sr.ReadToEnd();
                    }
                }
                return response;
                #endregion
            }
            else
            {
                //following code will be uncomment on api implemetation
                #region API Code

                //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                Process process = new Process();
                process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["TITLEFETCH_API_LOC"].ToString(); // Replace with the actual console application name
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.GetDirectoryName(ConfigurationManager.AppSettings["TITLEFETCH_API_LOC"].ToString()); ;
                // Start the process
                process.Start();

                // Write input to the console application
                //string input = "2050007000469801";
                process.StandardInput.WriteLine(ACC);
                process.StandardInput.Flush();
                process.StandardInput.Close();

                // Read the response from the console application
                string response = process.StandardOutput.ReadToEnd();

                // Wait for the process to exit
                process.WaitForExit();
                // Close the process
                process.Close();

                //var client = new RestClient("https://apis1.hbl.com:8343/mld/api/v2/account/titlefetch/" + ACC + "");
                //client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
                //var request = new RestRequest(Method.GET);
                //request.AddHeader("x-channel-id", "RTGS");
                //request.AddHeader("x-country-code", "MLDV");
                //request.AddHeader("x-req-id", "" + rqst_id + "");
                //request.AddHeader("x-sub-channel-id", "RTGS");
                //request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJrZXkiOiJjOUNpd2IwZUVJMmxuRWhHcDdPbDlpY0l6ZmVVZmppbiJ9.zVRvb1hxm1zYZf7kTF9HhDXO3tEx9E24TYaoONage2s");
                ////request.ss
                //var body = @"";
                //request.AddParameter("application/json", body, ParameterType.RequestBody);
                //IRestResponse response = client.Execute(request);
                //UniversalRepository UniversalRepository = new UniversalRepository();
                //UniversalRepository.WriteException("IsSuccessful  " + response.IsSuccessful+ "  status: " + response.StatusCode + " =1> " + response.Content.ToString() + "ac: " + ACC + "rqst_id: " +rqst_id, "LoginAPI");
                //UniversalRepository.WriteException(response.Content.ToString(), "LoginAPI");
                //ServicePointManager.ServerCertificateValidationCallback = null;
                return response.ToString();
                #endregion
            }
        }
        //public List<TITLFTCHNG_RSPNS> GetDataById(int mst_id)
        //{
        //    try
        //    {
        //        using (Connection db = new Connection())
        //        {
        //            List<TITLFTCHNG_RSPNS> aprv = new List<TITLFTCHNG_RSPNS>();
        //            SqlConnection con = db.getDatabaseConnection();
        //            // Get user aprv dt

        //            var dt = db.getDataTable("SELECT * FROM TR_APP.TITLFTCHNG_RSPNS WHERE IS_ACTV = 1 and DOC_SK = " + mst_id, con);
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
        //                aprv = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt);
        //            }
        //            return aprv;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public void Dispose()
        {

        }
    }
    //public class NoSslVerificationHandler : HttpClientHandler
    //{
    //    protected override bool ServerCertificateCustomValidationCallback(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain certificateChain, SslPolicyErrors sslPolicyErrors)
    //    {
    //        // Always return true to bypass SSL verification
    //        return true;
    //    }
    //}
}