using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HBL_MLDV_API.App_Start;
using System.Data.SqlClient;
using System.Data;

namespace HBL_MLDV_API.Providers
{
    public class LoginAuthService : IDisposable
    {
        ADOConnection dbconn = new ADOConnection();

        public DataTable GetUsers(string userid)
        {
            CustomObject data = new CustomObject();
            SqlParameter[] objSqlParameter = new SqlParameter[1];
            objSqlParameter[0] = new SqlParameter("@UserId", SqlDbType.VarChar, 15);
            objSqlParameter[0].Value = userid.Trim();
            DataTable dt = dbconn.ExecuteSpDataTable("TR_APP.[GetUserAuth]", objSqlParameter);
            //data.Data = dt.Rows[0]["ROLE_NME"]ToString();
            return dt;
        }
        public string GenerateLink(string User_id)
        {
            DataTable User_data = GetUsers(User_id);
            string Link = "";
            var guid = Guid.NewGuid();
            if (User_data.Rows.Count > 0 && User_data != null)
            {
                string token = guid.ToString().Replace("-", "");
                

            }
            return Link;
        }
        public bool is_exist()
        {

            return true;
        }

        public void Dispose()
        {
        }

    }
}