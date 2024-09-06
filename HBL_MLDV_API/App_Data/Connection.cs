using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Globalization;
using HBL_MLDV_API.Providers;
using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.App_Start;
using System.Collections.Generic;

/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection : IDisposable, IConnection
{
    public bool IsDapper = false;
    private readonly DapperConnection _dappercon;
    private readonly ADOConnection _sqlcon;
    public Connection()
    {
        if (IsDapper)
        {
            _dappercon = new DapperConnection();
        }
        else
        {
            _sqlcon = new ADOConnection();
        }
    }
    public SqlConnection getDatabaseConnection()
    {
        if (IsDapper)
        {
            return _dappercon.getDatabaseConnection();
        }
        else
        {
            return _sqlcon.getDatabaseConnection();
        }

    }
    public int doInsertUpdate(string str_sql, SqlConnection sqlconn)
    {
        if (IsDapper)
        {
            return _dappercon.doInsertUpdate(str_sql, sqlconn);
        }
        else
        {
            return _sqlcon.doInsertUpdate(str_sql, sqlconn);
        }

    }

    public DataTable getDataTable(string str_sql, SqlConnection sqlconn)
    {
        if (IsDapper)
        {
            return _dappercon.getDataTable(str_sql, sqlconn);
        }
        else
        {
            return _sqlcon.getDataTable(str_sql, sqlconn);
        }
    }

    public CustomObject SaveChanges(string schema, object Model, bool is_msg_create = false)
    {
        if (IsDapper)
        {
            return _dappercon.SaveChanges(schema, Model);
        }
        else
        {
            return _sqlcon.SaveChanges(schema, Model, is_msg_create);
        }
    }

    public DataTable SelectRecord(string schema, string tablename, string col, int key)
    {
        if (IsDapper)
        {
            return _dappercon.SelectRecord(schema, tablename, col, key);
        }
        else
        {
            return _sqlcon.SelectRecord(schema, tablename, col, key);
        }
    }

    public DataTable GetProcedureRecord(string schema, string tablename, string param, int value)
    {
        if (IsDapper)
        {
            return _dappercon.GetProcedureRecord(schema, tablename, param, value);
        }
        else
        {
            return _sqlcon.GetProcedureRecord(schema, tablename, param, value);
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
    public void Dispose()
    {
        //throw new NotImplementedException();
    }
}
