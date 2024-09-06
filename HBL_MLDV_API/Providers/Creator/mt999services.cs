using HBL_MLDV_API.Areas.Creator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Creator
{
    public class mt999services : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        DbContextHelper dx = new DbContextHelper();

        public CustomObject SaveData(VU_TXN_DTL_INPUT model, int user_sk)
        {
            try
            {
                VU_TXN_MST mst = new VU_TXN_MST();
                mst.TXN_Type = model.MSG_TYP;
                mst.TXN_Ref = model.C20_SNDR_REF;
                model.Created_By = user_sk;
                mst.Doc_Nbr = "";
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    resp = db.SaveChanges("TR_APP", model, true);

                    if (resp.Message == "Record has been saved successfully")
                    {

                    }

                    return resp;
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "SaveData()");
                return null;
            }
        }

        public List<VU_TXN_DTL_INPUT> GetData(int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<VU_TXN_DTL_INPUT> lst = new List<VU_TXN_DTL_INPUT>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.TXN_DTL_INPUT_VU WHERE MSG_TYP='mt999'and created_by = " + user_sk + " order by TXN_DTE_TME desc", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<VU_TXN_DTL_INPUT>(dt);
                        lst.AddRange(aprv);
                    }
                    return lst;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }

        public VU_TXN_DTL_INPUT GetDataById(int TXN_ID)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    VU_TXN_DTL_INPUT aprv = new VU_TXN_DTL_INPUT();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.TXN_DTL_INPUT_VU WHERE MSG_TYP='mt999' and Doc_Sk = " + TXN_ID, con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<VU_TXN_DTL_INPUT>(dt)[0];
                    }
                    return aprv;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataById()");
                return null;
            }
        }
        public void Dispose()
        {

        }
    }
}