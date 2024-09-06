using HBL_MLDV_API.App_Start;
using HBL_MLDV_API.Areas.Setup.Models;
using HBL_MLDV_API.Models.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Setup
{

    public class PoolServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        DbContextHelper db = new DbContextHelper();
        public CustomObject Save(Pools model)
        {
            using (ADOConnection dx = new ADOConnection())
            {
                return dx.SaveChanges("setup", model);

            }

        }
        public List<Pools> GetData()
        {
            try
            {
                using (ADOConnection dx = new ADOConnection())
                {
                    SqlConnection con = dx.getDatabaseConnection();
                    //List<> lst = new List<>();
                    List<Pools> lst = new List<Pools>();

                    //DataTable dt = dx.getDataTable("SELECT * FROM \"setup\".pools", con);
                    DataTable dt = dx.getDataTable("SELECT * FROM \"setup\".Vu_pools", con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lst = db.ConvertDataTable<Pools>(dt);
                    }

                    return lst;
                    //DataTable dt = dx.getDataTable("SELECT * FROM \"setup\".pools");
                    //return dx.ConvertDataTable<Pools>(dt);
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetData()");
                return null;
            }
        }
        public Pools GetDatabyid(int id)
        {
            try
            {
                using (ADOConnection dx = new ADOConnection())
                {
                    SqlConnection con = dx.getDatabaseConnection();
                    Pools lst = new Pools();

                    //DataTable dt = dx.getDataTable("SELECT * FROM \"setup\".Pools where pool_sk = " + id, con);
                    DataTable dt = dx.getDataTable("SELECT * FROM \"setup\".Vu_Pools where pool_sk = " + id, con);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lst = db.ConvertDataTable<Pools>(dt)[0];
                    }

                    return lst;
                    //DataTable dt = dx.getDataTable("SELECT * FROM \"setup\".Pools where pool_sk = " + id);
                    //return dx.ConvertDataTable<Pools>(dt)[0];
                }
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetDatabyid()");
                return null;
            }
        }
        public void Dispose() { }
    }
}