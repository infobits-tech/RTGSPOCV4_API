using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.IO;

namespace HBL_MLDV_API.Providers.Creator
{
    public class inward_rejected_txn_Services : IDisposable
    {
        DbContextHelper dx = new DbContextHelper();
        UniversalRepository universalRepository = new UniversalRepository();

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public List<GET_RJCT_TXN> GetData(int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<GET_RJCT_TXN> lst = new List<GET_RJCT_TXN>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM stp_eng.VU_GET_RJCT_TXN", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<GET_RJCT_TXN>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "MT103Getdata");
                return null;
            }
        }
        public CustomObject CreateRefund(int Doc_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    //dx.SelectDataTable("delete from \"setup\".pool_users_map where pool_sk = "+model.pool_sk+";" 
                    //    + "delete from \"setup\".pool_branch_map where pool_sk = " + model.pool_sk + ";"
                    //    + "delete from \"setup\".pool_cat_map where pool_sk = " + model.pool_sk + ";");
                    //following code will be uncomment on api implemetation
                    #region API Code

                    //ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                    Process process = new Process();
                    process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["REFUND_API_LOC"].ToString(); // Replace with the actual console application name
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.GetDirectoryName(ConfigurationManager.AppSettings["REFUND_API_LOC"].ToString());
                    // Start the process
                    process.Start();

                    // Write input to the console application
                    //string input = "2050007000469801";
                    process.StandardInput.WriteLine(Doc_sk.ToString());
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
                    CustomObject obj = new CustomObject();
                    obj.Data = null;
                    obj.Message = response.ToString();

                    return obj;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                CustomObject obj = new CustomObject();
                obj.Data = null;
                obj.Message = "Exception: Please contact system administrator";

                return obj;
            }
        }

    }
}