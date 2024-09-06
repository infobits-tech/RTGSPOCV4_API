using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers
{
    public class MSG_STSServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        DbContextHelper dx = new DbContextHelper();
        public CustomObject SaveData(MSG_STS model, int user_sk, bool is_ach = false)
        {
            try
            {
                //VU_TXN_MST mst = new VU_TXN_MST();
                //mst.TXN_Type = model.MSG_TYP;
                //mst.TXN_Ref = model.C20_SNDR_REF;
                //mst.Amount = model.AMNT;
                //mst.Doc_Nbr = "";

                CustomObject resp = new CustomObject();
                MSG_STS sts = null;
                DataTable dt = new DataTable();
                string hash = CreateTxnHash(model.DOC_ID, is_ach);
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    int isInvalid = 0;
                    if (model.IS_INVALID == 1)
                    {
                        isInvalid = 1;
                    }
                    if (is_ach)
                    {
                        dt = db.getDataTable("select * from TR_APP.VU_ach_mst where ach_mst_Id = '" + model.DOC_ID + "'", con);
                    }
                    else
                    {
                        dt = db.getDataTable("select * from TR_APP.vu_TXN_DTL_INPUT where DOC_SK = '" + model.DOC_ID + "'", con);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (is_ach)
                        {
                            sts = new MSG_STS()
                            {
                                DOC_ID = Convert.ToInt32(dt.Rows[0]["ach_mst_id"].ToString()),
                                IS_INVALID = isInvalid,
                                DOC_Typ_Sk = 1004,
                                SNDER_REF = dt.Rows[0]["msg_id"].ToString(),
                                STS_DTE_TME = DateTime.Now,
                                STS_SRC = "Web",
                                STS_CDE = model.STS_CDE,
                                HASH = hash,
                                txn_hold_sts = (model.txn_hold_sts != 0) ? 1 : 0,
                                DOC_INSTC_ID = Convert.ToInt64(dt.Rows[0]["DOC_INSTC"].ToString()) > 0 ? Convert.ToInt64(dt.Rows[0]["DOC_INSTC"].ToString()) : 0
                            };
                        }
                        else
                        {
                            sts = new MSG_STS()
                            {
                                DOC_ID = Convert.ToInt32(dt.Rows[0]["DOC_SK"].ToString()),
                                IS_INVALID = isInvalid,
                                DOC_Typ_Sk = Convert.ToInt32(dt.Rows[0]["DOC_TYP_SK"].ToString()),
                                SNDER_REF = dt.Rows[0]["C20_SNDR_REF"].ToString(),
                                STS_DTE_TME = DateTime.Now,
                                STS_SRC = "Web",
                                STS_CDE = model.STS_CDE,
                                HASH = hash,
                                txn_hold_sts = (model.txn_hold_sts != 0) ? 1 : 0,
                                DOC_INSTC_ID = Convert.ToInt64(dt.Rows[0]["DOC_INSTC"].ToString()) > 0 ? Convert.ToInt64(dt.Rows[0]["DOC_INSTC"].ToString()) : 0
                            };
                        }
                    }
                    resp = db.SaveChanges("STP_ENG", sts);

                    if (resp.Message == "Record has been saved successfully")
                    {

                    }

                    con.Close();
                    return resp;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData()");
                return null;
            }
        }
        public CustomObject SaveData_Inward(MSG_STS_INWARD model, int user_sk, bool is_ach = false)
        {
            try
            {
                CustomObject resp = new CustomObject();
                MSG_STS_INWARD sts = null;
                DataTable dt = new DataTable();
                string hash = CreateTxnHash(model.DOC_ID, is_ach,true);

                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    if (is_ach)
                    {
                        dt = db.getDataTable("select * from TR_APP.VU_ach_mst_inward where ach_mst_Id = '" + model.DOC_ID + "'", con);
                    }
                    else
                    {
                        dt = db.getDataTable("select coalesce(SNDers_REF,txn_ref)  as ref,* from STP_ENG.vu_Inbound_TXN_DTL where DOC_SK = '" + model.DOC_ID + "'", con);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        if (is_ach)
                        {
                            sts = new MSG_STS_INWARD()
                            {
                                DOC_ID = Convert.ToInt32(dt.Rows[0]["ach_mst_id"].ToString()),
                                IS_INVALID = 0,
                                DOC_Typ_Sk = 1004,
                                SNDER_REF = dt.Rows[0]["msg_id"].ToString(),
                                STS_DTE_TME = DateTime.Now,
                                STS_SRC = "Web",
                                HASH=hash,
                                STS_CDE = model.STS_CDE
                            };
                        }
                        else
                        {
                            sts = new MSG_STS_INWARD()
                            {
                                DOC_ID = Convert.ToInt32(dt.Rows[0]["DOC_SK"].ToString()),
                                IS_INVALID = 0,
                                DOC_Typ_Sk = Convert.ToInt32(dt.Rows[0]["DOC_TYP_SK"].ToString()),
                                SNDER_REF = dt.Rows[0]["ref"].ToString(),
                                STS_DTE_TME = DateTime.Now,
                                STS_SRC = "Web",
                                HASH = hash,
                                STS_CDE = model.STS_CDE
                            };
                        }
                    }
                    resp = db.SaveChanges("STP_ENG", sts);

                    if (resp.Message == "Record has been saved successfully")
                    {

                    }

                    con.Close();
                    return resp;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData_Inward()");
                return null;
            }
        }
        public CustomObject INSERT_INSTANCE(INSTANCE_TXN model, int user_sk)
        {
            try
            {
                //VU_TXN_MST mst = new VU_TXN_MST();
                //mst.TXN_Type = model.MSG_TYP;
                //mst.TXN_Ref = model.C20_SNDR_REF;
                //mst.Amount = model.AMNT;
                //mst.Doc_Nbr = "";

                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model

                    resp = db.SaveChanges("tr_app", model);

                    if (resp.Message == "Record has been saved successfully")
                    {

                    }

                    return resp;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "INSERT_INSTANCE()");
                return null;
            }
        }
        public List<Vu_FPSuccessfull> CheckFP(int doc_id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<Vu_FPSuccessfull> lst = new List<Vu_FPSuccessfull>();
                    string qry = "select * from TR_APP.[Vu_FPSuccessfull] where Doc_SK = '" + doc_id + "'";

                    // Get user aprv dt
                    DataTable dt = db.getDataTable(qry, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<Vu_FPSuccessfull>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "CheckFP()");
                return null;
            }

        }

        public string CreateTxnHash(int doc_sk, bool is_ach= false, bool is_inbound = false)
        {
            //string table_name = model.GetType().GetProperties().FirstOrDefault().Name;
            string clearValue = "";
            string hash = "";
            string qry = "";
            if (is_ach && !is_inbound)
            {
                qry = "select * from TR_APP.VU_Get_ACH_HASH where ACH_MST_ID = " + doc_sk + "";
            }
            else if(!is_ach && is_inbound)
            {
                qry = "select * from stp_eng.VU_Get_Inbound_HASH where doc_sk = '" + doc_sk + "'";
            }
            else if (is_ach && is_inbound)
            {
                qry = "select * from TR_APP.VU_Get_ACHInbound_HASH where ACH_MST_ID = " + doc_sk + "";
            }
            else
            {
                qry = "select * from tr_app.VU_Get_Outbound_HASH where doc_sk = " + doc_sk + "";
            }
            DataTable dt = new DataTable();
            using (Connection db = new Connection())
            {
                SqlConnection con = db.getDatabaseConnection();
                dt = db.getDataTable(qry, con);
            }
            for (int i = 1; i < dt.Columns.Count; i++)
            {
                if (i == 1)
                {
                    clearValue = dt.Rows[0][i].ToString();
                    continue;
                }
                clearValue += '|' + dt.Rows[0][i].ToString();
            }
            EncryptDecrypt enc = new EncryptDecrypt();
            hash = enc.Encrypt(clearValue);
            return hash;
        }
        public Hold_RSPNS CheckHold(int doc_id)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    Hold_RSPNS lst = new Hold_RSPNS();
                    string qry = "select * from TR_APP.Vu_HOLD_RSPNS where RSPNS_ID = (select max(RSPNS_ID) from TR_APP.Vu_HOLD_RSPNS where DOC_SK='" + doc_id + "')";
                    // Get user aprv dt
                    DataTable dt = db.getDataTable(qry, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        lst.Api_type = dt.Rows[0]["Api_type"].ToString();
                        lst.REPCODE = dt.Rows[0]["REPCODE"].ToString();
                    }
                    return lst;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "CheckHold()");
                return null;
            }
        }

        public void Dispose()
        {
        }
    }
}