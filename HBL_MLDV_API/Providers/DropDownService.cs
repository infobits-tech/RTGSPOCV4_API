using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Models;
using HBL_MLDV_API.Models.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers
{
    public class DropDownService : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        ADOConnection db = new ADOConnection();
        SqlConnection con = new SqlConnection();
        public List<DropDownModel> GetStatusMasterList()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("select status_sk,status_desc from status_mst where (record_status = 0 OR record_status is null) ORDER BY status_sk", con);
                    record_txn = db.getDataTable("select status_sk,status_desc from Vu_status_mst where (record_status = 0 OR record_status is null) ORDER BY status_sk", con);
                }

                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {

                            value = record_txn.Rows[i]["status_sk"].ToString(),
                            text = record_txn.Rows[i]["status_desc"].ToString(),

                        });
                    }
                    con.Close();
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetStatusMasterList()");
                return null;
            }
        }
        public List<DropDownModel> GetActivePools()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                con = db.getDatabaseConnection();
                //string qry = "select pool_sk,pool_nme from \"setup\".pools where record_status = 0 OR record_status is null ORDER BY pool_nme";
                string qry = "select pool_sk,pool_nme from \"setup\".Vu_pools where record_status = 0 OR record_status is null ORDER BY pool_nme";
                DataTable record_txn = db.getDataTable(qry, con);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["pool_sk"].ToString(),
                            text = record_txn.Rows[i]["pool_nme"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return lst = new List<DropDownModel>();
                }
            }
            catch (Exception ex)
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                return lst;
            }
        }
        public List<DropDownModel> pool_action()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                con = db.getDatabaseConnection();
                string qry = "select * from \"setup\".vu_pool_actions order by pool_sk desc";
                DataTable record_txn = db.getDataTable(qry, con);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["pool_sk"].ToString(),
                            text = record_txn.Rows[i]["pool_nme"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return lst = new List<DropDownModel>();
                }
            }
            catch (Exception ex)
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                return lst;
            }
        }
        public List<gen_snder_ref> GetReference(string msg_typ)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<gen_snder_ref> lst = new List<gen_snder_ref>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.VU_gen_snder_ref where msg_typ = '" + msg_typ + "'", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = db.ConvertDataTable<gen_snder_ref>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReference()");
                return null;
            }
        }
        public List<Vu_BANKS_CODE_MAPPING> Get53()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<Vu_BANKS_CODE_MAPPING> lst = new List<Vu_BANKS_CODE_MAPPING>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM [TR_APP].[get53A]", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = db.ConvertDataTable<Vu_BANKS_CODE_MAPPING>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Get53()");
                return null;
            }
        }
        /// <summary>
        /// Get all active countries
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable GetActiveCountries()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //return db.getDataTable("SELECT * FROM country_mst WHERE (record_status = 0 OR record_status IS NULL) ORDER BY country_desc", con);
                    return db.getDataTable("SELECT * FROM Vu_country_mst ORDER BY country_desc", con);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetActiveCountries()");
                return null;
            }
        }
        public List<DropDownModel> GetUserRoles()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    record_txn = db.getDataTable("select role_sk,role_desc from security.vu_role_mst", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["role_sk"].ToString(),
                            text = record_txn.Rows[i]["role_desc"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetUserRoles()");
                return null;
            }
        }

        public List<DropDownModel> GetUsers()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                string cmd = "select user_sk,user_ful_name from security.vu_users where (record_status = 0 OR record_status is null) ORDER BY user_ful_name";
                con = db.getDatabaseConnection();
                DataTable record_txn = db.getDataTable(cmd, con);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["user_sk"].ToString(),
                            text = record_txn.Rows[i]["user_ful_name"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetUsers()");
                return null;
            }
        }

        public List<DropDownModel> GetBranches()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("select br_code,br_desc from branch_mst where (record_status = 0 OR record_status is null) ORDER BY br_desc", con);
                    record_txn = db.getDataTable("select br_code,br_desc from Vu_branch_mst ORDER BY br_desc", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["br_code"].ToString(),
                            text = record_txn.Rows[i]["br_desc"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetBranches()");
                return null;
            }
        }

        public List<DropDownModel> GetRcivrlst()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [BIC], [AC_NO], bank_Code as [Code] FROM [TR_APP].[BANKS_CODE_MAPPING]", con);
                    record_txn = db.getDataTable("SELECT [BIC], bank_Code as [Code] FROM [TR_APP].[Vu_BANKS_CODE_MAPPING]", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["BIC"].ToString(),
                            text = record_txn.Rows[i]["BIC"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetRcivrlst()");
                return null;
            }
        }
        public List<DropDownModel> getchart()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [BIC], [AC_NO], bank_Code as [Code] FROM [TR_APP].[BANKS_CODE_MAPPING]", con);
                    record_txn = db.getDataTable("select * from test_chart", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["login_count"].ToString(),
                            text = record_txn.Rows[i]["url_dte_tme"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<DropDownModel> GetBankCode()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [BIC], [AC_NO], bank_Code as [Code] FROM [TR_APP].[BANKS_CODE_MAPPING]", con);
                    record_txn = db.getDataTable("SELECT BANK_NAME , AC_NO FROM [TR_APP].[Vu_BANKS_CODE_MAPPING]", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["AC_NO"].ToString(),
                            text = record_txn.Rows[i]["BANK_NAME"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetBankCode()");
                return null;
            }
        }
        public List<DropDownModel> GetReportType()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [BIC], [AC_NO], bank_Code as [Code] FROM [TR_APP].[BANKS_CODE_MAPPING]", con);
                    record_txn = db.getDataTable("select * from tr_app.vu_Reports_In_Out", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["Report_Id"].ToString(),
                            text = record_txn.Rows[i]["Report_Names"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReportType()");
                return null;
            }
        }

        public List<DropDownModel> GetReserve(int doc_typ_sk)
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [BIC], [AC_NO], bank_Code as [Code] FROM [TR_APP].[BANKS_CODE_MAPPING]", con);
                    record_txn = db.getDataTable("select * from security.Vu_param_dtl where doc_typ_sk = " + doc_typ_sk + "", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        if (doc_typ_sk == 1001)
                        {
                            lst.Add(new DropDownModel
                            {

                                value = record_txn.Rows[i]["Currency"].ToString(),
                                text = record_txn.Rows[i]["ReserveAccount"].ToString(),
                            });
                        }
                        else
                        {
                            lst.Add(new DropDownModel
                            {

                                value = record_txn.Rows[i]["Currency"].ToString(),
                                text = record_txn.Rows[i]["ReserveAccount"].ToString(),
                            });
                        }

                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReserve()");
                return null;
            }
        }
        public List<DropDownModel> MT202_Dbt_Act()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [BIC], [AC_NO], bank_Code as [Code] FROM [TR_APP].[BANKS_CODE_MAPPING]", con);
                    record_txn = db.getDataTable("SELECT AC_NO FROM [TR_APP].[Vu_BANKS_CODE_MAPPING]", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["AC_NO"].ToString(),
                            text = record_txn.Rows[i]["AC_NO"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "MT202_Dbt_Act()");
                return null;
            }
        }
        public DataTable GetAccount(string BIC)
        {
            try
            {
                using (Connection dx = new Connection())
                {
                    SqlConnection con = dx.getDatabaseConnection();
                    //DataTable dt = dx.getDataTable("SELECT * FROM [TR_APP].[vu_Transaction_Posting] WHERE doc_typ_sk = " + doc_typ_sk + " AND doc_mst_sk = " + mst_sk + " order by create_dt_tme", con);
                    //return dx.getDataTable("SELECT * FROM [TR_APP].[vu_Transaction_Posting] WHERE doc_typ_sk = " + doc_typ_sk + " AND doc_mst_sk = " + mst_sk + " order by create_dt_tme", con);
                    return dx.getDataTable("SELECT * FROM [TR_APP].[Vu_BANKS_CODE_MAPPING] where BIC = '" + BIC + "'", con);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetAccount()");
                return null;
            }
        }
        public List<DropDownModel> GetBankingPrioritylst()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("select br_code,br_desc from branch_mst where (record_status = 0 OR record_status is null) ORDER BY br_desc", con);
                    record_txn = db.getDataTable("select br_code,br_desc from Vu_branch_mst where (record_status = 0 OR record_status is null) ORDER BY br_desc", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["br_code"].ToString(),
                            text = record_txn.Rows[i]["br_desc"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetBankingPrioritylst()");
                return null;
            }
        }
        public List<DropDownModel> GetTxnCodelst(char Dr_Cr, char Msg_Flw, int Doc_typ_SK)
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("select br_code,br_desc from branch_mst where (record_status = 0 OR record_status is null) ORDER BY br_desc", con);

                    record_txn = db.getDataTable("select TXN_CDE,DEBIT_CREDIT from setup.vu_Dr_Cr_TxnCde where DEBIT_CREDIT = '" + Dr_Cr + "' and msg_flw = '"+Msg_Flw+ "' and doc_typ_sk = "+Doc_typ_SK+" ", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["TXN_CDE"].ToString(),
                            text = record_txn.Rows[i]["TXN_CDE"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetTxnCodelst()");
                return null;
            }
        }

        public List<DropDownModel> Getcrncycde()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    record_txn = db.getDataTable("select id,Cur_Code from TR_App.vu_Currency_Code order by is_default desc", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["Cur_Code"].ToString(),
                            text = record_txn.Rows[i]["Cur_Code"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Getcrncycde()");
                return null;
            }
        }

        public List<DropDownModel> Getsndrtorecvlst()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [Prefix]+[code] Send_toRec,[Description] FROM [TR_APP].[TXN_TYPE_CODES] WHERE Code IN ('001','005')", con);
                    record_txn = db.getDataTable("SELECT [Prefix]+[code] Send_toRec,[Description] FROM [TR_APP].[Vu_TXN_TYPE_CODES] WHERE Code IN ('001','005')", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["Send_toRec"].ToString(),
                            text = record_txn.Rows[i]["Description"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "Getsndrtorecvlst()");
                return null;
            }
        }
        public List<doc_typ_cat> GetdoctypcatList()
        {
            try
            {
                List<doc_typ_cat> lst = new List<doc_typ_cat>();
                DataTable record_txn = new DataTable();
                DbContextHelper _ctx = new DbContextHelper();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    record_txn = db.getDataTable("SELECT * FROM setup.doc_typ_cat", con);

                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst = _ctx.ConvertDataTable<doc_typ_cat>(record_txn);
                    return lst;
                }
                else
                {
                    return new List<doc_typ_cat>();
                }

            }
            catch (Exception ex)
            {
                return new List<doc_typ_cat>();
            }
        }
        public List<DropDownModel> GetLogReportType()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                DataTable record_txn = new DataTable();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    //record_txn = db.getDataTable("SELECT [BIC], [AC_NO], bank_Code as [Code] FROM [TR_APP].[BANKS_CODE_MAPPING]", con);
                    record_txn = db.getDataTable("select * from tr_app.vu_Reports_Logs", con);
                }
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--- Select ---",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["Report_Id"].ToString(),
                            text = record_txn.Rows[i]["Report_Names"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetReportType()");
                return null;
            }
        }
        public DataTable GetPoolDocsByDocTypSk(int doc_typ_sk, int mst_sk)
        {
            try
            {
                using (Connection dx = new Connection())
                {
                    SqlConnection con = dx.getDatabaseConnection();
                    return dx.getDataTable("SELECT * FROM setup.vu_pool_docs_partial WHERE doc_typ_sk = " + doc_typ_sk + " AND doc_mst_sk = " + mst_sk + " order by create_dt_tme", con);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetPoolDocsByDocTypSk()");
                return null;
            }
        }
        public DataTable GetTransDocsByDocTypSk(int doc_typ_sk, int mst_sk)
        {
            try
            {
                using (Connection dx = new Connection())
                {
                    SqlConnection con = dx.getDatabaseConnection();
                    //DataTable dt = dx.getDataTable("SELECT * FROM [TR_APP].[vu_Transaction_Posting] WHERE doc_typ_sk = " + doc_typ_sk + " AND doc_mst_sk = " + mst_sk + " order by create_dt_tme", con);
                    //return dx.getDataTable("SELECT * FROM [TR_APP].[vu_Transaction_Posting] WHERE doc_typ_sk = " + doc_typ_sk + " AND doc_mst_sk = " + mst_sk + " order by create_dt_tme", con);
                    return dx.getDataTable("select * from tr_app.vu_txn_hist where DOC_ID = '" + mst_sk + "' and DOC_TYP_SK = " + doc_typ_sk + " order by STS_DTE_TME desc", con);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetTransDocsByDocTypSk()");
                return null;
            }
        }

        public List<DropDownModel> GetPool_Category()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();

                //_ctx = new DbContextHelper();
                //NpgsqlCommand cmd = new NpgsqlCommand();
                //string qry = "select cat_sk,cat_desc from \"setup\".pool_category where (record_status = 0 OR record_status is null) ORDER BY cat_desc";
                string qry = "select cat_sk,cat_desc from \"setup\".Vu_pool_category where (record_status = 0 OR record_status is null) ORDER BY cat_desc";
                con = db.getDatabaseConnection();
                DataTable record_txn = db.getDataTable(qry, con);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["cat_sk"].ToString(),
                            text = record_txn.Rows[i]["cat_desc"].ToString(),
                        });
                    }
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetPool_Category()");
                return null;
            }
        }

        public transactional_status_model CheckTransactionalStatus(transactional_sk_model model)
        {
            try
            {
                transactional_status_model obj = new transactional_status_model();
                DataTable dt = new DataTable();

                using (DbContextHelper dx = new DbContextHelper())
                {
                    if (model.user_sk > 0)
                    {
                        // User check
                        dt = dx.SelectDataTable("SELECT id FROM security.users WHERE (record_status IS NULL OR record_status = 0) AND id = " + model.user_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "User disabled by system";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "User disabled by system";
                            return obj;
                        }
                    }

                    // Remitter check
                    if (model.remtr_sk > 0)
                    {

                        dt = dx.SelectDataTable("SELECT custmr_sk FROM txn.vu_beneficiary_list WHERE (record_status = 0 or record_status is null) AND custmr_sk = " + model.remtr_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Remitter disabled by system";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Remitter disabled by system";
                            return obj;
                        }
                    }

                    // Beneficiary check
                    if (model.beneficiary_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT custmr_sk FROM txn.vu_beneficiary_list WHERE  status_sk = 2 AND custmr_sk = " + model.beneficiary_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Beneficiary disabled by system";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Beneficiary disabled by system";
                            return obj;
                        }
                    }

                    // Customer check
                    if (model.customer_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT * FROM setup.vu_customer_search WHERE status_sk = 2 AND (record_status = 0 OR record_status IS NULL) AND remtr_sk = " + model.customer_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Customer disabled by system";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Customer disabled by system";
                            return obj;
                        }
                    }

                    // Agent check
                    if (model.agent_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT crsp_agnt_sk FROM txn.vu_crsp_agents_list WHERE crsp_agnt_sk = " + model.agent_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Agent disabled by system";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Agent disabled by system";
                            return obj;
                        }
                    }

                    // Instructed currency check
                    if (model.instrctd_currency_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT currency_sk FROM setup.currency_mst WHERE (record_status IS NULL OR record_status = 0) AND currency_sk = " + model.instrctd_currency_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Instructed currency not available";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Instructed currency not available";
                            return obj;
                        }
                    }

                    // Payment currency check
                    if (model.paymnt_currency_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT currency_sk FROM setup.currency_mst WHERE (record_status IS NULL OR record_status = 0) AND currency_sk = " + model.paymnt_currency_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Payment currency not available";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Payment currency not available";
                            return obj;
                        }
                    }

                    // Base Currency check
                    if (model.b_currency_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT currency_sk FROM setup.currency_mst WHERE (record_status IS NULL OR record_status = 0) AND currency_sk = " + model.b_currency_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Base currency not available";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Base currency not available";
                            return obj;
                        }
                    }

                    // Foreign Currency check
                    if (model.f_currency_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT currency_sk FROM setup.currency_mst WHERE (record_status IS NULL OR record_status = 0) AND currency_sk = " + model.f_currency_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Foreign currency not available";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Foreign currency not available";
                            return obj;
                        }
                    }

                    // Bank check
                    if (model.bank_sk > 0)
                    {
                        dt = dx.SelectDataTable("SELECT bank_sk FROM setup.vu_bank_mst WHERE (record_status IS NULL OR record_status = 0) AND bank_sk = " + model.bank_sk);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (int.Parse(dt.Rows[0][0].ToString()) <= 0)
                            {
                                obj.status = false;
                                obj.message = "Bank disabled by system";
                                return obj;
                            }
                        }
                        else
                        {
                            obj.status = false;
                            obj.message = "Bank disabled by system";
                            return obj;
                        }
                    }

                    // Currency loop check
                    if (model.currency_list != null && model.currency_list.Count > 0)
                    {
                        foreach (var item in model.currency_list)
                        {
                            dt = dx.SelectDataTable("SELECT * FROM setup.currency_mst WHERE currency_sk = " + item);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["record_status"].ToString() == "1")
                                {
                                    obj.status = false;
                                    obj.message = dt.Rows[0]["currency_name"].ToString() + " disabled by system";
                                    return obj;
                                }
                            }
                            else
                            {
                                obj.status = false;
                                obj.message = "Foreign currency not available";
                                return obj;
                            }
                        }
                    }
                }

                obj.status = true;
                obj.message = "";
                return obj;
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "CheckTransactionalStatus()");
                transactional_status_model obj = new transactional_status_model();
                obj.status = false;
                obj.message = ex.ToString();
                return null;
            }
        }

        public void Dispose()
        {

        }
    }
}