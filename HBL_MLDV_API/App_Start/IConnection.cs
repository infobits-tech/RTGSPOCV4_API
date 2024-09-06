using HBL_MLDV_API.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HBL_MLDV_API.App_Start
{
    public interface IConnection
    {
        //interface string constr;
        SqlConnection getDatabaseConnection();
        int doInsertUpdate(String str_sql, SqlConnection sqlconn);
        DataTable getDataTable(String str_sql, SqlConnection sqlconn);
        CustomObject SaveChanges(string schema, object Model, bool is_msg_create = false);
        DataTable SelectRecord(string schema, string tablename, string col, int key);
    }
}