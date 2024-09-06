using HBL_MLDV_API.App_Start;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.Providers.Approval
{
    public class PoolViewServices : IDisposable
    {
        UniversalRepository un = new UniversalRepository();
        public void Dispose()
        {

        }

        public DataTable GetPoolView()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                //using ()
                //{
                    using (ADOConnection dx = new ADOConnection())
                    {
                        con = dx.getDatabaseConnection();
                        return dx.getDataTable("SELECT * FROM vu_poolView",con);
                    }
                //}
            }
            catch (Exception ex)
            {
                un.WriteException(ex.ToString(), "GetPoolView()");
                return null;
            }
        }
    }
}