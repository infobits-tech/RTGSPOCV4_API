using HBL_MLDV_API.Areas.UserManagement.Models.Role;
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
    public class RoleController : ApiController
    {
        UniversalRepository un = new UniversalRepository();
        [HttpGet]
        [Route("api/UserManagement/Role/GetAllMenus/")]
        public IHttpActionResult GetAllMenus()
        {
            try
            {
                using (RoleService obj = new RoleService())
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
        [Route("api/UserManagement/Role/GetMenusByRoleSk/{role_sk}")]
        public IHttpActionResult GetMenusByRoleSk(int role_sk)
        {
            try
            {
                using (RoleService obj = new RoleService())
                {
                    return Ok(obj.GetMenusByRoleSk(role_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetMenusByRoleSk()");
                return null;
            }
        }

        [HttpGet]
        [Route("api/UserManagement/Role/GetRolesList/")]
        public IHttpActionResult GetRolesList()
        {
            try
            {
                using (RoleService obj = new RoleService())
                {
                    return Ok(obj.GetRolesList());
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetRolesList()");
                return null;
            }
        }

        [HttpGet]
        [Route("api/UserManagement/Role/GetRoleAprvById/{role_sk}")]
        public IHttpActionResult GetRoleAprvById(int role_sk)
        {
            try
            {
                using (RoleService obj = new RoleService())
                {
                    return Ok(obj.GetRoleAprvById(role_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetRoleAprvById()");
                return null;
            }
        }

        [HttpPost]
        [Route("api/UserManagement/Role/SendRoleForApproval/{br_code}/{user_sk}")]
        public IHttpActionResult SendRoleForApproval(vu_role_mst_aprv model, int br_code, int user_sk)
        {
            try
            {
                using (RoleService obj = new RoleService())
                {
                    return Ok(obj.SendRoleForApproval(model, br_code, user_sk));
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SendRoleForApproval()");
                return null;
            }
        }

        //[HttpPost]
        //[Route("api/UserManagement/Role/SendRoleForApprovalUpdate/{br_code}/{user_sk}")]
        //public IHttpActionResult SendRoleForApprovalUpdate(vu_role_mst_aprv model, int br_code, int user_sk)
        //{
        //    try
        //    {
        //        using (RoleService obj = new RoleService())
        //        {
        //            return Ok(obj.SendRoleForApproval(model, br_code, user_sk));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        un.WriteException(ex.ToString(), "SendRoleForApprovalUpdate()");
        //        return null;
        //    }
        //}
    }
}
