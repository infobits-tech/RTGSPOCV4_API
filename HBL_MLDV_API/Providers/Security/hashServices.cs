using HBL_MLDV_API.Providers;
using HBL_MLDV_API.Models.Security;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace HBL_MLDV_API.Providers.Security
{
    public class hashServices : IDisposable
    {
        DbContextHelper _ctx  = new DbContextHelper();
        private readonly Type IEnumerableType = typeof(IEnumerable);
        private readonly Type StringType = typeof(string);
        public void GenerateLog(int mst_Id, int doc_typ_sk, int UserId, DateTime hash_dt_tme)
        {
            CustomObject cm = new CustomObject();
            List<hash_dtls> lst = new List<hash_dtls>();
            hash_dtls db = new hash_dtls();
            db.state = "";
            db.src = "Backoffice";
            db.doc_typ_sk = doc_typ_sk;
            db.user_id = UserId;
            db.hash_dt_tme = hash_dt_tme;
            db.remote_ip = "";
            db.remote_pc_mac = System.Net.Dns.GetHostEntry(HttpContext.Current.Request.UserHostAddress).HostName;

            //db.remote_pc_mac = Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]).HostName;
            string Header = "";
            db.hash_data = "";
            //cm = _ctx.Insertion("security", db);
            DataTable dt = _ctx.SelectDataTable("SELECT * FROM \"txn\".tbl_logging Where doc_typ_sk = " + doc_typ_sk);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable data = _ctx.SelectDataTable(dt.Rows[0]["script_log"].ToString() + " "+ mst_Id);
                if (data != null && data.Rows.Count > 0)
                {
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        db.hash_data = "";
                        for (int j = 0; j < data.Columns.Count; j++)
                        {
                            Header += data.Columns[j] + "|";
                            db.hash_data += data.Rows[i][j].ToString() + "|";
                        }
                        Header = Header.TrimEnd('|');
                        db.hash_data = db.hash_data.TrimEnd('|');
                        lst.Add(db);
                    }
                    
                }

            }
            //Type myType = model.GetType();
            //IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            //var _Child = model.GetType().GetProperties().Where(p => IEnumerableType.IsAssignableFrom(p.PropertyType) && !StringType.IsAssignableFrom(p.PropertyType));
            //foreach (PropertyInfo prop in props)
            //{
            //    if (prop.GetType().GetProperties().Where(p => IEnumerableType.IsAssignableFrom(p.PropertyType) && !StringType.IsAssignableFrom(p.PropertyType)) != null)
            //    {
            //        Header += prop.Name + "|";
            //        object propValue = prop.GetValue(model, null);
            //        db.hash_data += propValue + "|";
            //    }


            //}
            //foreach (var detail in _Child)
            //{
            //    var _coll = (detail.GetValue(model) as IEnumerable);
            //    if (_coll != null)
            //    {
            //        //Reterive Rows in First Collection
            //        var _rows = (from object value in _coll select value).ToList();
            //        //Iterate Row in Collection
            //        foreach (var entry in _rows)
            //        {
            //            IList<PropertyInfo> propsChild = new List<PropertyInfo>(entry.GetType().GetProperties());
            //            foreach (PropertyInfo prop in propsChild)
            //            {
            //                Header += prop.Name + "|";
            //                object propValue = prop.GetValue(entry, null);
            //                db.hash_data += propValue + "|";

            //            }
            //        }
            //    }
            //}
            
            foreach (var va in lst)
            {
                cm = _ctx.Insertion("security", va);
            }
            //cm = _ctx.Insertion("security", db);
        }

        private string GetValue(PropertyInfo prop, object model)
        {

            object propValue = prop.GetValue(model, null);
            return propValue.ToString();

        }
        public void Dispose()
        {

        }
    }
}