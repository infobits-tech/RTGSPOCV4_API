using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Models.Security;
using HBL_MLDV_API.Repository;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using HBL_MLDV_API.Models;

namespace HBL_MLDV_API.Controllers
{
    public class AuthController : ApiController
    {
        UniversalRepository _repo = new UniversalRepository();
        EncryptDecrypt enc = new EncryptDecrypt();
        [Route("api/Auth/AuthenticateAndGenerateSession")]
        public async Task<IHttpActionResult> AuthenticateAndGenerateSession(vu_users model)
        {
            try
            {
                if (model != null)
                {
                    //string str = ConfigurationManager.AppSettings["logintyp"].ToString();
                    //ConfigurationManager.AppSettings["logintyp"] = model.logintyp;
                }
                using (var auth = new EmployeeSecurity())
                {

                    var usr = await auth.GetEmployee(model.username);

                    if (usr.record_status == 1)
                    {
                        return Content(HttpStatusCode.Unauthorized, "Not an active user!");
                    }

                    //If credentials are matched 
                    //if (auth.Login(model.username, model.password))
                    if (auth.LoginUserAndPwd(model.username, usr) == 200)
                    {
                        //var usr = await auth.GetEmployee(model.username, model.password);

                        //Check Account is Locked or NOT
                        if (auth.GetUserLock(usr))
                        {
                            return Content(HttpStatusCode.Unauthorized, "User account is locked!");
                        }
                        else
                        {
                            //UserAuthRepository usrauth = GetUserData(UserId);
                            //Maintain history and generation session
                            auth.MaintainLoginHistory(new login_history
                            {
                                usr_sk = usr.user_sk,
                                cntr = 0
                            });

                            UserAuthRepository usrauth = await auth.GenerateSession(usr);
                            if (usrauth.UserAccountObj != null)
                            {
                                return Ok(usrauth);
                            }
                            else
                            {
                                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
                            }

                        }

                    }

                    else if (auth.LoginUserAndPwd(model.username, usr) == 401)
                    {
                        if (auth.GetUserLock(usr))
                        {
                            return Content(HttpStatusCode.Unauthorized, "User account is locked!");
                        }
                        else
                        {
                            auth.MaintainLoginHistory(new login_history
                            {
                                usr_sk = usr.user_sk,
                                cntr = 1
                            });

                            //get counter from login history
                            //if coutner >= 3 insert lock record
                            if (auth.GetInvalidLoginCount(usr.user_sk))
                            {
                                auth.LockUser(new user_login_lock
                                {
                                    usr_sk = usr.user_sk,
                                    lck_dt_tm = new UniversalRepository().GetDateTimeForTimeZone()

                                });

                                return Content(HttpStatusCode.BadRequest, "User account is locked!");
                            }
                            else
                            {
                                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
                            }

                        }

                    }
                    //Return if creds are not matched
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "Username or Password is incorrect");
                        //return Unauthorized();
                    }
                }

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
            }
        }

        [Route("api/Auth/AuthenticateAndGenerateSessionDynmc/{IP}/{Machine_Name}")]
        public async Task<IHttpActionResult> AuthenticateAndGenerateSessionDynmc(vu_users model, string IP, string Machine_Name)
        {
                string url = model.url;
            try
            {
                IP = IP.Replace('_', '.');
                Machine_Name = Machine_Name.Replace('_', '.');
                if (model != null)
                {
                    //string str = ConfigurationManager.AppSettings["logintyp"].ToString();
                    //ConfigurationManager.AppSettings["logintyp"] = model.logintyp;
                }
                using (var auth = new EmployeeSecurity())
                {
                    var usr = await auth.GetEmployeeDynmc(model.dynmc_url);
                    model = usr;
                    //model.username = enc.Decrypt(model.username);
                    //model.password = enc.Decrypt(model.password);
                    if (auth.ChecklastLoginDate(usr.user_full_name))
                    {
                        usr.status = "Inactive";
                        usr.record_status = 1;
                        await auth.UpdateInactive(model.dynmc_url);
                        _repo.InvalidLogin("User Inactive due to specified non-activity period!", IP, Machine_Name, url);
                        return Content(HttpStatusCode.Unauthorized, "User Inactive due to specified non-activity period!");
                    }
                    if (usr.record_status == 1)
                    {
                        _repo.InvalidLogin("Not an active user!", IP, Machine_Name, url);
                        return Content(HttpStatusCode.Unauthorized, "Not an active user!");
                    }

                    //If credentials are matched 
                    //if (auth.Login(model.username, model.password))
                    if (auth.LoginUserAndPwd(enc.Decrypt(model.username), usr) == 200)
                    {
                        if (usr.is_login)
                        {
                            _repo.InvalidLogin("Retrieving data. Wait a few seconds and try to cut or copy again.", IP, Machine_Name, url);
                            return Content(HttpStatusCode.Unauthorized, "Retrieving data. Wait a few seconds and try to cut or copy again.");
                        }

                        //var usr = await auth.GetEmployee(model.username, model.password);

                        //Check Account is Locked or NOT
                        if (auth.GetUserLock(usr))
                        {
                            _repo.InvalidLogin("User account is locked!", IP, Machine_Name, url);
                            return Content(HttpStatusCode.Unauthorized, "User account is locked!");
                        }
                        //if (auth.ChecklastLoginDate(usr))
                        //{
                        //    return Content(HttpStatusCode.Unauthorized, "Retrieving data. Wait a few seconds and try to cut or copy again.");
                        //}
                        else
                        {
                            //UserAuthRepository usrauth = GetUserData(UserId);
                            //Maintain history and generation session
                            auth.MaintainLoginHistory(new login_history
                            {
                                usr_sk = usr.user_sk,
                                cntr = 0,
                                dynmc_url = usr.dynmc_url,
                                Login_DTE_TME = _repo.GetDateTimeForTimeZone()
                            });

                            UserAuthRepository usrauth = await auth.GenerateSession(usr);
                            if (usrauth.UserAccountObj != null)
                            {
                                return Ok(usrauth);
                            }
                            else
                            {
                                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
                            }

                        }

                    }

                    else if (auth.LoginUserAndPwd(enc.Decrypt(model.username), usr) == 401)
                    {
                        if (auth.GetUserLock(usr))
                        {
                            _repo.InvalidLogin("User account is locked!", IP, Machine_Name, url);
                            return Content(HttpStatusCode.Unauthorized, "User account is locked!");
                        }
                        else
                        {
                            auth.MaintainLoginHistory(new login_history
                            {
                                usr_sk = usr.user_sk,
                                cntr = 1
                            });

                            //get counter from login history
                            //if coutner >= 3 insert lock record
                            if (auth.GetInvalidLoginCount(usr.user_sk))
                            {
                                auth.LockUser(new user_login_lock
                                {
                                    usr_sk = usr.user_sk,
                                    lck_dt_tm = new UniversalRepository().GetDateTimeForTimeZone()

                                });

                                return Content(HttpStatusCode.BadRequest, "User account is locked!");
                            }
                            else
                            {
                                _repo.InvalidLogin("Invalid access. Please contact system adminstrator.", IP, Machine_Name, url);
                                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
                            }
                        }

                    }
                    //Return if creds are not matched
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "Username or Password is incorrect");
                        //return Unauthorized();
                    }
                }

            }
            catch (Exception ex)
            {
                _repo.WriteException(ex.ToString(), "AuthenticateAndGenerateSession");
                _repo.InvalidLogin(ex.InnerException.ToString(), IP, Machine_Name, url);
                if (ConfigurationManager.AppSettings["is_developer"].ToString().ToLower() == "y")
                {
                    return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator");
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, ex.ToString());
                }
            }
        }

        [Route("api/Auth/GenerateURL")]
        public IHttpActionResult GenerateURL(vu_users model)
        {
            try
            {
                using (var auth = new EmployeeSecurity())
                {
                    //var UserAgent = HttpContext.Current.Request.Headers["User-Agent"].ToString();
                    //var headers = HttpContext.Current.Request.Headers;
                    //var headersDictionary = new Dictionary<string, string>();

                    ////var headersDictionary = headers
                    //bool isSecure = HttpContext.Current.Request.IsAuthenticated;
                    //foreach (var headerKey in headers.Keys)
                    //{
                    //    var headerValue = headers[headerKey.ToString()].ToString();
                    //    headersDictionary.Add(headerKey.ToString(), headerValue);
                    //}
                    var usr = auth.GenerateURL(model.user_full_name, model.machine_name, model.IP, model.br_code);
                    if (usr != null)
                    {
                        var url = ConfigurationManager.AppSettings["APP_URL"].ToString() + "Login/GetLogin?du=" + usr.dynmc_url;
                        return Ok(url);
                    }
                    else
                    {
                        _repo.InvalidLogin("Invalid Credentials. Access denied!", model.IP, model.machine_name,string.Format("Login/{0}/{1}", model.user_full_name,model.br_code));
                        return Content(HttpStatusCode.Unauthorized, "Invalid Credentials. Access denied!");

                    }
                }

            }
            catch (Exception ex)
            {
                if (ConfigurationManager.AppSettings["is_developer"].ToString().ToLower() == "y")
                {
                    return Content(HttpStatusCode.BadRequest, ex.ToString());
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator");
                }
            }
        }

        //[Authorize]
        [HttpGet]
        [Route("api/Auth/TokenValidation/{Dynamic_URL}/{UserId}")]
        public IHttpActionResult TokenValidation(string Dynamic_URL, long? UserId)
        {

            try
            {
                using (var serv = new EmployeeSecurity())
                {
                    if (UserId != 0)
                    {
                        if (serv.GetTokenValidation(Dynamic_URL, UserId))
                        {
                            return Content(HttpStatusCode.OK, "true");
                        }
                        else
                        {   //to return UnAuthorized
                            return Content(HttpStatusCode.BadRequest, "false");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "false");
                    }

                }
            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "false");
            }
        }

        [HttpGet]
        [Route("api/Auth/TokenExpire/{UserId}")]
        public IHttpActionResult TokenExpire(int UserId)
        {

            try
            {
                using (var serv = new EmployeeSecurity())
                {
                    if (serv.TokenExpire(UserId))
                    {
                        return Content(HttpStatusCode.OK, "true");
                    }
                    else
                    {   //to return UnAuthorized
                        return Content(HttpStatusCode.BadRequest, "false");
                    }

                }
            }

            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "false");
            }
        }
        [Route("api/Auth/NavigationLog")]
        public IHttpActionResult NavigationLog(UserNavLog log)
        {
            try
            {
                _repo.NavigationLog(log.USER_ID, log.PageURL,log.DYNMC_URL);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
            }
        }
        [Route("api/Auth/InvalidNavigationLog")]
        public IHttpActionResult InvalidNavigationLog(UserNavLog log)
        {
            try
            {
                _repo.InvalidNavigationLog(log.USER_ID, log.PageURL,log.DYNMC_URL);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
            }
        }
        
        [Route("api/Auth/InvalidLoginLog")]
        public IHttpActionResult InvalidLoginLog(InvalidLogin log)
        {
            try
            {
                _repo.InvalidLogin(log.ERRORMSG, log.IPADDRESS, log.MACHINE_NAME,log.URL);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
            }
        }
        [Route("api/Auth/GenerateSetupLog")]
        public IHttpActionResult GenerateSetupLog(UserSetupLog log)
        {
            try
            {
                _repo.GenerateSetupLog(log);
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
            }
        }
        [Route("api/Auth/Logout")]
        public IHttpActionResult Logout(vu_users model)
        {
            try
            {
                using (var auth = new EmployeeSecurity())
                {
                    var usr = auth.GetUserInfo(model.user_sk);
                    if (usr != null)
                    {
                        auth.MaintainLogoutHistory(new login_history
                        {
                            usr_sk = usr.user_sk,
                            dynmc_url = usr.dynmc_url,
                            Login_DTE_TME = usr.Login_DTE_TME,
                            Logout_DTE_TME = _repo.GetDateTimeForTimeZone()
                        });
                    }
                    return Ok(usr);
                }

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, "Invalid access. Please contact system adminstrator.");
            }
        }

        //public string GetMACAddress()
        //{
        //    string deviceId = System.Net.Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName;
        //    //NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        //    //String sMacAddress = string.Empty;
        //    //foreach (NetworkInterface adapter in nics)
        //    //{
        //    //    if (sMacAddress == String.Empty)// only return MAC Address from first card  
        //    //    {
        //    //        IPInterfaceProperties properties = adapter.GetIPProperties();
        //    //        sMacAddress = adapter.GetPhysicalAddress().ToString();
        //    //    }
        //    //}
        //    return deviceId;
        //}
    }
}
