using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Areas.Approval.Models;
using HBL_MLDV_API.Areas.UserManagement.Models;
using HBL_MLDV_API.Areas.UserManagement.Models.Role;
using HBL_MLDV_API.Models.Common;
using HBL_MLDV_API.Models.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;
using HBL_MLDV_API.Models;

namespace HBL_MLDV_API.Providers.Approval
{
    public class PoolManagementServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        ADOConnection db = new ADOConnection();
        SqlConnection con = new SqlConnection();
        public int SubmitApproval(vu_pool_docs model)
        {
            using (ADOConnection dx = new ADOConnection())
            {
                con = dx.getDatabaseConnection();
                //var qry = GetQueryforpooldocupdate(model.doc_typ_sk, model.doc_mst_sk, model.approval_status_sk, model.approval_status, Convert.ToDateTime(model.update_dt_tme), model.aprv_cat_sk);
                //dx.doInsertUpdate(qry, con);
                dx.doInsertUpdate("update setup.vu_pool_docs_forward set approval_status_sk = " + model.approval_status_sk + ", approval_status = '" + model.approval_status + "',rmks='" + model.rmks + "',update_dt_tme='" + model.update_dt_tme + "',updated_by=" + model.updated_by + " where doc_pool_sk = " + model.doc_pool_sk, con);
                return dx.doInsertUpdate("update setup.pool_docs set approval_status_sk = " + model.approval_status_sk + ", approval_status = '" + model.approval_status + "',rmks='" + model.rmks + "',update_dt_tme='" + model.update_dt_tme + "',updated_by=" + model.updated_by + " where doc_pool_sk = " + model.doc_pool_sk, con);
                con.Close();
            }
        }
        public DataTable GetPoolDocs(int userId)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    return db.getDataTable("SELECT * FROM setup.sp_pool_docs_list(" + userId + ") order by create_dt_tme", con);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetPoolDocs()");
                return null;
            }
        }
        public object Bindetails(int Id)
        {
            try
            {

                using (DbContextHelper dx = new DbContextHelper())
                {
                    con = db.getDatabaseConnection();
                    //DataTable dt = db.getDataTable("SELECT * FROM \"setup\".pools where (record_status = 0 OR record_status IS NULL) AND  pool_sk = " + Id, con);
                    DataTable dt = db.getDataTable("SELECT * FROM \"setup\".Vu_pools where pool_sk = " + Id, con);
                    pools obj = new pools();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        obj = dx.ConvertDataTable<pools>(dt)[0];

                        //dt = (db.getDataTable("SELECT * FROM \"setup\".pool_users_map WHERE (record_status = 0 OR record_status IS NULL) AND pool_sk = " + Id, con));
                        dt = (db.getDataTable("SELECT * FROM \"setup\".Vu_pool_users_map WHERE pool_sk = " + Id, con));

                        if (dt != null && dt.Rows.Count > 0)
                            obj.lst_users = dx.ConvertDataTable<pool_users_map>(dt);

                        //dt = (db.getDataTable("SELECT * FROM \"setup\".pool_cat_map WHERE (record_status = 0 OR record_status IS NULL) AND pool_sk = " + Id, con));
                        dt = (db.getDataTable("SELECT * FROM \"setup\".Vu_pool_cat_map WHERE pool_sk = " + Id, con));

                        if (dt != null && dt.Rows.Count > 0)
                            obj.lst_cat = dx.ConvertDataTable<pool_cat_map>(dt);

                        //dt = (db.getDataTable("SELECT * FROM \"setup\".pool_branch_map WHERE (record_status = 0 OR record_status IS NULL) AND pool_sk = " + Id, con));
                        dt = (db.getDataTable("SELECT * FROM \"setup\".Vu_pool_branch_map WHERE pool_sk = " + Id, con));

                        if (dt != null && dt.Rows.Count > 0)
                            obj.lst_branch = dx.ConvertDataTable<pool_branch_map>(dt);

                        dt = (db.getDataTable("SELECT * FROM \"setup\".Vu_pool_amt_map WHERE pool_sk = " + Id, con));
                        if (dt != null && dt.Rows.Count > 0)
                            obj.amt = dx.ConvertDataTable<pool_amt_map>(dt);

                        pools model = beforeGet(obj);
                        return obj;
                    }
                    else
                    {
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Bindetails()");
                return null;
            }
        }
        public DataTable Getdata()
        {
            try
            {
                //using (DbContextHelper dx = new DbContextHelper())
                //{
                con = db.getDatabaseConnection();
                string qry = "SELECT * FROM \"setup\".Vu_pools where (record_status = 0 or record_status is null) ORDER BY pool_sk DESC";
                DataTable dt = new DataTable();
                dt = db.getDataTable(qry, con);
                return dt;
                //return dx.SelectDataTable("SELECT * FROM \"setup\".pools where (record_status = 0 or record_status is null) ORDER BY pool_sk DESC");
                //}
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Getdata()");
                return null;
            }
        }

        public CustomObject Save(pools model)
        {
            try
            {
                using (ADOConnection dx = new ADOConnection())
                {
                    con = dx.getDatabaseConnection();
                    //dx.SelectDataTable("delete from \"setup\".pool_users_map where pool_sk = "+model.pool_sk+";" 
                    //    + "delete from \"setup\".pool_branch_map where pool_sk = " + model.pool_sk + ";"
                    //    + "delete from \"setup\".pool_cat_map where pool_sk = " + model.pool_sk + ";");
                    return dx.SaveChanges("setup", model);

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
        private pools beforeGet(pools model)
        {
            if (model.lst_users != null && model.lst_users.Count > 0)
            {
                foreach (var va in model.lst_users)
                {
                    model.Users += va.user_sk + ",";
                }
                model.Users = model.Users.TrimStart(',');
                model.Users = model.Users.TrimEnd(',');
            }
            if (model.lst_branch != null && model.lst_branch.Count > 0)
            {
                foreach (var va in model.lst_branch)
                {
                    model.branches += va.br_code + ",";
                }
                model.branches = model.branches.TrimStart(',');
                model.branches = model.branches.TrimEnd(',');
            }
            if (model.lst_cat != null && model.lst_cat.Count > 0)
            {
                foreach (var va in model.lst_cat)
                {
                    model.categories += va.aprv_cat_sk + ",";
                }
                model.categories = model.categories.TrimStart(',');
                model.categories = model.categories.TrimEnd(',');
            }
            if (model.amt != null && model.amt.Count > 0)
            {
                foreach (var va in model.amt)
                {
                    model.max_amt += va.max_amt;
                    model.min_amt += va.min_amt;
                }
            }
            return model;
        }
        public CustomObject Savepooldoc(string param)
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
                    process.StartInfo.FileName = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["POOLCREATION_API_LOC"].ToString(); // Replace with the actual console application name
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory + Path.GetDirectoryName(ConfigurationManager.AppSettings["POOLCREATION_API_LOC"].ToString());
                    // Start the process
                    process.Start();

                    // Write input to the console application
                    //string input = "2050007000469801";
                    process.StandardInput.WriteLine(param);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();

                    // Read the response from the console application
                    string response = process.StandardOutput.ReadToEnd();

                    process.WaitForExit();
                    process.Close();
                    CustomObject cust_obj = new CustomObject();
                    cust_obj.Data = null;
                    cust_obj.Message = response.ToString();
                    return cust_obj;
                    #endregion
                    //return db.SaveChanges("setup", model);

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

        public string PoolForward(List<vu_pool_docs> lst)
        {
            using (ADOConnection dx = new ADOConnection())
            {
                int t = 0;
                int f = 0;
                string qhistory = "";
                string qforward = "";
                string qdisabled = "";
                con = dx.getDatabaseConnection();
                int i = 0;
                foreach (var model in lst)
                {
                    DataTable dt = dx.getDataTable("SELECT * FROM setup.vu_pool_docs_mst_frwd where doc_typ_sk = " + model.doc_typ_sk + " and doc_mst_sk = " + model.doc_mst_sk + " and br_sk = " + model.br_sk + " and aprv_cat_sk = " + model.aprv_cat_sk, con);
                    if (dt == null || dt.Rows.Count < 1)
                    {
                        qhistory += "insert into setup.vu_pool_docs_history SELECT doc_pool_sk, doc_typ_sk, doc_mst_sk, doc_no, doc_dte, br_sk, aprv_cat_sk, " + model.pool_sk + " as pool_sk, doc_link, approval_status, approval_status_sk, rmks, '" + lst[0].create_dt_tme + "' as create_dt_tme, " + lst[0].created_by + " as created_by, null as update_dt_tme, null as updated_by, record_status, row_version  ,recallreason,scrn_rslt_sk,txn_amt FROM setup.vu_pool_docs where doc_pool_sk = (" + model.doc_pool_sk + ");";
                        qforward += "insert into setup.vu_pool_docs_forward SELECT doc_pool_sk, doc_typ_sk, doc_mst_sk, doc_no, doc_dte, br_sk, aprv_cat_sk, " + model.pool_sk + " as pool_sk, doc_link, approval_status, approval_status_sk, rmks, '" + lst[0].create_dt_tme + "' as create_dt_tme, " + lst[0].created_by + " as created_by, null as update_dt_tme, null as updated_by, record_status, row_version  ,recallreason,txn_amt FROM setup.vu_pool_docs where doc_pool_sk = (" + model.doc_pool_sk + ");";
                        qdisabled += "insert into setup.vu_pool_docs_disabled SELECT doc_pool_sk, doc_typ_sk, doc_mst_sk, doc_no, doc_dte, br_sk, aprv_cat_sk, " + model.pool_sk + " as pool_sk, doc_link, approval_status, approval_status_sk, rmks, '" + lst[0].create_dt_tme + "' as create_dt_tme, " + lst[0].created_by + " as created_by, null as update_dt_tme, null as updated_by, record_status, row_version,recallreason,txn_amt FROM setup.vu_pool_docs where doc_pool_sk = (" + model.doc_pool_sk + ");";
                        i = dx.doInsertUpdate(qdisabled + qforward + qhistory, con);
                        f = i > 0 ? f++ : 0;
                    }
                    else
                    {
                        t++;
                    }
                }

                con.Close();
                return f + "," + t;
                //return dx.doinsertupdatedeleteNoAsync("update setup.pool_docs set pool_sk = " + model.aprv_cat_sk + ",updated_by = " + model.updated_by + ",update_dt_tme='" + model.update_dt_tme + "' where doc_pool_sk in (" + model.Pool_doc_sk_arr + ")");
            }
        }
        public bool CheckIsExist(vu_pool_docs model)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    DataTable dt = db.getDataTable("SELECT * FROM setup.vu_pool_docs where doc_typ_sk = " + model.doc_typ_sk + " and doc_mst_sk = " + model.doc_mst_sk + " and aprv_cat_sk = " + model.aprv_cat_sk + "and approval_status not in ('Rejected','Approved') and approval_status_sk not in (2,7,9)", con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //if (model.doc_typ_sk == 3014 || model.doc_typ_sk == 3007)
                        //{
                        //    int i = dx.doinsertupdatedeleteNoAsync("update \"setup\".vu_pool_docs set approval_status_sk = 9 where doc_typ_sk = " + model.doc_typ_sk + " and doc_mst_sk = " + model.doc_mst_sk);
                        //    i = dx.doinsertupdatedeleteNoAsync("update \"setup\".vu_pool_docs set approval_status_sk = 8,update_dt_tme= '" + model.create_dt_tme + "',updated_by = " + model.created_by + ",row_version = row_version + 1 where doc_pool_sk = (select doc_pool_sk from setup.vu_pool_docs where doc_typ_sk = " + model.doc_typ_sk + " and doc_mst_sk = " + model.doc_mst_sk + " and aprv_cat_sk = " + model.aprv_cat_sk + " order by doc_pool_sk desc limit 1)");
                        //    return true;
                        //}
                        //else
                        //{
                        //    return true;
                        //}
                        int i = db.doInsertUpdate("update setup.vu_pool_docs set approval_status_sk = 9 where doc_typ_sk = " + model.doc_typ_sk + " and doc_mst_sk = " + model.doc_mst_sk, con);
                        i = db.doInsertUpdate("update setup.vu_pool_docs set approval_status_sk = 8,update_dt_tme= '" + model.create_dt_tme + "',updated_by = " + model.created_by + ",row_version = row_version + 1 where doc_pool_sk = (select doc_pool_sk from setup.vu_pool_docs where doc_typ_sk = " + model.doc_typ_sk + " and doc_mst_sk = " + model.doc_mst_sk + " and aprv_cat_sk = " + model.aprv_cat_sk + " order by doc_pool_sk desc limit 1)", con);
                        con.Close();
                        return true;
                    }
                    else
                    {
                        con.Close();
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public CustomObject UpdatePoolDocs(vu_pool_docs_history model)
        {
            try
            {
                CustomObject obj = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    db.SaveChanges("setup", model);
                    int i = db.doInsertUpdate("update setup.vu_pool_docs set create_dt_tme = '" + model.create_dt_tme + "' where doc_typ_sk = " + model.doc_typ_sk + " and doc_mst_sk =" + model.doc_mst_sk + " and approval_status not in ('Rejected','Approved')", con);
                    if (i > 0)
                    {
                        obj.Message = "Record has been updated successfully";
                    }
                    else
                    {
                        obj.Message = "Record not updated";
                    }

                    con.Close();
                }
                return obj;
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "UpdatePoolDocs()");
                return null;
            }
        }
        private string GetQueryforpooldocupdate(int doc_typ_sk, int doc_mst_sk, int status_sk)
        {
            string query = "";
            if (status_sk == 2)
            {
                if (doc_typ_sk == 3001) // User Management 
                {
                    query = "";
                    ApprovalUsermanagementProccess(doc_mst_sk, status_sk);
                }
                else if (doc_typ_sk == 3002)// User Role Management 
                {
                    query = "";
                    RoleMstAndRoleLvlCanDo(doc_mst_sk, status_sk);
                }
                else
                {
                    query = "";
                }
            }
            return query;
        }

        private void ApprovalUsermanagementProccess(int doc_mst_sk, int status_sk)
        {
            try
            {

                SqlConnection con = new SqlConnection();
                EncryptDecrypt enc = new EncryptDecrypt();
                if (status_sk == 2)
                {
                    #region Approve
                    using (Connection dx = new Connection())
                    {
                        con = dx.getDatabaseConnection();
                        #region Users
                        //DataTable users_aprv = dx.getDataTable("select * from security.users_aprv where user_sk = " + doc_mst_sk);
                        DataTable users_aprv = dx.getDataTable("select * from security.Vu_users_aprv where user_sk = " + doc_mst_sk, con);
                        int userid = 0;
                        if (users_aprv != null && users_aprv.Rows.Count > 0)
                        {
                            string encusername = users_aprv.Rows[0]["username"].ToString();
                            userid = Convert.ToInt32(users_aprv.Rows[0]["user_sk"].ToString());
                            //DataTable dtusers = dx.getDataTable("select * from security.users where username = '" + encusername + "'");
                            DataTable dtusers = dx.getDataTable("select * from security.users where id = '" + doc_mst_sk + "'", con);
                            if (dtusers != null && dtusers.Rows.Count > 0) // Update
                            {
                                userid = Convert.ToInt32(dtusers.Rows[0]["id"].ToString());
                                //Archieve User
                                int i = dx.doInsertUpdate("insert into security.users_archv select * from security.users where id = " + userid,con);

                                //Update User
                                List<users> model = dx.ConvertDataTable<users>(users_aprv);
                                if (model != null)
                                {
                                    model[0].username = users_aprv.Rows[0]["username"].ToString();
                                    model[0].password = users_aprv.Rows[0]["password"].ToString();
                                    model[0].updtd_by_IP = users_aprv.Rows[0]["updtd_by_IP"].ToString();
                                    model[0].id = Convert.ToInt32(dtusers.Rows[0]["id"].ToString());
                                    model[0].row_version = Convert.ToInt32(dtusers.Rows[0]["row_version"].ToString());
                                    model[0].state = "Changed";
                                    model[0].user_ful_name = users_aprv.Rows[0]["user_full_name"].ToString();
                                    CustomObject obj = dx.SaveChanges("security", model[0]);
                                }
                            }
                            else // Insert
                            {
                                List<users> model = dx.ConvertDataTable<users>(users_aprv);
                                if (model != null)
                                {
                                    model[0].username = users_aprv.Rows[0]["username"].ToString();
                                    model[0].password = users_aprv.Rows[0]["password"].ToString();
                                    model[0].usr_creatd_id = Convert.ToInt32(users_aprv.Rows[0]["created_by"].ToString());
                                    model[0].created = Convert.ToDateTime(users_aprv.Rows[0]["created_on"].ToString());
                                    model[0].user_ful_name = users_aprv.Rows[0]["user_full_name"].ToString();
                                    CustomObject obj = dx.SaveChanges("security", model[0]);
                                    if (obj.status == true)
                                    {
                                        userid = Convert.ToInt32(obj.Data.Rows[0]["id"].ToString());
                                    }
                                }
                            }
                        }
                        #endregion

                        #region user_br_mapping
                        //DataTable dtuser_br_mapping_aprv = dx.getDataTable("select * from security.user_br_mapping_aprv where user_sk = " + doc_mst_sk);
                        DataTable dtuser_br_mapping_aprv = dx.getDataTable("select * from security.Vu_user_br_mapping_aprv where user_sk = " + doc_mst_sk, con);
                        //DataTable dtuser_role_mapping_aprv = dx.getDataTable("select * from security.user_role_mapping_aprv where user_sk = " + doc_mst_sk);
                        //DataTable dtuser_lvl_can_do_aprv = dx.getDataTable("select * from security.user_lvl_can_do_aprv where user_id = " + doc_mst_sk);

                        if (dtuser_br_mapping_aprv != null && dtuser_br_mapping_aprv.Rows.Count > 0)
                        {
                            //DataTable br_mapping = dx.getDataTable("select * from security.user_br_mapping where user_sk = " + userid);
                            DataTable br_mapping = dx.getDataTable("select * from security.Vu_user_br_mapping where user_sk = " + userid, con);
                            if (br_mapping != null && br_mapping.Rows.Count > 0) // Update
                            {
                                //Archieve User
                                int ires = dx.doInsertUpdate("update security.user_br_mapping set invalidDatetime = '" + un.GetDateTimeForTimeZone() + "' where user_sk = " + userid + ";", con);

                                //Update User
                                List<vu_user_br_mapping> model = dx.ConvertDataTable<vu_user_br_mapping>(dtuser_br_mapping_aprv);
                                foreach (var va in model)
                                {
                                    if (va != null)
                                    {
                                        va.user_sk = userid;
                                        va.mapping_sk = 0;
                                        va.state = "";
                                        va.is_default = true;
                                        CustomObject obj = dx.SaveChanges("security", va);
                                    }
                                }
                            }
                            else // Insert
                            {
                                List<vu_user_br_mapping> model = dx.ConvertDataTable<vu_user_br_mapping>(dtuser_br_mapping_aprv);
                                foreach (var va in model)
                                {
                                    if (va != null)
                                    {
                                        va.user_sk = userid;
                                        va.mapping_sk = 0;
                                        va.state = "";
                                        va.is_default = true;
                                        CustomObject obj = dx.SaveChanges("security", va);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region user_role_mapping
                        //DataTable dtuser_role_mapping_aprv = dx.getDataTable("select * from security.user_role_mapping_aprv where user_sk = " + doc_mst_sk);
                        DataTable dtuser_role_mapping_aprv = dx.getDataTable("select * from security.Vu_user_role_mapping_aprv where user_sk = " + doc_mst_sk, con);
                        //DataTable dtuser_lvl_can_do_aprv = dx.getDataTable("select * from security.user_lvl_can_do_aprv where user_id = " + doc_mst_sk);

                        if (dtuser_role_mapping_aprv != null && dtuser_role_mapping_aprv.Rows.Count > 0)
                        {
                            //DataTable role_mapping = dx.getDataTable("select * from security.user_role_mapping where user_sk = " + userid);
                            DataTable role_mapping = dx.getDataTable("select * from security.Vu_user_role_mapping where user_sk = " + userid, con);
                            if (role_mapping != null && role_mapping.Rows.Count > 0) // Update
                            {
                                //Archieve User
                                int ires = dx.doInsertUpdate("update security.user_role_mapping set invalidDatetime = '" + un.GetDateTimeForTimeZone() + "' where user_sk = " + userid + ";", con);

                                //Update User
                                List<vu_user_role_mapping> model = dx.ConvertDataTable<vu_user_role_mapping>(dtuser_role_mapping_aprv);
                                foreach (var va in model)
                                {
                                    if (va != null)
                                    {
                                        va.user_sk = userid;
                                        va.usr_rol_mapping_sk = 0;
                                        va.state = "";
                                        CustomObject obj = dx.SaveChanges("security", va);
                                    }
                                }
                            }
                            else // Insert
                            {
                                List<vu_user_role_mapping> model = dx.ConvertDataTable<vu_user_role_mapping>(dtuser_role_mapping_aprv);
                                foreach (var va in model)
                                {
                                    if (va != null)
                                    {
                                        va.user_sk = userid;
                                        va.usr_rol_mapping_sk = 0;
                                        va.state = "";
                                        CustomObject obj = dx.SaveChanges("security", va);
                                    }
                                }
                            }
                        }
                        #endregion

                        #region user_lvl_can_do
                        //DataTable dtuser_lvl_can_do_aprv = dx.getDataTable("select * from security.user_lvl_can_do_aprv where user_id = " + doc_mst_sk);
                        DataTable dtuser_lvl_can_do_aprv = dx.getDataTable("select * from security.user_lvl_can_do_aprv where user_id = " + doc_mst_sk, con);

                        if (dtuser_lvl_can_do_aprv != null && dtuser_lvl_can_do_aprv.Rows.Count > 0)
                        {
                            //DataTable role_mapping = dx.getDataTable("select * from security.user_lvl_can_do where user_id = " + userid);
                            DataTable role_mapping = dx.getDataTable("select * from security.user_lvl_can_do where user_id = " + userid, con);
                            if (role_mapping != null && role_mapping.Rows.Count > 0) // Update
                            {
                                //Archieve User
                                //int ires = dx.doInsertUpdate("insert into security.user_lvl_can_do_archv select * from security.user_lvl_can_do_aprv where user_id = " + userid + "; Delete from security.user_lvl_can_do where user_id = " + userid, con);
                                int ires = dx.doInsertUpdate("update security.user_lvl_can_do set invalidDatetime = '" + un.GetDateTimeForTimeZone() + "' where user_id = " + userid+"", con);
                                //Update User
                                List<vu_user_lvl_can_do> model = dx.ConvertDataTable<vu_user_lvl_can_do>(dtuser_lvl_can_do_aprv);
                                foreach (var va in model)
                                {
                                    if (va != null)
                                    {
                                        va.user_sk = userid;
                                        va.user_cando_sk = 0;
                                        va.state = "";
                                        CustomObject obj = dx.SaveChanges("security", va);
                                    }
                                }
                            }
                            else // Insert
                            {
                                List<vu_user_lvl_can_do> model = dx.ConvertDataTable<vu_user_lvl_can_do>(dtuser_lvl_can_do_aprv);
                                foreach (var va in model)
                                {
                                    if (va != null)
                                    {
                                        va.user_sk = userid;
                                        va.user_cando_sk = 0;
                                        va.state = "";
                                        CustomObject obj = dx.SaveChanges("security", va);
                                    }
                                }
                            }
                            #endregion
                            con.Close();
                        }
                    }
                    #endregion
                }
                else if (status_sk == 7)
                {

                    #region Rejected

                    using (Connection dx = new Connection())
                    {
                        con = dx.getDatabaseConnection();
                        #region Users
                        //DataTable users_aprv = dx.getDataTable("select * from security.users_aprv where user_sk = " + doc_mst_sk);
                        DataTable users_aprv = dx.getDataTable("select * from security.Vu_users_aprv where user_sk = " + doc_mst_sk, con);
                        int userid = 0;
                        int userid_aprv = 0;
                        string encusername = "";
                        if (users_aprv != null && users_aprv.Rows.Count > 0)
                        {
                            encusername = enc.Encrypt(users_aprv.Rows[0]["username"].ToString());
                            userid_aprv = Convert.ToInt32(users_aprv.Rows[0]["user_sk"].ToString());
                        }
                        //DataTable dtusers = dx.getDataTable("select * from security.users where username = '" + encusername + "'");
                        DataTable dtusers = dx.getDataTable("select * from security.Vu_users where username = '" + encusername + "'", con);

                        if (dtusers != null && dtusers.Rows.Count > 0)
                        {
                            //string encusername = enc.Encrypt(users_aprv.Rows[0]["username"].ToString());
                            userid = Convert.ToInt32(dtusers.Rows[0]["id"].ToString());
                            //int ires = dx.doinsertupdatedeleteNoAsync("Delete from security.users_aprv where user_sk = " + userid_aprv);
                            List<vu_users_aprv> model = dx.ConvertDataTable<vu_users_aprv>(dtusers);
                            if (model != null)
                            {
                                model[0].user_full_name = users_aprv.Rows[0]["user_full_name"].ToString();
                                model[0].username = enc.Decrypt(dtusers.Rows[0]["username"].ToString());
                                model[0].password = enc.Decrypt(dtusers.Rows[0]["password"].ToString());
                                CustomObject obj = dx.SaveChanges("security", model[0]);
                            }

                        }
                        #endregion

                        #region user_br_mapping
                        //DataTable dtuser_br_mapping = dx.getDataTable("select * from security.user_br_mapping where user_sk = " + userid);
                        DataTable dtuser_br_mapping = dx.getDataTable("select * from security.Vu_user_br_mapping where user_sk = " + userid, con);
                        //DataTable dtuser_role_mapping_aprv = dx.getDataTable("select * from security.user_role_mapping_aprv where user_sk = " + doc_mst_sk);
                        //DataTable dtuser_lvl_can_do_aprv = dx.getDataTable("select * from security.user_lvl_can_do_aprv where user_id = " + doc_mst_sk);

                        if (dtuser_br_mapping != null && dtuser_br_mapping.Rows.Count > 0)
                        {
                            //int ires = dx.doinsertupdatedeleteNoAsync("Delete from security.user_br_mapping_aprv where user_sk = " + userid_aprv);
                            List<vu_user_br_mapping_aprv> model = dx.ConvertDataTable<vu_user_br_mapping_aprv>(dtuser_br_mapping);
                            foreach (var va in model)
                            {
                                if (va != null)
                                {
                                    va.mapping_sk = 0;
                                    va.user_sk = userid_aprv;
                                    CustomObject obj = dx.SaveChanges("security", va);
                                }
                            }
                        }
                        #endregion

                        #region user_role_mapping
                        //DataTable dtuser_role_mapping = dx.getDataTable("select * from security.user_role_mapping where user_sk = " + userid);
                        DataTable dtuser_role_mapping = dx.getDataTable("select * from security.Vu_user_role_mapping where user_sk = " + userid, con);

                        if (dtuser_role_mapping != null && dtuser_role_mapping.Rows.Count > 0)
                        {
                            //int ires = dx.doinsertupdatedeleteNoAsync("Delete from security.user_role_mapping_aprv where user_sk = " + userid_aprv);
                            List<vu_user_role_mapping_aprv> model = dx.ConvertDataTable<vu_user_role_mapping_aprv>(dtuser_role_mapping);
                            foreach (var va in model)
                            {
                                if (va != null)
                                {
                                    va.usr_rol_mapping_sk = 0;
                                    va.user_sk = userid_aprv;
                                    CustomObject obj = dx.SaveChanges("security", va);
                                }
                            }
                        }
                        #endregion

                        #region user_lvl_can_do
                        //DataTable dtuser_lvl_can_do = dx.getDataTable("select * from security.user_lvl_can_do where user_id = " + userid);
                        DataTable dtuser_lvl_can_do = dx.getDataTable("select * from security.Vu_user_lvl_can_do where user_id = " + userid, con);

                        if (dtuser_lvl_can_do != null && dtuser_lvl_can_do.Rows.Count > 0)
                        {
                            //int ires = dx.doinsertupdatedeleteNoAsync("Delete from security.user_lvl_can_do_aprv where user_id = " + userid_aprv);
                            List<vu_user_lvl_can_do_aprv> model = dx.ConvertDataTable<vu_user_lvl_can_do_aprv>(dtuser_lvl_can_do);
                            foreach (var va in model)
                            {
                                if (va != null)
                                {
                                    va.user_cando_sk = 0;
                                    va.user_sk = userid_aprv;
                                    CustomObject obj = dx.SaveChanges("security", va);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                
            }

        }
        public bool CheckRange(int max, int min)
        {
            try
            {
                con = db.getDatabaseConnection();
                DataTable dt = db.getDataTable("SELECT COUNT(*) [count] FROM setup.vu_pool_amt_map WHERE max_amt BETWEEN " + min + " AND " + max + " OR min_amt BETWEEN " + min + " AND " + max + ";", con);
                return (Convert.ToInt32(dt.Rows[0][0]) == 0) ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Role Setup Approval and Rejection Process
        ///  //if Approved
        //  get data from setup.vu_role_mst_aprv and security.vu_role_lvl_can_do_aprv on docMstsk
        //  
        //  if exist 
        //  Archive Main Tables Data
        //  Delete Main Tables Data
        //  Insert Approved/Fetched Data

        //  if Rejected
        //  Get Data from Main Tables
        //  Delete from setup.vu_role_mst_aprv and security.vu_role_lvl_can_do_aprv on docMstSk
        //  Insert into setup.vu_role_mst_aprv and security.vu_role_lvl_can_do_aprv on docMstSk (if found from Main Tables)
        /// </summary>
        private bool RoleMstAndRoleLvlCanDo(int roleSk, int statusSk)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                if (statusSk == (int)ApprovalStatus.Approved)
                {
                    try
                    {
                        using (var dx = new Connection())
                        {
                            con = dx.getDatabaseConnection();
                            #region RoleMst_Rolelvl_can_do

                            //get mst and txn data
                            DataTable roleMstDt = dx.getDataTable("select * from security.vu_role_mst_aprv where role_sk = " + roleSk, con);
                            DataTable roleLvlCanDoDt = dx.getDataTable("select * from security.vu_role_lvl_can_do_aprv where role_sk = " + roleSk, con);

                            if ((roleMstDt != null && roleMstDt.Rows.Count > 0) && (roleLvlCanDoDt != null && roleLvlCanDoDt.Rows.Count > 0))
                            {
                                //check existence in main tables
                                // DataTable roleMstDtCount = dx.SelectDataTable("select * from security.vu_role_mst_aprv where role_sk = " + roleSk);
                                // DataTable roleLvlCanDoDtCount = dx.SelectDataTable("select * from security.vu_role_lvl_can_do_aprv where role_sk = " + roleSk);
                                //archive tables                              
                                try
                                {
                                    //archive mst
                                    int i = dx.doInsertUpdate("insert into security.role_mst_arch select * from security.role_mst where role_sk = " + roleSk + "; Delete from security.role_mst where role_sk = " + roleSk,con);
                                    //int i = dx.doInsertUpdate("update security.role_mst set InvalidDateTime = '" + un.GetDateTimeForTimeZone() + "',record_status=1 where role_sk = " + roleSk, con);
                                    dx.doInsertUpdate("update security.role_lvl_can_do set InvalidDateTime = '" + un.GetDateTimeForTimeZone() + "', record_status=1 where role_sk = " + roleSk, con);
                                    con.Close();
                                }
                                catch (Exception ex)
                                {
                                    return false;
                                }

                                //Insert Approved Data in Main Tables
                                var mstModel = dx.ConvertDataTable<vu_role_mst>(roleMstDt);
                                var txnModels = dx.ConvertDataTable<vu_role_lvl_can_do>(roleLvlCanDoDt);
                                try
                                {

                                    if (mstModel != null)
                                    {
                                        DataTable MainDt = dx.getDataTable("select * from security.vu_role_mst where role_sk = " + roleSk, con);
                                        if (MainDt.Rows.Count > 0 && MainDt != null)
                                        {

                                        }
                                        else
                                        {
                                            mstModel[0].row_version = 0;
                                            mstModel[0].role_sk = 0;
                                        }
                                        CustomObject obj = dx.SaveChanges("security", mstModel.FirstOrDefault());
                                        if (obj.status)
                                        {
                                            foreach (var txn in txnModels)
                                            {
                                                txn.row_version = 0;
                                                txn.role_sk = Convert.ToInt32(obj.Data.Rows[0]["role_sk"].ToString());
                                                txn.role_cando_sk = 0;
                                                CustomObject cobj = dx.SaveChanges("security", txn);
                                            }
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
                                catch (Exception ex)
                                {
                                    return false;
                                }



                                #endregion
                            }
                            else
                            {
                                return false;
                            }


                        }
                    }

                    catch (Exception ex)
                    {
                        return false;

                    }

                }
                //else if (statusSk == (int)ApprovalStatus.Rejected)
                //{
                //    try
                //    {
                //        using (var dx = new Connection())
                //        {
                //            #region RoleMst_Rolelvl_can_do Rejection Process
                //            con = dx.getDatabaseConnection();
                //            //get mst and txn data
                //            DataTable roleMstDt = dx.getDataTable("select * from security.vu_role_mst where role_sk = " + roleSk, con);
                //            DataTable roleLvlCanDoDt = dx.getDataTable("select * from security.vu_role_lvl_can_do where role_sk = " + roleSk,con);

                //            if ((roleMstDt != null && roleMstDt.Rows.Count > 0) && (roleLvlCanDoDt != null && roleLvlCanDoDt.Rows.Count > 0))
                //            {
                //                //check existence in main tables
                //                // DataTable roleMstDtCount = dx.SelectDataTable("select * from security.vu_role_mst_aprv where role_sk = " + roleSk);
                //                // DataTable roleLvlCanDoDtCount = dx.SelectDataTable("select * from security.vu_role_lvl_can_do_aprv where role_sk = " + roleSk);

                //                //archive tables                              
                //                try
                //                {
                //                    //archive mst
                //                    //int i = dx.doinsertupdatedeleteNoAsync("insert into security.role_mst_arch select * from security.role_mst_aprv where role_sk = " + roleSk + "; Delete from security.role_mst_aprv where role_sk = " + roleSk);

                //                    //i = dx.doinsertupdatedeleteNoAsync("insert into security.vu_role_lvl_can_do_arch select * from security.vu_role_lvl_can_do_aprv where role_sk = " + roleSk + "; Delete from security.vu_role_lvl_can_do_aprv where role_sk = " + roleSk);
                //                    con.Close();
                //                }
                //                catch (Exception ex)
                //                {
                //                    return false;
                //                }

                //                //Insert Main Tables Data into Staging
                //                var mstModel = dx.ConvertDataTable<vu_role_mst_aprv>(roleMstDt);
                //                var txnModels = dx.ConvertDataTable<vu_role_lvl_can_do_aprv>(roleLvlCanDoDt);
                //                try
                //                {
                //                    mstModel[0].row_version = 0;
                //                    mstModel[0].role_sk = 0;
                //                    if (mstModel != null)
                //                    {
                //                        CustomObject obj = dx.SaveChanges("security", mstModel.FirstOrDefault());
                //                        if (obj.status)
                //                        {
                //                            foreach (var txn in txnModels)
                //                            {
                //                                txn.row_version = 0;
                //                                txn.role_sk = Convert.ToInt32(obj.Data.Rows[0]["role_sk"].ToString());
                //                                txn.role_cando_sk = 0;
                //                                dx.SaveChanges("security", txn);
                //                            }
                //                            return true;
                //                        }
                //                        else
                //                        {
                //                            return false;
                //                        }
                //                    }
                //                    else
                //                    {
                //                        return false;
                //                    }

                //                }
                //                catch (Exception ex)
                //                {
                //                    return false;
                //                }



                //                #endregion
                //            }
                //            else
                //                return false;


                //        }
                //    }

                //    catch (Exception ex)
                //    {
                //        return false;

                //    }
                //}
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int GetReviewLog(int doc_typ_sk, int doc_mst_sk, int pool_doc_sk)
        {
            try
            {
                using (ADOConnection dx = new ADOConnection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    DataTable dt = dx.getDataTable("SELECT COUNT(*) FROM setup.vu_pool_docs_rvw_log WHERE doc_typ_sk = " + doc_typ_sk + " AND doc_mst_sk = " + doc_mst_sk + " AND pool_doc_sk = " + pool_doc_sk, con);

                    if (dt != null && dt.Rows.Count > 0)
                        return int.Parse(dt.Rows[0][0].ToString());
                }
            }
            catch (Exception ex)
            {

            }

            return 0;
        }
        public CustomObject GenerateReviewLog(vu_pool_docs_rvw_log model)
        {
            try
            {
                CustomObject obj = new CustomObject();
                using (Connection dx = new Connection())
                {
                    obj = dx.SaveChanges("setup", model);
                }
                return obj;
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GenerateReviewLog()");
                return null;
            }
        }
        public int UpdateStatusForDocument(int doc_typ_sk, int doc_mst_sk, int status_sk, int user_sk)
        {
            try
            {
                using (ADOConnection dx = new ADOConnection())
                {
                    string query = "";
                    SqlConnection con = db.getDatabaseConnection();
                    UniversalRepository repo = new UniversalRepository();

                    if (doc_typ_sk == 3001) // User Account
                        query = "UPDATE security.users_aprv SET status_sk = " + status_sk + ", row_version = (row_version+1), updated_by = " + user_sk + ", updated_on = '" + repo.GetDateTimeForTimeZone() + "' WHERE user_sk = " + doc_mst_sk;
                    else if (doc_typ_sk == 3002) // Role Creation
                        query = "UPDATE security.role_mst_aprv SET status_sk = " + status_sk + ", row_version = (row_version+1), updated_by = " + user_sk + ", updated_on = '" + repo.GetDateTimeForTimeZone() + "' WHERE role_sk = " + doc_mst_sk;
                    int i = dx.doInsertUpdate(query, con);
                    GetQueryforpooldocupdate(doc_typ_sk, doc_mst_sk, status_sk);
                    con.Close();
                    return i;
                }
            }
            catch (Exception ex)
            {

            }

            return 0;
        }
        
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}