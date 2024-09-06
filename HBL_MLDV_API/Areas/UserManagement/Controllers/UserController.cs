using HBL_MLDV_API.Areas.UserManagement.Models;
using HBL_MLDV_API.Models.Security;
using HBL_MLDV_API.Providers.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Areas.UserManagement.Controllers
{
    public class UserController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpGet]
        [Route("api/UserManagement/User/GetAllMenus/")]
        public IHttpActionResult GetAllMenus()
        {
            try
            {
                using (UserService obj = new UserService())
                {
                    return Ok(obj.GetAllMenus());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetAllMenus()");
                return null;
            }
        }

        [HttpGet]
        [Route("api/UserManagement/User/GetMenusByUserSk/{user_sk}")]
        public IHttpActionResult GetMenusByUserSk(int user_sk)
        {
            try
            {
                using (UserService obj = new UserService())
                {
                    return Ok(obj.GetMenusByUserSk(user_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetMenusByUserSk()");
                return null;
            }
        }

        [HttpGet]
        [Route("api/UserManagement/User/GetUsersList/")]
        public IHttpActionResult GetUsersList()
        {
            try
            {
                using (UserService obj = new UserService())
                {
                    return Ok(obj.GetUsersList());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetUsersList()");
                return null;
            }
        }

        [HttpGet]
        [Route("api/UserManagement/User/GetUserAprvById/{user_sk}")]
        public IHttpActionResult GetUserAprvById(int user_sk)
        {
            try
            {
                using (UserService obj = new UserService())
                {
                    return Ok(obj.GetUserAprvById(user_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetUserAprvById()");
                return null;
            }
        }



        [HttpPost]
        [Route("api/UserManagement/User/SendUserForApproval/{br_code}/{user_sk}")]
        public IHttpActionResult SendUserForApproval(vu_users_aprv_vm model, int br_code, int user_sk)
        {
            try
            {
                using (UserService obj = new UserService())
                {
                    return Ok(obj.SendUserForApproval(model, br_code, user_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SendUserForApproval()");
                return null;
            }
        }

        //[HttpPost]
        //[Route("api/UserManagement/User/SendUserForApprovalUpdate/{br_code}/{user_sk}")]
        //public IHttpActionResult SendUserForApprovalUpdate(vu_users_aprv_vm model, int br_code, int user_sk)
        //{
        //    try
        //    {
        //        using (UserService obj = new UserService())
        //        {
        //            return Ok(obj.SendUserForApprovalUpdate(model, br_code, user_sk));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "SendUserForApprovalUpdate()");
        //        return null;
        //    }
        //}
        [HttpPost]
        [Route("api/UserManagement/User/ChangePassword/")]
        public IHttpActionResult ChangePassword(ChangePassword model)
        {
            try
            {
                using (UserService obj = new UserService())
                {
                    return Ok(obj.ChangePassword(model));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "ChangePassword()");
                return null;
            }
        }

        [HttpPost]
        [Route("api/UserManagement/User/ValidateEmailRegistered/{page}")]
        public IHttpActionResult ValidateEmailRegistered(vu_users model,string page)
        {
            try
            {
                using (UserService obj = new UserService())
                {
                    return Ok(obj.ValidateEmailRegistered(model, page));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "ValidateEmailRegistered()");
                return null;
            }
        }
    }
}
