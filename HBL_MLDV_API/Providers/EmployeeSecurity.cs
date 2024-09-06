using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using HBL_MLDV_API.Models.Security;
using HBL_MLDV_API.Repository;
using Npgsql;
using System.Web.Mvc;
using System.Net;

namespace HBL_MLDV_API.Providers
{
    public class EmployeeSecurity : IDisposable
    {
        UniversalRepository UniversalRepository = new UniversalRepository();
        SqlConnection con = new SqlConnection();
        EncryptDecrypt enc = new EncryptDecrypt();
        DbContextHelper dx = new DbContextHelper();
        public bool Login(string userName, string password)
        {
            string[] creds = GetCreds(userName);
            string un = creds[0];
            string pw = creds[1];
            return ValidateLogin(un, pw);
        }

        public int LoginUserAndPwd(string userName, vu_users usr)
        {
            string[] creds = GetCreds(userName);
            string un = creds[0];
            //string pw = creds[1];
            if (usr.username == un)
            {
                return 200;
            }
            else if (usr.username == un)
            {
                return 401;
            }
            else
                return 404;
        }

        public bool GetInvalidLoginCount(int userSk)
        {
            // select* from security.vu_invalid_login_attempt;
            string query = "select * from \"security\".sp_invalid_login_count({0})";
            string countParam = "select param_sk, param_desc,dt_val,char_val,num_val from \"security\".param_dtl where param_sk = {0}";
            query = string.Format(query, userSk);
            countParam = string.Format(countParam, 1);
            using (DbContextHelper db = new DbContextHelper())
            {
                DataTable pDt = new DataTable();
                DataTable dt = new DataTable();
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        dt = db.SelectDataTable(query);
                    }
                    else
                    {
                        pDt = db.SelectDataTable(countParam);
                    }

                }


                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["invalid_cnt"]) >= Convert.ToInt32(pDt.Rows[0]["num_val"]))
                    {
                        return true;
                    }
                    return
                        false;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool ValidateLogin(string username, string password)
        {

            //string query = "select * from \"security\".users where username = '{0}' and password ='{1}' and (record_status::text <> '1'::text OR record_status IS NULL)";
            string query = "select * from \"security\".Vu_users where username = '{0}' and password ='{1}' and (record_status::text <> '1'::text OR record_status IS NULL)";
            query = string.Format(query, username, password);
            using (DbContextHelper db = new DbContextHelper())
            {
                DataTable dt = db.SelectDataTable(query);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public bool ChecklastLoginDate(string usr_nme)
        {
            //var lockoutDate = DateTime.Now.AddDays(-90);
            string query = "select convert(date,Login_DTE_TME,120) as Login_DTE_TME from security.ChecklastLoginDate where USR_ID = '{0}'";
            query = string.Format(query, usr_nme);
            bool islock = false;
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable(query, con);
                con.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dbDateTime = Convert.ToDateTime(dt.Rows[0]["Login_DTE_TME"]);
                    TimeSpan difference = DateTime.Now - dbDateTime;
                    if (difference.TotalDays >= Convert.ToInt32(ConfigurationManager.AppSettings["LastLoginLimit"].ToString()))
                    {
                        //return lock
                        islock = true;
                    }
                    else
                        islock = false;
                }
                else
                {
                    islock = false;
                }
                if (islock)
                {
                    query = "update security.users set status = 'Inactive' , record_status=1 where user_ful_name = '{0}'";
                    query = string.Format(query, usr_nme);
                    con = db.getDatabaseConnection();
                    db.doInsertUpdate(query, con);
                    con.Close();
                }
            }
            return islock;
        }
        public bool GetUserLock(vu_users usr)
        {
            string query = "select  convert(date,lck_dt_tm,120) AS lck_dt_tm from \"security\".vu_user_lock where usr_sk = {0}";
            query = string.Format(query, usr.user_sk);
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable(query, con);
                con.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dbDateTimeTest = Convert.ToDateTime(dt.Rows[0]["lck_dt_tm"].ToString());

                    if (Convert.ToDateTime(dt.Rows[0]["lck_dt_tm"]) <= UniversalRepository.GetDateTimeForTimeZone())
                    {
                        //return lock
                        return false;
                    }
                    else
                        return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void MaintainLoginHistory(login_history histry)
        {
            using (Connection db = new Connection())
            {
                try
                {
                    var res = db.SaveChanges("security", histry);
                }
                catch (Exception ex)
                {


                }
            }
        }
        public void MaintainLogoutHistory(login_history histry)
        {
            using (Connection db = new Connection())
            {
                try
                {
                    SqlConnection con = db.getDatabaseConnection();
                    string qry = "update security.login_history set logout_DTE_TME = '" + histry.Logout_DTE_TME + "' where usr_sk=1 and dynmc_url='" + histry.dynmc_url + "'and login_DTE_TME is not null";
                    int i = db.doInsertUpdate(qry, con);
                }
                catch (Exception ex)
                {


                }
            }
        }

        public void LockUser(user_login_lock lck)
        {
            using (Connection db = new Connection())
            {
                try
                {
                    db.SaveChanges("security", lck);
                }
                catch (Exception ex)
                {


                }
            }
        }

        public vu_users GetUserInfo(string username)
        {

            //string query = "select usr.*,hist.login_dte_tme from security.vu_user_login as usr LEFT JOIN security.login_history hist on  usr.user_sk = hist.usr_sk and usr.dynmc_url=hist.dynmc_url where usr.user_sk='{0}'";
            string query = "select usr.*,hist.login_dte_tme from security.vu_user_login as usr LEFT JOIN security.Vu_login_history hist on  usr.user_sk = hist.usr_sk and usr.dynmc_url=hist.dynmc_url where usr.user_sk='{0}'";
            query = string.Format(query, username);
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable(query, con);
                con.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return new vu_users
                    {
                        user_sk = Convert.ToInt32(dt.Rows[0]["user_sk"]),
                        username = dt.Rows[0]["username"].ToString(),
                        user_full_name = dt.Rows[0]["user_full_name"].ToString(),
                        password = dt.Rows[0]["password"].ToString(),
                        email = dt.Rows[0]["email"].ToString(),
                        //br_code = Convert.ToInt32(dt.Rows[0]["br_code"]),
                        //br_desc = dt.Rows[0]["br_desc"].ToString(),
                        record_status = Convert.ToInt32(dt.Rows[0]["record_status"])
                    };
                }
                else
                {
                    return null;
                }

            }

        }
        public vu_users GetUserInfo(int UsrID)
        {

            string query = "select usr.*,hist.login_dte_tme from security.vu_user_login as usr LEFT JOIN security.login_history hist on  usr.user_sk = hist.usr_sk and usr.dynmc_url=hist.dynmc_url where usr.user_sk='{0}'";
            query = string.Format(query, UsrID);
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable(query, con);
                con.Close();
                if (dt != null && dt.Rows.Count > 0)
                {
                    //return new vu_users
                    //{
                    //    user_sk = Convert.ToInt32(dt.Rows[0]["user_sk"]),
                    //    username = dt.Rows[0]["username"].ToString(),
                    //    user_full_name = dt.Rows[0]["user_full_name"].ToString(),
                    //    password = dt.Rows[0]["password"].ToString(),
                    //    email = dt.Rows[0]["email"].ToString(),
                    //    //br_code = Convert.ToInt32(dt.Rows[0]["br_code"]),
                    //    //br_desc = dt.Rows[0]["br_desc"].ToString(),
                    //    record_status = Convert.ToInt32(dt.Rows[0]["record_status"])
                    //};
                    var usr = dx.ConvertDataTable<vu_users>(dt)[0];
                    return usr;
                }
                else
                {
                    return null;
                }

            }

        }

        public string[] GetCreds(string userName)
        {
            string[] userNamePassword = new string[2];
            try
            {
                var ed = new EncryptDecrypt();
                userNamePassword[0] = ed.Encrypt(userName);
                //userNamePassword[1] = ed.Encrypt(Password);
                return userNamePassword;
            }
            catch (Exception ex)
            {

            }
            return userNamePassword;
        }

        public async Task<vu_users> GetEmployee(string userName)
        {
            vu_users uid = new vu_users();
            string[] creds = GetCreds(userName);
            string uname = creds[0];
            await Task.Run(() =>
            {
                if (userName != null)
                {

                    uid = GetUserInfo(uname);

                }
            });
            return uid;
        }
        public async Task<vu_users> GetEmployeeDynmc(string dynmcUrl)
        {
            //vu_users uid = new vu_users();
            string query = "select * from security.vu_user_login where dynmc_url = '{0}'";
            query = string.Format(query, dynmcUrl);
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable(query, con);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var usr = dx.ConvertDataTable<vu_users>(dt)[0];
                    usr.user_full_name = dt.Rows[0]["user_ful_name"].ToString();
                    if (!usr.is_login)
                    {
                        int upt = db.doInsertUpdate("update security.users set is_login='1' where id=" + usr.user_sk, con);
                    }
                    return usr;//dx.ConvertDataTable<vu_users>(dt)[0];
                    //return new vu_users
                    //{
                    //    user_sk = Convert.ToInt32(dt.Rows[0]["user_sk"]),
                    //    username = dt.Rows[0]["username"].ToString(),
                    //    user_full_name = dt.Rows[0]["user_full_name"].ToString(),
                    //    password = dt.Rows[0]["password"].ToString(),
                    //    email = dt.Rows[0]["email"].ToString(),
                    //    dynmc_url = dt.Rows[0]["dynmc_url"].ToString(),
                    //    //br_code = Convert.ToInt32(dt.Rows[0]["br_code"]),
                    //    //br_desc = dt.Rows[0]["br_desc"].ToString(),
                    //    record_status = Convert.ToInt32(dt.Rows[0]["record_status"])
                    //};
                }
                else
                {
                    return null;
                }
                con.Close();
            };
        }
        public async Task<bool> UpdateInactive(string dynmcUrl)
        {
            //vu_users uid = new vu_users();
            //bool upt = false;
            int i = 0;
            string query = "select * from security.vu_user_login where dynmc_url = '{0}'";
            query = string.Format(query, dynmcUrl);
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable(query, con);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var usr = dx.ConvertDataTable<vu_users>(dt)[0];
                    i = db.doInsertUpdate("update security.users set record_status='1',status='Inactive' where id=" + usr.user_sk, con);
                }
                con.Close();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            };
        }
        public string GetToken(string userid)
        {
            string key = ConfigurationManager.AppSettings["key"].ToString();
            var issuer = ConfigurationManager.AppSettings["issuer"].ToString();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var permclaims = new List<Claim>();
            permclaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permclaims.Add(new Claim("valid", "1"));
            permclaims.Add(new Claim("user", userid.ToString()));
            //permclaims.Add(new Claim("", "Ahmed"));

            var token = new JwtSecurityToken(issuer,
                issuer,
                permclaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;

        }

        public async Task<UserAuthRepository> GenerateSession(vu_users model)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();

                    EncryptDecrypt crypt = new EncryptDecrypt();

                    UserAccountModel obj = new UserAccountModel();
                    DataTable dt = new DataTable();
                    DataTable dt1 = new DataTable();
                    List<UserAccountDetails> detail = new List<UserAccountDetails>();
                    //string query = "SELECT * from \"security\".vu_users_urls_parent_child  where user_sk in(0," + model.user_sk + ")";
                    string query = "EXECUTE [security].[sp_users_urls_parent_child_MX] " + model.user_sk;
                    dt = db.getDataTable(query, con);

                    obj.UserId = model.user_sk;
                    obj.UserFullName = enc.Decrypt(model.username);
                    obj.UserFullName_Dec = crypt.Decrypt(model.username);
                    obj.Username_Enc = model.username;
                    obj.Dynamic_URL = model.dynmc_url;
                    obj.Username = enc.Decrypt(model.username);

                    obj.Token = GetToken(crypt.Encrypt(model.user_sk.ToString()));
                    foreach (DataRow dr in dt.Rows)
                    {
                        UserAccountDetails ud = new UserAccountDetails();
                        ud.UserId = Convert.ToInt32(dr["user_sk"]);
                        ud.CanEdit = Convert.ToInt32(dr["can_edit"]);
                        ud.CanView = Convert.ToInt32(dr["can_view"]);
                        ud.CanAdd = Convert.ToInt32(dr["can_add"]);
                        ud.CanDel = Convert.ToInt32(dr["can_del"]);
                        ud.AcivityId = Convert.ToInt32(dr["activityid"]);
                        ud.AcivityName = dr["activitydisc"].ToString();
                        ud.AcivityTitle = dr["activitytitle"].ToString();
                        ud.ActivityUrl = dr["activityurl"].ToString();
                        ud.ActivityParentId = Convert.ToInt32(dr["activityparentid"]);
                        ud.ActivityIcon = dr["activiticon"].ToString();
                        //ud.ActivityAttr = dr["ActivityAttr"].ToString();
                        detail.Add(ud);
                    }

                    ///////////////////////////////////////// Token Insertion

                    token_expire t = new token_expire();
                    t.user_sk = model.user_sk;
                    t.token_payload = obj.Token;
                    t.token_issue_dtme = DateTime.Now;
                    t.token_expired_by = 0;
                    t.row_version = 0;

                    CustomObject customObj = db.SaveChanges("security", t);


                    /////////////////////////////////////////
                    //UniversalRepository.WriteException(JsonConvert.SerializeObject(obj), "LoginAPI");
                    return new UserAuthRepository { UserAccountObj = obj, UserAccountDetailObj = detail, br_code = model.br_code, br_desc = model.br_desc };
                }
            }
            catch (Exception ex)
            {
                UniversalRepository.WriteException(ex.ToString(), "LoginAPI");
                return new UserAuthRepository { UserAccountObj = null, UserAccountDetailObj = null };
            }
        }

        public int GetDecUserId(string encUserId)
        {
            EncryptDecrypt crypt = new EncryptDecrypt();

            return Convert.ToInt32(crypt.Decrypt(encUserId));
        }

        public bool GetTokenValidation(string Dynamic_URL, long? UserId)
        {
            //var handler = new JwtSecurityTokenHandler();
            //var jwt = handler.ReadJwtToken(encUserId);
            //var tokenS = handler.ReadJwtToken(encUserId) as JwtSecurityToken;
            //var u = tokenS.Claims.Where(y => y.Type == "user").FirstOrDefault().Value;



            //var x = GetDecUserId(u);

            //string query = "select top 1 max(row_sk) as row_sk, is_token_exp from security.token_expire where user_sk=1 group by is_token_exp order by row_sk desc";
            string query = "select * from security.vu_users where user_sk = " + UserId + "";
            //query = string.Format(query, x);
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable(query, con);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["dynmc_url"].ToString() == Dynamic_URL)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

        }

        public bool TokenExpire(int UserId)
        {
            int x = 1;
            string query = "update \"security\".token_expire set is_token_exp = 1 where row_sk = (select row_sk from \"security\".token_expire where user_sk={0} order by row_sk desc limit 1)";
            query = string.Format(query, x);
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = "update \"security\".token_expire set is_token_exp = 1 where row_sk = (select row_sk from \"security\".token_expire where user_sk=" + UserId + " order by row_sk desc limit 1)";
            using (DbContextHelper db = new DbContextHelper())
            {
                int i = db.SelectSingleValue(cmd);
                return true;
            }

        }

        public vu_users GenerateURL(string UID, string machine_name = "", string IP = "", int branch_code = 0)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    SqlConnection.ClearPool(con);
                    //var url_expire = Convert.ToInt32(ConfigurationManager.AppSettings["URl_Expire"].ToString());
                    //string query = "select top 1 * from security.vu_url_hist where usr_id = " + UID + "and Machine_name='" + machine_name + "'and IPAddress='" + IP + "'";
                    //string query = "select top 1 * from security.url_hist where url_dte_tme >= DATEADD(HOUR, -" + url_expire + ", GETDATE()) and IPAddress = '" + IP + "' and Machine_name = '" + machine_name + "'";
                    string query = "select * from security.Vu_url_hist where USR_ID = '" + UID + "' and IPAddress = '" + IP + "'";
                    DataTable dt = db.getDataTable(query, con);
                    var url = "";
                    string EncRandom = UniversalRepository.RandomString();
                    if (dt.Rows.Count != 0 && dt != null)
                    {
                        query = "update security.users set is_login= 0,dynmc_url='" + EncRandom + "' where user_ful_name = '{0}'";
                        query = string.Format(query, UID);
                        int i = db.doInsertUpdate(query, con);
                        url = encryptURL(/*dt.Rows[0]["Link"].ToString()*/EncRandom, machine_name, IP, branch_code);
                        query = "insert into security.url_hist values('" + UID + "','" + EncRandom + "',getdate(),'" + machine_name + "','" + IP + "')";
                        db.doInsertUpdate(query, con);
                        return new vu_users
                        {
                            dynmc_url = url
                        };
                    }
                    else
                    {
                        EncryptDecrypt enc = new EncryptDecrypt();
                        ChecklastLoginDate(UID);
                        //ReverseLookup("192.168.0.115");
                        query = "update security.users set is_login= 0,dynmc_url='" + EncRandom + "' where user_ful_name = '{0}'";
                        query = string.Format(query, UID);
                        int i = db.doInsertUpdate(query, con);

                        //query = "select * from security.users where user_ful_name = '{0}'";
                        query = "select * from security.Vu_users where user_ful_name = '{0}'";
                        query = string.Format(query, UID);
                        dt = db.getDataTable(query, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            query = "insert into security.url_hist values('" + UID + "','" + EncRandom + "',getdate(),'" + machine_name + "','" + IP + "')";
                            db.doInsertUpdate(query, con);
                            con.Close();
                            url = encryptURL(dt.Rows[0]["dynmc_url"].ToString(), machine_name, IP, branch_code);
                            return new vu_users
                            {
                                //user_sk = Convert.ToInt32(dt.Rows[0]["id"]),
                                username = dt.Rows[0]["username"].ToString(),
                                user_full_name = dt.Rows[0]["user_ful_name"].ToString(),
                                password = dt.Rows[0]["password"].ToString(),
                                email = dt.Rows[0]["email"].ToString(),
                                dynmc_url = url,
                                record_status = Convert.ToInt32(dt.Rows[0]["record_status"])
                            };
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UniversalRepository.InvalidLogin(ex.InnerException.ToString(), machine_name, IP);
                UniversalRepository.WriteException(ex.ToString(), "GenerateURL");
                con.Close();
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        //public string ReverseLookup(string ip)
        //{
        //    if (String.IsNullOrWhiteSpace(ip))
        //        return ip;

        //    //try
        //    //{
        //        IPHostEntry host = Dns.GetHostEntry(ip);

        //        return host != null ? host.HostName : ip;
        //    //}
        //    //catch (SocketException)
        //    //{
        //    //    return ip;
        //    //}
        //}
        public string encryptURL(string hash, string machine_name, string IP, int br_code)
        {
            string url = hash;
            url += "|" + machine_name;
            url += "|" + IP;
            url += "|" + br_code;
            url = enc.Encrypt(url);
            url = url.Replace('+', ']');
            return url;
        }
        public void Dispose()
        {
        }
    }

}
