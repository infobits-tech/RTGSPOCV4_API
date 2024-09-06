using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Models;
using HBL_MLDV_API.Models.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Repository
{
    public class DropDownService : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        DbContextHelper _ctx;
        public List<DropDownModel> Getdata()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select Country_sk,country_desc from \"setup\".country_mst where record_status = 0 OR record_status is null ORDER BY country_desc";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["Country_sk"].ToString(),
                            text = record_txn.Rows[i]["country_desc"].ToString(),
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

        public List<DropDownModel> GetActiveProducts()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select prod_sk,prod_nme from \"setup\".prod_mst where (record_status = 0 OR record_status is null) ORDER BY prod_nme";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["prod_sk"].ToString(),
                            text = record_txn.Rows[i]["prod_nme"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveProducts()");
                return null;
            }
        }
        

        public List<DropDownModel> GetActiveStates(int countryId)
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select state_sk,state_desc from \"setup\".state_mst where (record_status = 0 OR record_status is null) AND country_sk = " + countryId + " ORDER BY state_desc";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["state_sk"].ToString(),
                            text = record_txn.Rows[i]["state_desc"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveStates()");
                return null;
            }
        }

        public List<DropDownModel> GetActiveCities(int countryId)
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select city_sk,city_desc from \"setup\".city_mst where (record_status = 0 OR record_status is null) AND country_sk = " + countryId + " ORDER BY city_desc";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["city_sk"].ToString(),
                            text = record_txn.Rows[i]["city_desc"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveCities()");
                return null;
            }
        }

        public List<DropDownModel> GetActiveDistricts()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select district_sk,district_desc from \"setup\".district_mst where (record_status = 0 OR record_status is null) ORDER BY district_desc";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    //lst.Add(new DropDownModel
                    //{
                    //    value = "0",
                    //    text = "--Select District--",
                    //});
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["district_sk"].ToString(),
                            text = record_txn.Rows[i]["district_desc"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveDistricts()");
                return null;
            }
        }

        public List<DropDownModel> GetActiveRemitterContacts(int remitterId)
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select remtr_cntct_sk,remtr_cntct_nme from \"setup\".remtr_corp_cntct where (record_status = 0 OR record_status is null) AND remtr_sk = " + remitterId + " ORDER BY remtr_cntct_nme";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["remtr_cntct_sk"].ToString(),
                            text = record_txn.Rows[i]["remtr_cntct_nme"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveRemitterContacts()");
                return null;
            }
        }

        public List<DropDownModel> GetActiveCrspTypes()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select crsp_typ_sk,crsp_typ_desc from \"setup\".crsp_typ where (record_status = 0 OR record_status is null) ORDER BY crsp_typ_desc";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["crsp_typ_sk"].ToString(),
                            text = record_txn.Rows[i]["crsp_typ_desc"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveCrspTypes()");
                return null;
            }
        }

        public DataTable GetActiveGetDocuments(string type)
        {
            try
            {
                //List<suprtng_doc_typs> lst = new List<suprtng_doc_typs>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select * from \"setup\".suprtng_doc_typs where (record_status = 0 OR record_status is null)";
                if (!string.IsNullOrEmpty(type) && type != "all")
                {
                    cmd.CommandText += "and remtr_typ='" + type + "'";
                }
                else
                {
                    cmd.CommandText += "and remtr_typ in ('Individual','Corporate')";
                }
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    return record_txn;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetActiveGetDocuments()");
                return null;
            }
        }

        public List<DropDownModel> GetActiveProfession()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select prof_sk ,prof_desc from \"setup\".vu_profession_mst where (record_status = 0 OR record_status is null)";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--Select--",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["prof_sk"].ToString(),
                            text = record_txn.Rows[i]["prof_desc"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveProfession()");
                return null;
            }
        }
        public List<DropDownModel> GetActiveIndustrylist()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select * from \"setup\".vu_industry_mst where (record_status = 0 OR record_status is null)";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst.Add(new DropDownModel
                    {
                        value = "0",
                        text = "--Select--",
                    });
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["indstry_sk"].ToString(),
                            text = record_txn.Rows[i]["indstry_desc"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveIndustrylist()");
                return null;
            }
        }
        public List<DropDownModel> GetActiveSourceofIncome()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select INCOM_SOURCE_SK,INCOM_SOURCE_DESC from \"setup\".INCOM_SOURCE where (record_status = 0 OR record_status is null) ORDER BY INCOM_SOURCE_DESC";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["INCOM_SOURCE_SK"].ToString(),
                            text = record_txn.Rows[i]["INCOM_SOURCE_DESC"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActiveSourceofIncome()");
                return null;
            }
        }

        public List<DropDownModel> GetActivePurposeofTransfer()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select TRNSFR_PURPS_SK,TRNSFR_PURPS_DESC from \"setup\".TRNSFR_PURPS where (record_status = 0 OR record_status is null) ORDER BY TRNSFR_PURPS_DESC";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["TRNSFR_PURPS_SK"].ToString(),
                            text = record_txn.Rows[i]["TRNSFR_PURPS_DESC"].ToString(),
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
                un.WriteException(ex.ToString(), "GetActivePurposeofTransfer()");
                return null;
            }
        }

        public List<DropDownModel> GetCustomerList()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select remtr_sk,CONCAT(member_id,' | ', frst_nme,' ',mid_nme,' ',last_nme) as name from \"setup\".vu_customer_search";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["remtr_sk"].ToString(),
                            text = record_txn.Rows[i]["name"].ToString(),
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
                un.WriteException(ex.ToString(), "GetCustomerList()");
                return null;
            }
        }

        public List<vu_customer_search> AdvanceCustomerSearch()
        {
            try
            {
                List<vu_customer_search> lst = new List<vu_customer_search>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select * from setup.vu_customer_search where status_sk <> 7 and (record_status = 0 or record_status is null)";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst = _ctx.ConvertDataTable<vu_customer_search>(record_txn);
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "AdvanceCustomerSearch()");
                return null;
            }
        }

        public List<vu_customer_search> AdvanceCustomerSearch_All()
        {
            try
            {
                List<vu_customer_search> lst = new List<vu_customer_search>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select * from setup.vu_customer_search";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    lst = _ctx.ConvertDataTable<vu_customer_search>(record_txn);
                    return lst;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "AdvanceCustomerSearch_All()");
                return null;
            }
        }

        public int GetDraftNo()
        {
            try
            {
                using (DbContextHelper dx  = new DbContextHelper())
                {
                    //DataTable dt = dx.SelectDataTable("select COALESCE(max(draft_no + 1),1) from  \"txn\".doc_mst");
                    DataTable dt = dx.SelectDataTable("SELECT setval('txn.seq_doctxn_draft_nbr',nextval('txn.seq_doctxn_draft_nbr'));");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return int.Parse(dt.Rows[0][0].ToString());
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public int GetDraftNoGeneral()
        {
            try
            {
                using (DbContextHelper dx  = new DbContextHelper())
                {
                    DataTable dt = dx.SelectDataTable("select COALESCE(max(draft_no + 1),1) from  \"txn\".doc_mst_gnrl");

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return int.Parse(dt.Rows[0][0].ToString());
                    }

                    return 0;
                }
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public List<DropDownModel> GetUsers()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select user_sk,user_full_name from security.vu_users where (record_status = 0 OR record_status is null) ORDER BY user_full_name";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    for (int i = 0; i < record_txn.Rows.Count; i++)
                    {
                        lst.Add(new DropDownModel
                        {
                            value = record_txn.Rows[i]["user_sk"].ToString(),
                            text = record_txn.Rows[i]["user_full_name"].ToString(),
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

        public DataTable GetMasterStatusList(string docTypeSk)
        {
            try
            {
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select status_sk,status_desc,doc_typ_sk from setup.vu_doc_status where doc_typ_sk IN (" + docTypeSk + ") AND is_active = true ORDER BY doc_typ_sk, status_sk";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    return record_txn;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetMasterStatusList()");
                return null;
            }
        }

        public DataTable GetTxnStatusList(string docTypeSk)
        {
            try
            {
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select status_sk,status_desc,doc_typ_sk from setup.vu_doc_status where doc_typ_sk IN (" + docTypeSk + ") AND is_active = true ORDER BY doc_typ_sk, status_sk";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
                if (record_txn != null && record_txn.Rows.Count > 0)
                {
                    return record_txn;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetTxnStatusList()");
                return null;
            }
        }
        
        public DataTable GetPoolDocsByDocTypSk(int doc_typ_sk, int mst_sk)
        {
            try
            {
                using (DbContextHelper dx  = new DbContextHelper())
                {
                    return dx.SelectDataTable("SELECT * FROM setup.vu_pool_docs_partial WHERE doc_typ_sk = " + doc_typ_sk + " AND doc_mst_sk = " + mst_sk + " order by create_dt_tme");
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetPoolDocsByDocTypSk()");
                return null;
            }
        }

        public List<DropDownModel> GetPoolCategory()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select cat_sk,cat_desc from setup.vu_pool_category_rule";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
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
                un.WriteException(ex.ToString(), "GetPoolCategory()");
                return null;
            }
        }

        public List<DropDownModel> GetUserRoles()
        {
            try
            {
                List<DropDownModel> lst = new List<DropDownModel>();
                _ctx  = new DbContextHelper();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = "select role_sk,role_desc from security.vu_role_mst";
                DataTable record_txn = _ctx.SelectDataTable(cmd);
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
        public void Dispose()
        {

        }
    }
}