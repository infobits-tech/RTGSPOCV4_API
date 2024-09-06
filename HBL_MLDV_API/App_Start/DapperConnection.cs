using HBL_MLDV_API.DBAttribute;
using HBL_MLDV_API.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web;
using Dapper;

namespace HBL_MLDV_API.App_Start
{
    public class DapperConnection
    {
        string constr = null;

        public DapperConnection()
        {

            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"\ConnectionInfo.txt"))
            {
                constr = sr.ReadLine();
            }

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        }
        public SqlConnection getDatabaseConnection()
        {
            SqlConnection sqlconn = new SqlConnection();
            sqlconn.ConnectionString = constr;
            sqlconn.Open();
            return sqlconn;
        }
        public int doInsertUpdate(String str_sql, SqlConnection sqlconn)
        {
            int i = 0;
            i = sqlconn.Execute(str_sql);
            return i;
        }
        public DataTable getDataTable(String str_sql, SqlConnection sqlconn)
        {
            object customers = sqlconn.Query(str_sql);
            return new DataTable();//ToDataTable<object>(customers);
        }
        public CustomObject SaveChanges(string schema, object Model)
        {
            if (null == Model)
                return new CustomObject { Data = null, Message = "Data does not exist" };

            //Get Primary Key of Master
            var _Modelkey = Convert.ToInt32(Model.GetType().GetProperties().FirstOrDefault().GetValue(Model));
            //Get Master State
            var _EntityState = Convert.ToString(Model.GetType().GetProperty("state").GetValue(Model));
            //Retreive Detail or Child of Master
            var _Child = Model.GetType()
              .GetProperties()
               .Where(p => IEnumerableType.IsAssignableFrom(p.PropertyType) && !StringType.IsAssignableFrom(p.PropertyType));
            //Check master primary key for Update

            if (Convert.ToInt32(_Modelkey) != 0)
            {
                string _mskey = Model.GetType().GetProperties().FirstOrDefault().Name;
                var query = string.Format("select max({0}) from \"{1}\".{2}", _mskey, schema, Model.GetType().Name.ToLower());
                int _pK = 0;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    try
                    {
                        con.Open();
                        SqlTransaction _globalTrans = con.BeginTransaction();
                        //SqlCommand cmd = con.CreateCommand();
                        SqlCommand cmd = new SqlCommand(query, con, _globalTrans);
                        try
                        {

                            cmd.CommandText = query;
                            //Assign Primary key of Model to _pk variable
                            //var va = cmd.ExecuteNonQuery();
                            SqlDataReader dtReader = cmd.ExecuteReader();
                            con.Close();
                            //SqlConnection connection, SqlTransaction transaction
                            cmd = GenerateUpdateComand(schema, Model);
                            cmd.Connection = con;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            //Iterate Detail one by one
                            foreach (var detail in _Child)
                            {
                                //Get First Collection in Model
                                var _coll = (detail.GetValue(Model) as IEnumerable);
                                if (_coll != null)
                                {
                                    //Reterive Rows in First Collection
                                    var _rows = (from object value in _coll select value).ToList();
                                    //Iterate Row in Collection
                                    foreach (var entry in _rows)
                                    {
                                        try
                                        {
                                            entry.GetType().GetProperty(_mskey).SetValue(entry, _pK + 1);
                                            cmd = GenerateColAndParam(schema, entry);
                                            cmd.Connection = con;
                                            //cmd.ExecuteNonQuery();
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (SqlException ex)
                                        {
                                            throw;
                                        }
                                    }
                                }
                            }
                            //_globalTrans.Commit();
                            return new CustomObject
                            {
                                Data = SelectRecord(schema, Model.GetType().Name.ToLower(), _mskey, _pK + 1),
                                Message = "Record has been Update successfully",
                                status = true
                            };
                        }
                        catch (Exception e)
                        {
                            //_globalTrans.Rollback();
                            cmd.Dispose();
                            _globalTrans.Dispose();
                            return new CustomObject { Data = null, Message = e.ToString(), status = false };
                        }
                        finally
                        {
                            con.Close();
                            cmd.Dispose();
                            _globalTrans.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        return new CustomObject { Data = null, Message = ex.ToString(), status = false };
                    }
                }

            }
            return Insertion(schema, Model);

        }
        public CustomObject Insertion(string schema, object Model)
        {
            //Reterive Details Of Model
            var _Child = Model.GetType()
             .GetProperties()
             .Where(p => IEnumerableType.IsAssignableFrom(p.PropertyType) && !StringType.IsAssignableFrom(p.PropertyType));
            //Get Name Of primary Key of Model
            string _mskey = Model.GetType().GetProperties().FirstOrDefault().Name;
            //Get Primary Key Of  Model
            var query = string.Format("select max({0}) from \"{1}\".{2}", _mskey, schema, Model.GetType().Name.ToLower());
            int _pK = 0;
            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    con.Open();
                    SqlTransaction _globalTrans = con.BeginTransaction();
                    //SqlCommand cmd = con.CreateCommand();
                    SqlCommand cmd = new SqlCommand(query, con, _globalTrans);
                    try
                    {

                        cmd.CommandText = query;
                        //Assign Primary key of Model to _pk variable
                        //var va = cmd.ExecuteNonQuery();
                        SqlDataReader dtReader = cmd.ExecuteReader();
                        DataTable dt = new DataTable();
                        dt.Load(dtReader);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                            {
                                _pK = Convert.ToInt32(dt.Rows[0][0]);
                            }
                            else
                            {
                                _pK = 0;
                            }
                        }
                        else
                        {
                            _pK = 0;
                        }
                        cmd.Connection.Close();
                        //max +1 in primary key for saved
                        Model.GetType().GetProperty(_mskey).SetValue(Model, _pK + 1);
                        cmd = GenerateColAndParam(schema, Model);
                        cmd.Connection = con;
                        cmd.Connection.Open();
                        int i = cmd.ExecuteNonQuery();
                        //Iterate Detail one by one
                        foreach (var detail in _Child)
                        {
                            //Get First Collection in Model
                            var _coll = (detail.GetValue(Model) as IEnumerable);
                            if (_coll != null)
                            {
                                //Reterive Rows in First Collection
                                var _rows = (from object value in _coll select value).ToList();
                                //Iterate Row in Collection
                                foreach (var entry in _rows)
                                {
                                    try
                                    {
                                        entry.GetType().GetProperty(_mskey).SetValue(entry, _pK + 1);
                                        cmd = GenerateColAndParam(schema, entry);
                                        cmd.Connection = con;
                                        //cmd.ExecuteNonQuery();
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (SqlException ex)
                                    {
                                        throw;
                                    }
                                }
                            }
                        }
                        //_globalTrans.Commit();
                        return new CustomObject
                        {
                            Data = SelectRecord(schema, Model.GetType().Name.ToLower(), _mskey, _pK + 1),
                            Message = "Record has been saved successfully",
                            status = true
                        };
                    }
                    catch (Exception e)
                    {
                        //_globalTrans.Rollback();
                        cmd.Dispose();
                        _globalTrans.Dispose();
                        return new CustomObject { Data = null, Message = e.ToString(), status = false };
                    }
                    finally
                    {
                        con.Close();
                        cmd.Dispose();
                        _globalTrans.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    return new CustomObject { Data = null, Message = ex.ToString(), status = false };
                }
            }

        }
        public DataTable SelectRecord(string schema, string tablename, string col, int key)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand();
                DataTable tb = new DataTable("DataTable");
                SqlDataAdapter da;
                var q = string.Format("select *  from \"{0}\".{1} WHERE {2} =@key", schema, tablename.ToLower(), col);
                cmd.Connection = con;
                cmd.CommandText = q;
                cmd.Parameters.AddWithValue("@key", key);
                da = new SqlDataAdapter(cmd);
                da.Fill(tb);
                con.Close();
                //con.ClearPool(con);
                cmd.Dispose();
                return tb;
            }
        }

        public DataTable GetProcedureRecord(string schema, string tablename, string Param, int value)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(schema+'.'+ tablename.ToLower(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Param, value);
                DataTable tb = new DataTable();
                SqlDataAdapter da= new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Dispose();
                con.Close();
                return tb;
            }
        }

        private readonly Type IEnumerableType = typeof(IEnumerable);
        private readonly Type StringType = typeof(string);
        private SqlCommand GenerateUpdateComand(string schema, object Model)
        {
            Type t = Model.GetType();
            Type genericType = typeof(Mapper<>).MakeGenericType(t);
            object genericInstance = Activator.CreateInstance(genericType);
            MethodInfo mi = genericType.GetMethod("GetMappingElements",
            BindingFlags.Instance | BindingFlags.Public);
            var p1 = Expression.Parameter(genericType, "generic");
            var p2 = Expression.Parameter(t, "instance");
            var func = typeof(Func<,,>);
            var genericFunc = func.MakeGenericType(genericType, t, typeof(object));
            var x = Expression.Lambda(genericFunc, Expression.Call(p1, mi, p2), new[] { p1, p2 });
            var invoke = Expression.Invoke(x, Expression.Constant(genericInstance), Expression.Constant(Model));
            var answer = Expression.Lambda<Func<dynamic>>(invoke).Compile()();
            int count = 0;
            string sb = String.Empty;
            SqlCommand cmd = new SqlCommand();
            foreach (var entry in answer)
            {
                SkipAttribute skip = Attribute.GetCustomAttribute(Model.GetType().GetProperty(entry.FieldName), typeof(SkipAttribute)) as SkipAttribute;
                if (skip != null)
                {
                    continue;
                }

                if (count > 0)
                {
                    sb += ",";

                }
                count++;
                sb += entry.FieldName + " = @" + entry.SqlParameter.ParameterName;
                var obj = Model.GetType().GetProperty(entry.FieldName).GetValue(Model);
                //cmd.Parameters.Add(entry.SqlParameter).sqlValue = (obj == null) ? DBNull.Value : obj;
                if (obj == null)
                {
                    cmd.Parameters.AddWithValue("@" + entry.SqlParameter.ParameterName, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@" + entry.SqlParameter.ParameterName, obj);
                }
            }
            cmd.CommandText = string.Format("UPDATE \"{0}\".{1} SET {2} Where {3} = @param", schema,
            Model.GetType().Name.ToLower(), sb,
            Model.GetType().GetProperties().FirstOrDefault().Name);
            //cmd.Parameters.Add("@param", Model.GetType().GetProperties().FirstOrDefault().GetValue(Model));
            cmd.Parameters.AddWithValue("@param", Model.GetType().GetProperties().FirstOrDefault().GetValue(Model));

            sb = String.Empty;
            return cmd;



        }
        private SqlCommand GenerateColAndParam(string schema, object Model)
        {
            Type t = Model.GetType();
            Type genericType = typeof(Mapper<>).MakeGenericType(t);
            object genericInstance = Activator.CreateInstance(genericType);
            MethodInfo mi = genericType.GetMethod("GetMappingElements",
              BindingFlags.Instance | BindingFlags.Public);
            var p1 = Expression.Parameter(genericType, "generic");
            var p2 = Expression.Parameter(t, "instance");
            var func = typeof(Func<,,>);
            var genericFunc = func.MakeGenericType(genericType, t, typeof(object));
            var x = Expression.Lambda(genericFunc, Expression.Call(p1, mi, p2), new[] { p1, p2 });
            var invoke = Expression.Invoke(x, Expression.Constant(genericInstance), Expression.Constant(Model));
            var answer = Expression.Lambda<Func<dynamic>>(invoke).Compile()();
            StringBuilder col = new StringBuilder();
            StringBuilder param = new StringBuilder();
            int count = 0;
            SqlCommand cmd = new SqlCommand();
            foreach (var entry in answer)
            {
                IgnoreAttribute ignore = Attribute.GetCustomAttribute(Model.GetType().GetProperty(entry.FieldName), typeof(IgnoreAttribute)) as IgnoreAttribute;
                if (ignore != null)
                {
                    continue;
                }

                if (count > 0)
                {
                    col.Append(',');
                    param.Append(',');
                }
                count++;
                col.Append(entry.FieldName);
                param.Append("@" + entry.SqlParameter.ParameterName);
                var obj = Model.GetType().GetProperty(entry.FieldName).GetValue(Model);
                //cmd.Parameters.Add(entry.SqlParameter).sqlValue = (obj == null) ? DBNull.Value : obj;

                if (obj == null)
                {
                    cmd.Parameters.AddWithValue("@" + entry.SqlParameter.ParameterName, DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@" + entry.SqlParameter.ParameterName, obj);
                }

            }
            cmd.CommandText = string.Format("INSERT INTO \"{0}\".{1} ({2}) VALUES ({3})", schema, Model.GetType().Name.ToLower(), col, param);


            return cmd;
        }

        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            // Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties((BindingFlags.Public | BindingFlags.Instance));
            foreach (PropertyInfo prop in Props)
            {
                // Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                object values = new object[] 
                {
                    (Props.Length - 1)
                };
                for (int i = 0; (i <= (Props.Length - 1)); i++)
                {
                    // inserting property values to datatable rows
                    values = Props[i].GetValue(item, null);
                }

                dataTable.Rows.Add(values);
            }

            // put a breakpoint here and check datatable
            return dataTable;
        }
    }
}