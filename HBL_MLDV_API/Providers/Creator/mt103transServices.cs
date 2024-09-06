﻿using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Models;
using HBL_MLDV_API.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.Providers.Creator
{
    public class mt103transServices : IDisposable
    {
        DbContextHelper dx = new DbContextHelper();
        UniversalRepository universalRepository =new  UniversalRepository();
        public List<VU_TXN_DTL_INPUT> GetData(int user_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<VU_TXN_DTL_INPUT> lst = new List<VU_TXN_DTL_INPUT>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.TXN_DTL_INPUT_VU WHERE MSG_TYP='MT103' and created_by = " + user_sk + " order by TXN_DTE_TME desc", con);

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
                universalRepository.WriteException(ex.ToString(),"MT103Getdata");
                return null;
            }
        }
        public CustomObject SaveData(VU_TXN_DTL_INPUT model, int user_sk)
        {
            try
            {
                //VU_TXN_MST mst = new VU_TXN_MST();
                //mst.TXN_Type = model.MSG_TYP;
                //mst.TXN_Ref = model.C20_SNDR_REF;
                //mst.Amount = model.AMNT;
                //mst.Doc_Nbr = "";
                model.Created_By = user_sk;
                CustomObject resp = new CustomObject();
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    // Save model
                    model.Created_By = user_sk;
                    resp = db.SaveChanges("TR_APP", model, true);

                    if (resp.Message == "Record has been saved successfully")
                    {

                    }

                    return resp;
                }
            }
            catch (Exception ex)
            {
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
                    DataTable dt = db.getDataTable("SELECT * FROM TR_APP.TXN_DTL_INPUT_VU WHERE MSG_TYP='MT103' and doc_sk = " + TXN_ID, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        // Convert to user aprv model
                        aprv = dx.ConvertDataTable<VU_TXN_DTL_INPUT>(dt)[0];
                    }
                    //dt = db.getDataTable("SELECT * FROM TR_APP.TITLFTCHNG_RSPNS WHERE IS_ACTV = 1 and DOC_SK = " + TXN_ID, con);
                    //dt = db.getDataTable("SELECT * FROM TR_APP.Vu_TITLFTCHNG_RSPNS WHERE DOC_SK = " + TXN_ID, con);
                    dt = db.getDataTable("select * from TR_APP.Vu_TITLFTCHNG_RSPNS where RSPNS_ID = (select max(RSPNS_ID) from TR_APP.Vu_TITLFTCHNG_RSPNS where DOC_SK='"+ TXN_ID + "')", con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        aprv.Title_Fetching_RSPNS = new TITLFTCHNG_RSPNS();
                        aprv.Title_Fetching_RSPNS = dx.ConvertDataTable<TITLFTCHNG_RSPNS>(dt)[0];
                        //arpv.Title_Fetching_RSPNS.
                    }

                    dt = db.getDataTable("select * from TR_APP.Vu_HOLD_RSPNS where RSPNS_ID = (select max(RSPNS_ID) from TR_APP.Vu_HOLD_RSPNS where DOC_SK='" + TXN_ID + "')", con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        aprv.Hold_RSPNS = new Hold_RSPNS();
                        aprv.Hold_RSPNS = dx.ConvertDataTable<Hold_RSPNS>(dt)[0];
                        //arpv.Title_Fetching_RSPNS.
                    }
                    return aprv;
                }

            }

            catch (Exception ex)
            {
                return null;
            }
        }

        public VU_TXN_DTL_INPUT HoldResponseById(int doc_sk)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    VU_TXN_DTL_INPUT aprv = new VU_TXN_DTL_INPUT();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("select REPCODE,REPMESSAGE,Api_type,REPHOLDCODE from TR_APP.Vu_HOLD_RSPNS where RSPNS_ID = (select max(RSPNS_ID) from TR_APP.Vu_HOLD_RSPNS where DOC_SK='"+doc_sk+"')" , con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        aprv.Hold_RSPNS = new Hold_RSPNS();
                        aprv.Hold_RSPNS = dx.ConvertDataTable<Hold_RSPNS>(dt)[0];
                        //arpv.Title_Fetching_RSPNS.
                    }
                    return aprv;
                }

            }

            catch (Exception ex)
            {
                return null;
            }
        }
        public void Dispose()
        {

        }
    }
}