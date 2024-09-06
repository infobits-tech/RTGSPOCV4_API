using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Areas.Creator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Creator 
{
    public class CB_CutOffTimesServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        public void Dispose()
        {
            // throw new NotImplementedException();
        }

        public CustomObject SaveData(CB_CUTOFF_TIMES mod, int user_sk)
        {
            CustomObject resp = new CustomObject(); 
            using (ADOConnection db = new ADOConnection())
            {
                SqlConnection con = db.getDatabaseConnection();
                // Save model
                if (mod != null)
                {
                    foreach (var va in mod.lstCB_CUTOFF_TIMES)
                    {
                        resp = db.SaveChanges("setup", va);
                    }
                }

                //if (resp.Message == "Record has been saved successfully")
                //{

                //}
                return resp;
            }
        }
        //public CustomObject Update(CB_CUTOFF_TIMES mod, int user_sk)
        //{
        //    using (ADOConnection db = new ADOConnection())
        //    {
        //        SqlConnection con = db.getDatabaseConnection();
        //        // Save model
        //        var resp = db.SaveChanges("TR_APP", mod);

        //        if (resp.Message == "Record has been updated successfully")
        //        {

        //        }
        //        return resp;
        //    }
        //}

        internal object GetData()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<CB_CUTOFF_TIMES> lst = new List<CB_CUTOFF_TIMES>();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("select * from setup.vu_CB_Cutoff_Times", con);

                 
                    return dt;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }

        internal object GetDataById(string DOC_TYP_SK)
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    CB_CUTOFF_TIMES model = new CB_CUTOFF_TIMES();

                    // Get user aprv dt
                    DataTable dt = db.getDataTable("select * from setup.vu_getcuttofftime_byId where DOC_TYP_SK='" + DOC_TYP_SK + "'", con);
                    //if(dt!=null && dt.Rows.Count > 0)
                    //{
                    //    List<CB_CUTOFF_TIMES> lst = db.ConvertDataTable<CB_CUTOFF_TIMES>(dt);
                    //    if (lst != null && lst.Count > 0)
                    //    {
                    //        model = lst.FirstOrDefault();
                    //        model.lstCB_CUTOFF_TIMES = lst;
                    //    }
                    //}

                    return dt;
                }

            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDataById()");
                return null;
            }
        }
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {


                    if (pro.Name.ToLower() == column.ColumnName.ToLower())
                    {
                        if (dr[column.ColumnName].ToString() != "")
                            pro.SetValue(obj, dr[column.ColumnName], null);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return obj;
        }
    }
}