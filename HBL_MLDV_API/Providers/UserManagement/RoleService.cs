using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Areas.UserManagement.Models.Role;
using HBL_MLDV_API.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace HBL_MLDV_API.Providers.UserManagement
{
    public class RoleService : IDisposable
    {
        //UniversalRepository un = new UniversalRepository();
        UniversalRepository universalRepository = new UniversalRepository();
        DbContextHelper dbe = new DbContextHelper();

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

        public DataTable GetMenusByRoleSk(int role_sk)
        {
            try
            {

                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM security.vu_role_menu_list WHERE role_sk = " + role_sk, con);
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetMenusByRoleSk()");
                return null;
            }
        }

        public DataTable GetRolesList()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM security.vu_roles_list ORDER BY role_sk DESC", con);
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetRolesList()");
                return null;
            }
        }

        public vu_role_mst_aprv GetRoleAprvById(int role_sk)
        {
            try
            {

                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    vu_role_mst_aprv aprv = new vu_role_mst_aprv();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM security.vu_role_mst_aprv WHERE role_sk = " + role_sk, con);


                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model

                        aprv = dbe.ConvertDataTable<vu_role_mst_aprv>(dt)[0];

                        // Get user cando dt
                        dt = db.getDataTable("SELECT * FROM security.vu_role_menu_list WHERE role_sk IN (" + aprv.role_sk + ") ORDER BY activitytitle", con);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Convert to role cando model
                            List<vu_role_lvl_can_do_aprv> cando = dbe.ConvertDataTable<vu_role_lvl_can_do_aprv>(dt);
                            aprv.cando = cando;
                        }
                    }

                    return aprv;
                }
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "GetRoleAprvById()");
                return null;
            }
        }
        public CustomObject SendRoleForApproval(vu_role_mst_aprv model, int br_code, int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    DataTable dt = db.getDataTable("SELECT * FROM security.vu_role_mst WHERE status_sk IN (1,2,6) AND role_sk = " + model.role_sk + "", con);
                    CustomObject cObj = new CustomObject();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        cObj = SendRoleForApprovalUpdate(model, br_code, user_sk);
                    }
                    else
                    {
                        cObj = SendRoleForApprovalCreate(model, br_code, user_sk);
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
        public CustomObject SendRoleForApprovalCreate(vu_role_mst_aprv model, int br_code, int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    DataTable dt = db.getDataTable("SELECT * FROM security.vu_role_mst WHERE status_sk IN (1,2,6) AND role_desc = '" + model.role_desc + "'", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CustomObject cObj = new CustomObject();
                        cObj.status = (0 == 1);
                        cObj.Message = "Role already exists";
                        return cObj;
                    }
                }

                model.created_by = user_sk;
                model.created_on = universalRepository.GetDateTimeForTimeZone();
                model.record_status = 0;
                model.row_version = 0;

                // Set cando properties
                model.cando = model.cando.Where(x => (x.can_add > 0 || x.can_view > 0 || x.can_edit > 0 || x.can_del > 0)).ToList();
                model.cando.ForEach(x =>
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
                    resp = db.SaveChanges("security", model);
                    if (model.sender == "6")
                    {
                        // Check for pool doc
                        DataTable dt = db.getDataTable("SELECT * FROM setup.vu_pool_docs WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3002 and aprv_cat_sk in (2014)  AND doc_mst_sk = " + model.role_sk, con);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Update existing pool record time
                            db.doInsertUpdate("UPDATE setup.vu_pool_docs SET create_dt_tme = '" + universalRepository.GetDateTimeForTimeZone() + "' WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3002 AND  aprv_cat_sk in (2014) AND doc_mst_sk = " + model.role_sk, con);
                        }
                        else
                        {
                            CustomObject obj = universalRepository.CreatePoolRecord(br_code, int.Parse(model.role_sk.ToString()), model.role_desc, model.created_on, model.created_by, model.doc_link.Replace("Create", "View/" + resp.Data.Rows[0]["role_sk"].ToString()), 3002, 2014);
                        }
                    }
                }
                return resp;
            }
            catch (Exception ex)
            {
                universalRepository.WriteException(ex.ToString(), "SendRoleForApproval()");
                return null;
            }
        }

        public CustomObject SendRoleForApprovalUpdate(vu_role_mst_aprv model, int br_code, int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //DataTable dt = db.getDataTable("SELECT * FROM security.vu_role_mst_aprv WHERE status_sk IN (1,2,6) AND role_sk <> " + model.role_sk + " AND role_desc = '" + model.role_desc + "'", con);

                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    CustomObject cObj = new CustomObject();
                    //    cObj.status = (0 == 1);
                    //    cObj.Message = "Role already exists";
                    //    return cObj;
                    //}
                }

                model.updated_on = universalRepository.GetDateTimeForTimeZone();
                model.updated_by = user_sk; // logged in user

                // Set cando properties
                model.cando = model.cando.Where(x => ((x.state == "" || x.state == null) && (x.can_add > 0 || x.can_view > 0 || x.can_edit > 0 || x.can_del > 0)) || (x.state == "Changed")).ToList();
                model.cando.Where(x => (x.state == "" || x.state == null)).ToList().ForEach(x =>
                {
                    x.created_by = user_sk;
                    x.created_on = universalRepository.GetDateTimeForTimeZone();
                    x.record_status = 0;
                    x.row_version = 0;
                    x.role_sk = model.role_sk;
                });
                model.cando.Where(x => x.state == "Changed").ToList().ForEach(x =>
                {
                    x.updated_by = user_sk;
                    x.updated_on = universalRepository.GetDateTimeForTimeZone();
                    x.row_version = x.row_version + 1;
                });
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("security", model);
                    if (model.sender == "6")
                    {
                        // Check for pool doc
                        DataTable dt = db.getDataTable("SELECT * FROM setup.vu_pool_docs WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3002 AND aprv_cat_sk in (2016) AND doc_mst_sk = " + model.role_sk, con);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            // Update existing pool record time
                            db.doInsertUpdate("UPDATE setup.vu_pool_docs SET create_dt_tme = '" + universalRepository.GetDateTimeForTimeZone() + "' WHERE approval_status = 'Pending' AND approval_status_sk = 8 AND doc_typ_sk = 3002 AND aprv_cat_sk = 2014 AND doc_mst_sk = " + model.role_sk, con);
                        }
                        else
                        {
                            CustomObject obj = universalRepository.CreatePoolRecord(br_code, int.Parse(model.role_sk.ToString()), model.role_desc, model.created_on, model.created_by, model.doc_link, 3002, 2016);
                        }
                    }
                }
                return resp;
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

        //private void CreatePoolRecord(int br_code, int user_sk, string doc_no, DateTime created_on, int created_by, string doc_link)
        //{
        //    vu_pool_docs poolModel = new vu_pool_docs();
        //    poolModel.br_sk = br_code;
        //    poolModel.doc_no = doc_no;
        //    poolModel.doc_typ_sk = 3002;
        //    poolModel.doc_mst_sk = user_sk;
        //    poolModel.doc_dte = created_on;
        //    poolModel.aprv_cat_sk = 26;
        //    poolModel.doc_link = doc_link;
        //    poolModel.created_by = created_by;
        //    poolModel.create_dt_tme = universalRepository.GetDateTimeForTimeZone();
        //    poolModel.approval_status = "Pending";
        //    poolModel.approval_status_sk = 8;
        //    poolModel.row_version = 0;
        //    poolModel.record_status = 0;

        //    // Save pool model
        //    using (DbContextHelper dx  = new DbContextHelper())
        //    {
        //        dx.SaveChanges("setup", poolModel);
        //    }
        //}

        public void Dispose() { }
    }
}