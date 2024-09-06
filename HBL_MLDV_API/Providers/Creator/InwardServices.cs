using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Creator
{
    public class InwardServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        DbContextHelper dx = new DbContextHelper();
        public List<VU_INBOUND_TXN_DTL> GetData(int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<VU_INBOUND_TXN_DTL> lst = new List<VU_INBOUND_TXN_DTL>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("select * from STP_ENG.Inbound_TXN_DTL_Vu where doc_typ_sk=1001 order by TXN_DTE_TME desc", con);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        var aprv = dx.ConvertDataTable<VU_INBOUND_TXN_DTL>(dt);
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

        public CustomObject SaveData(VU_INBOUND_TXN_DTL model, int user_sk)
        {
            try
            {
                //VU_TXN_MST mst = new VU_TXN_MST();
                //mst.TXN_Type = model.MSG_TYP;
                //mst.TXN_Ref = model.C20_SNDR_REF;
                //mst.Amount = model.AMNT;
                //mst.Doc_Nbr = "";
                
                //model.Created_By = user_sk;

                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    //model.Created_By = user_sk;

                    resp = db.SaveChanges("STP_ENG", model, true);

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

        public VU_INBOUND_TXN_DTL GetDataById(int TXN_ID)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    VU_INBOUND_TXN_DTL aprv = new VU_INBOUND_TXN_DTL();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM STP_ENG.Inbound_TXN_DTL_VU WHERE DOC_SK = " + TXN_ID, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<VU_INBOUND_TXN_DTL>(dt)[0];
                    }
                    //dt = db.getDataTable("SELECT * FROM TR_APP.TITLFTCHNG_RSPNS WHERE IS_ACTV = 1 and DOC_SK = " + TXN_ID, con);
                    dt = db.getDataTable("SELECT * FROM TR_APP.Vu_TITLFTCHNG_RSPNS WHERE DOC_SK = " + TXN_ID, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        aprv.Title_Fetching_RSPNS = new TITLFTCHNG_RSPNS();
                        aprv.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt)[0];
                        //arpv.Title_Fetching_RSPNS.
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
            //throw new NotImplementedException();
        }
    }
}
