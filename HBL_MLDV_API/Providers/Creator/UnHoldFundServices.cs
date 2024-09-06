using HBL_MLDV_API.Models;
using HBL_MLDV_API.Areas.Creator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace HBL_MLDV_API.Providers.Creator
{
    public class UnHoldFundServices : IDisposable
    {
        DbContextHelper dx = new DbContextHelper();

        public Hold_RSPNS GETTrasByID(Hold_RQST tf)
        {
            try
            {
                // request Misys Title Fetch API 
                string rspns;
                // request Misys Title Fetch API 
                if (ConfigurationManager.AppSettings["IS_TTL_FTCH"].ToString() == "N")
                {
                    rspns = @"{""OUTPUTPARM"":{""REPCODE"":""0000000"",""REPMESSAGE"":"""",""UNQREFERENCE1"":""230719169713290""}}";
                }
                else
                {
                    rspns = rqst_api(tf);
                }

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
                         @"    ""holdCode"": ""{HOLDCODE}"",
                " +
                         @"    ""branch"": ""1002""
" +@"}";
            string response = "";
            try
            {
                body = body.Replace("{ACCOUNTNO}", model.ACCOUNTNO.Replace("/", ""));
                body = body.Replace("{HOLDCODE}", model.HOLDCODE);
                body = body.Replace("{TRXAMOUNT}", model.TRXAMOUNT);

                //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                Process process = new Process();
                process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["HOLDFUNDS_API_LOC"].ToString(); ; // Replace with the actual console application name
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
                //string Infb_ID = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"); //11/24/2022 2:49:40 PM 

                return response.ToString();
            }
            catch (Exception e)
            {
                return response;
            }
            #endregion
        }

        public void Dispose()
        {


        }
    }
}