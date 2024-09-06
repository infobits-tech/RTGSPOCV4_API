using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Areas.Creator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Creator
{
    public class AuthorizationServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public CustomObject SaveData(MSG_AUTHRZE mod, int user_sk)
        {
            CustomObject resp = new CustomObject();
            using (ADOConnection db = new ADOConnection())
            {
                SqlConnection con = db.getDatabaseConnection();
                // Save model
                if (mod != null)
                {
                    //foreach (var va in mod)
                    //{
                    resp = db.SaveChanges("dbo", mod);
                    //}
                }
                return resp;
            }
        }
        internal object GetData()
        {
            try
            {
                using (Connection db = new Connection())
                {
                    SqlConnection con = db.getDatabaseConnection();
                    List<Authorization> lst = new List<Authorization>();

                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("select * from dbo.Vu_MSG_AUTHRZE", con);
                    DataTable dt = db.getDataTable("select * from setup.Vu_MSG_AUTHRZE", con);


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
                    MSG_AUTHRZE model = new MSG_AUTHRZE();

                    // Get user aprv dt
                    //DataTable dt = db.getDataTable("select * from  dbo.Vu_MSG_AUTHRZE where DOC_TYP_SK='" + DOC_TYP_SK + "'", con);
                    DataTable dt = db.getDataTable("select * from  setup.Vu_MSG_AUTHRZE where DOC_TYP_SK='" + DOC_TYP_SK + "'", con);
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