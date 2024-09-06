using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Areas.Creator.Models;
using HBL_MLDV_API.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HBL_MLDV_API.Areas.Creator.Controllers
{
    public class TRANSLOGServices : IDisposable
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        public CustomObject SaveData(List<Act_logs> model)
        {
            CustomObject resp = new CustomObject();
            using (ADOConnection db = new ADOConnection())
            {
                Guid g = new Guid();
                ListtoDataTable lsttodt = new ListtoDataTable();
                DataTable dt = lsttodt.ToDataTable(model);
                SqlConnection con = db.getDatabaseConnection();

                SqlCommand cmd = new SqlCommand("TR_APP.SP_GenerateActivitylogs", con);
                cmd.CommandType = CommandType.StoredProcedure;

                var RecipeId = new SqlParameter("@LogId", SqlDbType.VarChar) { Value = "1" };
                var RecipeIngredients = new SqlParameter("@Actlogs", SqlDbType.Structured) { Value = dt, TypeName = "[TR_APP].[type_actlogs]" };

                cmd.Parameters.Add(RecipeId);
                cmd.Parameters.Add(RecipeIngredients);
                if (con.State == ConnectionState.Closed)
                { con.Open(); }
                cmd.ExecuteNonQuery();
                DataTable dtres = db.getDataTable("select * from TR_APP.Act_logs", con);
                con.Close();    

                return resp;
            }
        }

        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }
    }

}