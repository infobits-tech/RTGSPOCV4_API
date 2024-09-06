using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Areas.UserManagement.Models;
using HBL_MLDV_API.Areas.UserManagement.Models.Role;
using HBL_MLDV_API.Models.Security;
using HBL_MLDV_API.Providers.Security;
using HBL_MLDV_API.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using HBL_MLDV_API.Areas.Approval.Models;

namespace HBL_MLDV_API.Providers.UserManagement
{
    public class UserService : IDisposable
    {
        UniversalRepository universalRepository = new UniversalRepository();
        Connection dx = new Connection();
        EncryptDecrypt enc = new EncryptDecrypt();
        public DataTable GetAllMenus()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM security.vu_mnu_activity ORDER BY activitytitle", con);
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetAllMenus()");
                return null;
            }
        }

        public DataTable GetMenusByUserSk(int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM security.vu_user_menu_list WHERE user_sk = " + user_sk, con);
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetMenusByUserSk()");
                return null;
            }
        }

        public DataTable GetUsersList()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM security.vu_users_list ORDER BY user_sk DESC", con);
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetUsersList()");
                return null;
            }
        }

        public List<vu_role_mst> GetRoleList()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //DataTable dt = db.getDataTable("select * from security.role_mst where (record_status = 0 or record_status is null)",con);
                    DataTable dt = db.getDataTable("select * from security.Vu_role_mst where (record_status = 0 or record_status is null)", con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return dx.ConvertDataTable<vu_role_mst>(dt);
                    }
                    else
                    {
                        return new List<vu_role_mst>();
                    }
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetRoleList()");
                return null;
            }
        }

        public vu_users_aprv_vm GetUserAprvById(int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    vu_users_aprv_vm aprv = new vu_users_aprv_vm();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM security.vu_users_aprv WHERE user_sk = " + user_sk, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<vu_users_aprv_vm>(dt)[0];

                        // Get user cando dt
                        dt = db.getDataTable("SELECT * FROM security.vu_user_menu_list WHERE user_sk IN (0," + aprv.user_sk + ") ORDER BY activitytitle", con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Convert to user cando model
                            List<vu_user_lvl_can_do_aprv> cando = dx.ConvertDataTable<vu_user_lvl_can_do_aprv>(dt);
                            List<vu_user_lvl_can_do_aprv> candoDistinct = new List<vu_user_lvl_can_do_aprv>();
                            List<vu_role_mst> rolelist = GetRoleList();
                            if (cando != null && cando.Count > 0)
                            {
                                foreach (var va in cando)
                                {
                                    if (cando.FindAll(x => x.activityid == va.activityid).ToList().Count > 1)
                                    {
                                        if (candoDistinct.FindAll(x => x.activityid == va.activityid).ToList().Count < 1)
                                        {
                                            int role_sk = cando.FindAll(x => x.activityid == va.activityid && x.role_sk > 0).FirstOrDefault() == null ? 0 : cando.FindAll(x => x.activityid == va.activityid && x.role_sk > 0).FirstOrDefault().role_sk;
                                            var max = cando.FindAll(x => x.activityid == va.activityid).OrderByDescending(x => x.can_total).FirstOrDefault();
                                            max.role_sk = role_sk;
                                            if (role_sk > 0)
                                            {
                                                max.role_name = rolelist.FindAll(x => x.role_sk == role_sk).FirstOrDefault().role_desc;
                                            }

                                            candoDistinct.Add(max);
                                        }
                                    }
                                    else
                                    {
                                        if (va.role_sk > 0)
                                        {
                                            va.role_name = rolelist.FindAll(x => x.role_sk == va.role_sk).FirstOrDefault().role_desc;

                                        }
                                        candoDistinct.Add(va);
                                    }
                                }
                            }
                            aprv.cando = candoDistinct;
                            aprv.role_sk = candoDistinct.Select(x => x.role_sk).ToArray();
                        }

                        // Get user branch dt
                        dt = db.getDataTable("SELECT * FROM security.vu_user_br_mapping_aprv WHERE user_sk = " + aprv.user_sk, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Convert to user br mapping model
                            List<vu_user_br_mapping_aprv> br = dx.ConvertDataTable<vu_user_br_mapping_aprv>(dt);
                            aprv.br_code = br.Select(x => x.br_code).ToArray();
                        }

                        // Get user role dt
                        dt = db.getDataTable("SELECT * FROM security.vu_user_role_mapping_aprv WHERE user_sk = " + aprv.user_sk, con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Convert to user role mapping model
                            List<vu_user_role_mapping_aprv> role = dx.ConvertDataTable<vu_user_role_mapping_aprv>(dt);
                            aprv.role_sk = role.Select(x => x.role_sk).ToArray();
                            dt = db.getDataTable("select * from security.vu_role_lvl_can_do_aprv where role_sk in (" + string.Join(",", aprv.role_sk.ToArray()) + ")", con);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                aprv.role = dx.ConvertDataTable<vu_role_lvl_can_do_aprv>(dt);
                            }
                        }
                    }

                    return aprv;
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetUserAprvById()");
                return null;
            }
        }
        public CustomObject SendUserForApproval(vu_users_aprv_vm model, int br_code, int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    DataTable dt = db.getDataTable("SELECT * FROM security.users WHERE status_sk IN (1,2,6) AND id = "+model.user_sk+"", con);
                    CustomObject cObj = new CustomObject();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cObj = SendUserForApprovalUpdate(model, br_code, user_sk);
                    }
                    else
                    {
                        cObj = SendUserForApprovalCraete(model, br_code, user_sk);
                    }
                    return cObj;
                }

            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "SendRoleForApproval()");
                return null;
            }
        }
        public CustomObject SendUserForApprovalCraete(vu_users_aprv_vm model, int br_code, int user_sk)
        {
            try
            {
                //model.email = model.email.ToLower();
                model.username = model.user_full_name;

                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    DataTable dt = db.getDataTable("SELECT * FROM security.vu_users WHERE status_sk IN (1,2,6) AND user_ful_name = '" + model.user_full_name + "'", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CustomObject cObj = new CustomObject();
                        cObj.status = (0 == 1);
                        cObj.Message = "Username already exists";
                        return cObj;
                    }
                }
                // Create user aprv model
                vu_users_aprv obj = new vu_users_aprv();
                obj.created_by = user_sk;
                obj.created_on = universalRepository.GetDateTimeForTimeZone();
                //obj.email = model.email;
                //obj.mobile_cntry_cde = model.mobile_cntry_cde;
                //obj.mobile_nbr = model.mobile_nbr;
                obj.username = enc.Encrypt(model.user_full_name);
                //obj.password = enc.Encrypt(model.password);
                obj.remarks = model.remarks;
                obj.Crtd_by_IP = model.Crtd_by_IP;
                obj.record_status = model.record_status;
                obj.row_version = 0;
                obj.status_sk = model.status_sk;
                obj.user_full_name = model.user_full_name;
                obj.state = model.state;
                obj.status = model.status;

                // Set cando properties
                obj.cando = model.cando.Where(x => (x.can_add > 0 || x.can_view > 0 || x.can_edit > 0 || x.can_del > 0)).ToList();
                obj.cando.ForEach(x =>
                {
                    x.created_by = user_sk;
                    x.created_on = universalRepository.GetDateTimeForTimeZone();
                    x.record_status = 0;
                    x.row_version = 0;
                });
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("security", obj);



                    if (resp.Message == "Record has been saved successfully")
                    {
                        // Convert to user aprv model
                        vu_users_aprv savedModel = dx.ConvertDataTable<vu_users_aprv>(resp.Data)[0];

                        if (savedModel.user_sk > 0)
                        {
                            // Branch assignment
                            if (model.br_code.Length > 0)
                            {
                                // Delete existing branch mappings
                                db.doInsertUpdate("DELETE FROM security.vu_user_br_mapping_aprv WHERE user_sk = " + savedModel.user_sk, con);

                                for (int i = 0; i < model.br_code.Length; i++)
                                {
                                    vu_user_br_mapping_aprv brMapping = new vu_user_br_mapping_aprv();
                                    brMapping.br_code = model.br_code[i];
                                    brMapping.user_sk = int.Parse(savedModel.user_sk.ToString());
                                    brMapping.is_default = false; // Default branch boolean
                                    brMapping.created_by = model.created_by;
                                    brMapping.created_on = universalRepository.GetDateTimeForTimeZone();
                                    brMapping.record_status = 0;
                                    brMapping.row_version = 0;

                                    // Save branch mapping
                                    resp = db.SaveChanges("security", brMapping);
                                }
                            }

                            // Role assignment
                            if (model.role_sk.Length > 0)
                            {
                                // Delete existing branch mappings
                                db.doInsertUpdate("DELETE FROM security.vu_user_role_mapping_aprv WHERE user_sk = " + savedModel.user_sk, con);

                                for (int i = 0; i < model.role_sk.Length; i++)
                                {
                                    vu_user_role_mapping_aprv roleMapping = new vu_user_role_mapping_aprv();
                                    roleMapping.role_sk = model.role_sk[i];
                                    roleMapping.user_sk = int.Parse(savedModel.user_sk.ToString());
                                    roleMapping.created_by = model.created_by;
                                    roleMapping.created_on = universalRepository.GetDateTimeForTimeZone();

                                    // Save role mapping
                                    resp = db.SaveChanges("security", roleMapping);
                                }
                            }
                            if (model.sender == "6")
                            {
                                // Check for pool doc
                                DataTable dt = db.getDataTable("SELECT * FROM setup.vu_pool_docs WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3001 AND aprv_cat_sk = 2013 AND doc_mst_sk = " + savedModel.user_sk, con);

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    // Update existing pool record time
                                    db.doInsertUpdate("UPDATE setup.vu_pool_docs SET create_dt_tme = '" + universalRepository.GetDateTimeForTimeZone() + "' WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3001 AND aprv_cat_sk = 2013 AND doc_mst_sk = " + savedModel.user_sk, con);
                                }
                                else
                                {
                                    CustomObject custobj = universalRepository.CreatePoolRecord(br_code, int.Parse(savedModel.user_sk.ToString()), savedModel.user_full_name, savedModel.created_on, savedModel.created_by, model.doc_link +"/"+savedModel.user_sk , 3001, 2013);
                                }
                            }
                        }
                    }

                    return resp;
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "SendUserForApproval()");
                return null;
            }
        }

        public CustomObject SendUserForApprovalUpdate(vu_users_aprv_vm model, int br_code, int user_sk)
        {
            try
            {
                //model.email = model.email.ToLower();
                //model.username = model.email.ToLower();

                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //DataTable dt = db.getDataTable("SELECT * FROM security.vu_users_aprv WHERE status_sk IN (1,2,6) AND user_sk <> " + model.user_sk + " AND username = '" + model.username+ "'", con);
                    //
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    CustomObject cObj = new CustomObject();
                    //    cObj.status = (0 == 1);
                    //    cObj.Message = "Username already exists";
                    //    return cObj;
                    //}
                }
                // Create user aprv model
                vu_users_aprv obj = new vu_users_aprv();
                obj.username = model.username;
                obj.created_by = model.created_by;
                obj.Crtd_by_IP = model.Crtd_by_IP;
                obj.updtd_by_IP = model.updtd_by_IP;
                obj.created_on = (DateTime)model.created_on;
                //obj.email = model.email;
                //obj.mobile_cntry_cde = model.mobile_cntry_cde;
                //obj.mobile_nbr = model.mobile_nbr;
                //obj.password = model.password;
                obj.remarks = model.remarks;
                obj.record_status = model.record_status;
                obj.row_version = model.row_version;
                obj.status_sk = model.status_sk;
                obj.user_full_name = model.user_full_name;
                obj.state = model.state;
                obj.updated_on = universalRepository.GetDateTimeForTimeZone();
                obj.updated_by = user_sk; // logged in user
                obj.user_sk = model.user_sk;
                obj.status = model.status;

                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    int[] arr = model.cando.Select(x => x.activityid).ToArray();
                    DataTable dt = db.getDataTable("select * from security.vu_user_lvl_can_do_aprv where user_sk = " + model.user_sk + " and activityid in (" + string.Join(",", arr) + ")", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<vu_user_lvl_can_do_aprv> lst = dx.ConvertDataTable<vu_user_lvl_can_do_aprv>(dt);
                        foreach (var va in model.cando)
                        {
                            var res = lst.FindAll(x => x.activityid == va.activityid).FirstOrDefault();
                            if (res != null)
                            {
                                va.state = "Changed";
                            }
                            else
                            {
                                va.state = "";
                            }
                        }
                    }
                }
                // Set cando properties
                obj.cando = model.cando.Where(x => ((x.state == "" || x.state == null) && (x.can_add > 0 || x.can_view > 0 || x.can_edit > 0 || x.can_del > 0)) || (x.state == "Changed")).ToList();
                obj.cando.Where(x => (x.state == "" || x.state == null)).ToList().ForEach(x =>
                {
                    x.created_by = user_sk;
                    x.created_on = universalRepository.GetDateTimeForTimeZone();
                    x.record_status = 0;
                    x.row_version = 0;
                    x.user_sk = (int)model.user_sk;
                });

                obj.cando.Where(x => x.state == "Changed").ToList().ForEach(x =>
                {
                    x.updated_by = user_sk;
                    x.updated_on = universalRepository.GetDateTimeForTimeZone();
                    x.row_version = x.row_version + 1;
                });

                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    var resp = db.SaveChanges("security", obj);

                    if (resp.Message.ToLower() == "record has been updated successfully")
                    {
                        // Convert to user aprv model
                        vu_users_aprv savedModel = dx.ConvertDataTable<vu_users_aprv>(resp.Data)[0];

                        if (savedModel.user_sk > 0)
                        {
                            // Branch assignment
                            if (model.br_code.Length > 0)
                            {
                                // Delete existing branch mappings
                                db.doInsertUpdate("DELETE FROM security.vu_user_br_mapping_aprv WHERE user_sk = " + savedModel.user_sk, con);

                                for (int i = 0; i < model.br_code.Length; i++)
                                {
                                    vu_user_br_mapping_aprv brMapping = new vu_user_br_mapping_aprv();
                                    brMapping.br_code = model.br_code[i];
                                    brMapping.user_sk = int.Parse(savedModel.user_sk.ToString());
                                    brMapping.is_default = false; // Default branch boolean
                                    brMapping.created_by = model.created_by;
                                    brMapping.created_on = universalRepository.GetDateTimeForTimeZone();
                                    brMapping.record_status = 0;
                                    brMapping.row_version = 0;

                                    // Save branch mapping
                                    db.SaveChanges("security", brMapping);
                                }
                            }

                            // Role assignment
                            if (model.role_sk.Length > 0)
                            {
                                // Delete existing branch mappings
                                db.doInsertUpdate("DELETE FROM security.vu_user_role_mapping_aprv WHERE user_sk = " + savedModel.user_sk, con);

                                for (int i = 0; i < model.role_sk.Length; i++)
                                {
                                    vu_user_role_mapping_aprv roleMapping = new vu_user_role_mapping_aprv();
                                    roleMapping.role_sk = model.role_sk[i];
                                    roleMapping.user_sk = int.Parse(savedModel.user_sk.ToString());
                                    roleMapping.created_by = model.created_by;
                                    roleMapping.created_on = universalRepository.GetDateTimeForTimeZone();

                                    // Save role mapping
                                    db.SaveChanges("security", roleMapping);
                                }
                            }
                            if (model.sender == "6")
                            {
                                // Check for pool doc
                                DataTable dt = db.getDataTable("SELECT * FROM setup.vu_pool_docs WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3001 AND aprv_cat_sk = 2015 AND doc_mst_sk = " + savedModel.user_sk,con);

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    // Update existing pool record time
                                    db.doInsertUpdate("UPDATE setup.vu_pool_docs SET create_dt_tme = '" + universalRepository.GetDateTimeForTimeZone() + "' WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3001 AND aprv_cat_sk = 2015 AND doc_mst_sk = " + savedModel.user_sk,con);
                                }
                                else
                                {
                                    CustomObject custobj = universalRepository.CreatePoolRecord(br_code, int.Parse(savedModel.user_sk.ToString()), savedModel.user_full_name, savedModel.created_on, savedModel.created_by, model.doc_link, 3001, 2015);
                                }
                            }

                        }
                    }
                    con.Close();
                    return resp;
                }

            }
            catch (Exception ex)
            {
                CustomObject obj = new CustomObject();
                obj.Message = "Error Occured! Please contact system administrator";
                obj.status = false;
                obj.Data = new DataTable();
                return obj;
            }

        }

        public CustomObject ChangePassword(ChangePassword model)
        {
            CustomObject obj = new CustomObject();
            try
            {

                using (DbContextHelper dx = new DbContextHelper())
                {

                    //DataTable dt = dx.SelectDataTable("select * from security.users where id=" + model.userid + " and password = '" + model.OldPasswordEncrpt + "'");
                    DataTable dt = dx.SelectDataTable("select * from security.Vu_users where id=" + model.userid + " and password = '" + model.OldPasswordEncrpt + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        using (hashServices logs = new hashServices())
                        {
                            logs.GenerateLog(Convert.ToInt32(model.userid), 3001, Convert.ToInt32(model.userid), universalRepository.GetDateTimeForTimeZone());
                        }
                        int i = dx.doinsertupdatedeleteNoAsync("update security.users set password = '" + model.NewPasswordEncrpt + "' where id=" + model.userid);

                        obj.status = true;
                        obj.Message = "Password changed successfully";
                        obj.Data = dt;
                    }
                    else
                    {
                        obj.status = false;
                        obj.Message = "Old Password is incorrect";
                        obj.Data = dt;
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                obj.status = false;
                obj.Message = "Please contact system administrator";
                obj.Data = new DataTable();
                return obj;
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Bool</returns>
        public bool ValidateEmailRegistered(vu_users model, string page)
        {
            try
            {
                using (DbContextHelper dx = new DbContextHelper())
                {

                    DataTable dt = dx.SelectDataTable("SELECT * FROM security.vu_users_aprv WHERE email = '" + model.email + "'");

                    if (dt != null)
                    {
                        if (page == "Create" && dt.Rows.Count > 0)
                        {
                            return false;
                        }
                        //else if (page == "Edit" && dt.Rows.Count == 1)
                        //{
                        //    return false;
                        //}
                        else
                        {
                            return true;
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose() { }
    }
}