using HBL_MLDV_API.Providers;
using System;
using System.Data;

namespace HBL_MLDV_API.Providers
{
    public class HomeServices : IDisposable
    {
        //public DataTable GetDocumentsList(PO_Filters model = null)
        //{
        //    try
        //    {
        //        using (DbContextHelper dx = new DbContextHelper())
        //        {
        //            string query = "";
        //            query = "SELECT * FROM txn.vu_All_Documents_Search";
        //            int count = 0;
        //            if (count == 0)
        //            {
        //                query += " where ";
        //            }
        //            if (model == null)
        //            {
        //                model = new PO_Filters();
        //            }
        //            foreach (var prop in model.GetType().GetProperties())
        //            {
        //                var str = prop.GetValue(model);
        //                if (str != null && !string.IsNullOrWhiteSpace(str.ToString()))
        //                {
        //                    if (count > 0)
        //                        query += " AND ";

        //                    //ttdate
        //                    if (prop.Name == "ttdate")
        //                    {
        //                        query += "cast(created_on as date) >= TO_DATE('" + prop.GetValue(model).ToString() + "','DD-MM-YYYY')";
        //                    }
        //                    if (prop.Name == "ttdateTo")
        //                    {
        //                        query += "cast(created_on as date) <= TO_DATE('" + prop.GetValue(model).ToString() + "','DD-MM-YYYY')";
        //                    }
        //                    //br_sk
        //                    if (prop.Name == "br_sk")
        //                    {
        //                        query += "assign_br_sk = " + prop.GetValue(model).ToString();
        //                    }
        //                    //trnsfr_typ
        //                    if (prop.Name == "trnsfr_typ")
        //                    {
        //                        query += "assign_ttype = " + prop.GetValue(model).ToString();
        //                    }
        //                    //remtr_typ
        //                    if (prop.Name == "remtr_typ")
        //                    {
        //                        query += "remtr_typ = '" + prop.GetValue(model).ToString() + "'";
        //                    }
        //                    //country_sk
        //                    if (prop.Name == "country_sk")
        //                    {
        //                        query += "assign_country_sk = " + prop.GetValue(model).ToString();
        //                    }
        //                    //agent_sk
        //                    if (prop.Name == "agent_sk")
        //                    {
        //                        query += "assign_agent_sk = " + prop.GetValue(model).ToString();
        //                    }
        //                    //trnsfr_crncy
        //                    if (prop.Name == "trnsfr_crncy")
        //                    {
        //                        query += "assign_trcurrency_sk = " + prop.GetValue(model).ToString();
        //                    }
        //                    //trnsfr_amt_min
        //                    if (prop.Name == "trnsfr_amt_min")
        //                    {
        //                        query += "assign_tramt >= " + prop.GetValue(model).ToString();
        //                    }
        //                    //trnsfr_amt_max
        //                    if (prop.Name == "trnsfr_amt_max")
        //                    {
        //                        query += "assign_tramt <= " + prop.GetValue(model).ToString();
        //                    }
        //                    //lc_amt_min
        //                    if (prop.Name == "lc_amt_min")
        //                    {
        //                        query += "assign_lcamt >= " + prop.GetValue(model).ToString();
        //                    }
        //                    //lc_amt_max
        //                    if (prop.Name == "lc_amt_max")
        //                    {
        //                        query += "assign_lcamt <= " + prop.GetValue(model).ToString();
        //                    }
        //                    count++;
        //                }
        //            }
        //            if (count == 0) { query = query.ToLower().Replace("where", ""); }
        //            //if (!string.IsNullOrEmpty(model.ttdate.ToString("yyyy-MM-dd")))
        //            //    query += " assign_ms_doc_dte = '" + model.ttdate.ToString("yyyy-MM-dd") + "'";
        //            //if (model.br_sk > 0)
        //            //    query += "and assign_br_sk = " + model.br_sk;
        //            DataTable dt = dx.SelectDataTable(query);
        //            return dt;
        //        }
        //                   }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        public void Dispose()
        {
        }
    }
}