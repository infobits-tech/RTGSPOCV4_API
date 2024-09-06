using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using System.Collections;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;
using HBL_MLDV_API.Repository;

using HBL_MLDV_API.DBAttribute;
using System.Data.SqlClient;

namespace HBL_MLDV_API.Providers

{
    enum EntitySate { Changed, UnChanged, Deleted }

    //Return Field With Paramter Of Query
    public class Mapping<T>
    {
        public Mapping(string fieldname, NpgsqlParameter sqlParameter, Action<T, NpgsqlParameter> assigner)
        {
            FieldName = fieldname;
            SqlParameter = sqlParameter;
            SqlParameterAssignment = assigner;
        }
        public string FieldName { get; private set; }
        public NpgsqlParameter SqlParameter { get; private set; }
        public Action<T, Npgsql.NpgsqlParameter> SqlParameterAssignment { get; private set; }
    }
    //Iterate Object For Generate Query
    public class Mapper<T>
    {
        public IEnumerable<Mapping<T>> GetMappingElements(T instance)
        {
            foreach (var reflectionProperty in typeof(T).GetProperties())
            {
                // Input parameters to the created assignment action
                var accessor = Expression.Parameter(typeof(T), "input");
                var sqlParmAccessor = Expression.Parameter(typeof(NpgsqlParameter), "sqlParm");

                // Access the property (compiled later, but use reflection to locate property)
                var property = Expression.Property(accessor, reflectionProperty);

                // Cast the property to ensure it is assignable to SqlProperty.Value 
                // Should contain branching for DBNull.Value when property == null
                var castPropertyToObject = Expression.Convert(property, typeof(object));


                // The sql parameter
                var sqlParm = new NpgsqlParameter(reflectionProperty.Name, null);

                // input parameter for assignment action
                var sqlValueProp = Expression.Property(sqlParmAccessor, "Value");

                // Expression assigning the retrieved property from input object 
                // to the sql parameters 'Value' property
                var dbnull = Expression.Constant(DBNull.Value);
                var coalesce = Expression.Coalesce(castPropertyToObject, dbnull);
                var assign = Expression.Assign(sqlValueProp, coalesce);

                // Compile into action (removes reflection and makes real CLR object)
                var assigner = Expression.Lambda<Action<T, NpgsqlParameter>>(assign, accessor, sqlParmAccessor).Compile();

                yield return
                    new Mapping<T>(reflectionProperty.Name, // Table name
                        sqlParm, // The constructed sql parameter
                        assigner); // The action assigning from the input <T> 

            }
        }
    }
    //Retrun Response OR Exception After Crud
    public class CustomObject
    {
        public bool status { get; set; }
        public string Message { get; set; }
        public DataTable Data { get; set; }

    }
    //Perform Crude Operation 

    public class DbContextHelper : IDisposable
    {
        string connstr = "";
        public DbContextHelper()
        {
            try
            {
                //if (ConfigurationManager.AppSettings["logintyp"].ToString() == "bf")
                //{
                //    connstr = ConfigurationManager.ConnectionStrings["ConnectionString_backoffice"].ToString();
                //}
                //else
                //{
                //    connstr = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                //}

            }
            catch (Exception ex)
            {

            }
        }


        private readonly Type IEnumerableType = typeof(IEnumerable);
        private readonly Type StringType = typeof(string);

        //Return Parameterize Update Command Of Model
        private NpgsqlCommand GenerateUpdateComand(string schema, object Model)
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
            NpgsqlCommand cmd = new NpgsqlCommand();
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
                cmd.Parameters.Add(entry.SqlParameter).NpgsqlValue = (obj == null) ? DBNull.Value : obj;

            }
            cmd.CommandText = string.Format("UPDATE \"{0}\".{1} SET {2} Where {3} = @param", schema,
            Model.GetType().Name.ToLower(), sb,
            Model.GetType().GetProperties().FirstOrDefault().Name);
            //cmd.Parameters.Add("@param", Model.GetType().GetProperties().FirstOrDefault().GetValue(Model));
            cmd.Parameters.AddWithValue("@param", Model.GetType().GetProperties().FirstOrDefault().GetValue(Model));

            sb = String.Empty;
            return cmd;



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

        //Return Parameterize Insert Command Of Model
        private NpgsqlCommand GenerateColAndParam(string schema, object Model)
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
            NpgsqlCommand cmd = new NpgsqlCommand();
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
                cmd.Parameters.Add(entry.SqlParameter).NpgsqlValue = (obj == null) ? DBNull.Value : obj;



            }
            cmd.CommandText = string.Format("INSERT INTO \"{0}\".{1} ({2}) VALUES ({3})", schema, Model.GetType().Name.ToLower(), col, param);


            return cmd;
        }
        //Serialze DataTable To Json String
        //public string SerliazeToJson(DataTable dt)
        //{

        //    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //    Dictionary<string, object> row;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        row = new Dictionary<string, object>();
        //        foreach (DataColumn col in dt.Columns)
        //        {
        //            row.Add(col.ColumnName, dr[col]);
        //        }
        //        rows.Add(row);
        //    }
        //    return serializer.Serialize(rows);

        //}

        //To Impelement
        private int ArcheiveData(object Model)
        {
            int result = 0;

            //var query = "insert into \"APP_CORE\".upd_del_archive(entry_id, entry_group, name)
            //    select * from entrys where entry_id=2";
            return result;
        }


        //Insert Model with nth detail 
        //Note: not working with detail of detail
        //return CustomObject with exception in case of error other wise return data in customobject
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
            using (NpgsqlConnection con = new NpgsqlConnection(connstr))
            {
                try
                {
                    con.Open();
                    NpgsqlTransaction _globalTrans = con.BeginTransaction();
                    NpgsqlCommand cmd = con.CreateCommand();
                    try
                    {

                        cmd.CommandText = query;
                        //Assign Primary key of Model to _pk variable
                        _pK = ExecuteScalar(cmd);
                        //max +1 in primary key for saved
                        Model.GetType().GetProperty(_mskey).SetValue(Model, _pK + 1);
                        cmd = GenerateColAndParam(schema, Model);
                        cmd.Connection = con;
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
                                    catch (NpgsqlException ex)
                                    {
                                        throw;
                                    }
                                }
                            }
                        }
                        _globalTrans.Commit();
                        return new CustomObject
                        {
                            Data = SelectRecord(schema, Model.GetType().Name.ToLower(), _mskey, _pK + 1),
                            Message = "Record has been saved successfully",
                            status = true
                        };
                    }
                    catch (Exception e)
                    {
                        _globalTrans.Rollback();
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

        public CustomObject SaveChanges(string Schema, object Model)
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
                var _key = Model.GetType().GetProperties().FirstOrDefault().Name;
                using (NpgsqlConnection con = new NpgsqlConnection(connstr))
                {
                    try
                    {
                        con.Open();
                        //Manage Concurrency
                        NpgsqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = string.Format("select row_version from \"{0}\".{1} where {2}={3}", Schema, Model.GetType().Name.ToLower(), _key, _Modelkey);
                        int _oldRowVersion = Convert.ToInt32(cmd.ExecuteScalar());
                        if (_oldRowVersion != Convert.ToInt32(Model.GetType().GetProperty("row_version").GetValue(Model)))
                        {
                            cmd.Dispose();
                            con.Close();
                            return new CustomObject { Data = null, Message = "Record Not Saved: Error= Record Already Updated By Another User " };
                        }
                        Model.GetType().GetProperty("row_version").SetValue(Model, _oldRowVersion + 1);
                        //Close Concurrency
                        //Begin Transaction For Update
                        NpgsqlTransaction _globalTrans = con.BeginTransaction();
                        try
                        {
                            if (_EntityState == EntitySate.Changed.ToString())
                            {
                                cmd = GenerateUpdateComand(Schema, Model);
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();

                            }
                            else if (_EntityState == EntitySate.Deleted.ToString())
                            {
                                cmd = Delete(Schema, Model);
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                            }
                            foreach (var detail in _Child)
                            {
                                var _coll = (detail.GetValue(Model) as IEnumerable);
                                if (_coll != null)
                                {
                                    var _rows = (from object value in _coll select value).ToList();
                                    foreach (var entity in _rows)
                                    {
                                        var _EntityKey = Convert.ToInt32(entity.GetType().GetProperties().FirstOrDefault().GetValue(entity));
                                        _EntityState = Convert.ToString(entity.GetType().GetProperty("state").GetValue(entity));
                                        if (_EntityKey != 0 && _EntityState == EntitySate.Changed.ToString())
                                        {
                                            cmd = GenerateUpdateComand(Schema, entity);
                                            cmd.Connection = con;
                                            cmd.ExecuteNonQuery();

                                        }
                                        else if (_EntityKey != 0 && _EntityState == EntitySate.Deleted.ToString())
                                        {
                                            cmd = Delete(Schema, entity);
                                            cmd.Connection = con;
                                            cmd.ExecuteNonQuery();

                                        }
                                        else if (_EntityState == "")
                                        {
                                            entity.GetType().GetProperty(_key).SetValue(entity, _Modelkey);
                                            cmd = GenerateColAndParam(Schema, entity);
                                            cmd.Connection = con;
                                            cmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                            }
                            _globalTrans.Commit();
                            return new CustomObject { Data = SelectRecord(Schema, Model.GetType().Name.ToLower(), _key, _Modelkey), Message = "Record has been updated successfully", status = true };
                        }
                        catch (Exception ex)
                        {
                            _globalTrans.Rollback();
                            cmd.Dispose();
                            _globalTrans.Dispose();
                            return new CustomObject { Data = null, Message = ex.ToString(), status = false };
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
            return Insertion(Schema, Model);

        }


        private NpgsqlCommand Delete(string schema, object Model)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.CommandText = String.Format("DELETE FROM  \"{0}\".{1}  WHERE {2}=@param", schema, Model.GetType().Name.ToLower(), Model.GetType().GetProperties().FirstOrDefault().Name);
            cmd.Parameters.AddWithValue("@param", Model.GetType().GetProperties().FirstOrDefault().GetValue(Model));
            return cmd;
        }

        public DataTable SelectRecord(string schema, string tablename, string col, int key)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connstr))
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                DataTable tb = new DataTable("DataTable");
                NpgsqlDataAdapter da;
                var q = string.Format("select *  from \"{0}\".{1} WHERE {2} =@key", schema, tablename.ToLower(), col);
                cmd.Connection = con;
                cmd.CommandText = q;
                cmd.Parameters.AddWithValue("@key", key);
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(tb);
                con.Close();
                //con.ClearPool(con);
                cmd.Dispose();
                return tb;
            }
        }
        //public string SelectRecord(NpgsqlCommand cmd)
        //{
        //    using (NpgsqlConnection con = new NpgsqlConnection(connstr))
        //    {
        //        con.Open();
        //        cmd.Connection = con;
        //        DataTable dt = new DataTable();
        //        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        con.Close();
        //        //con.ClearPool();
        //        cmd.Dispose();
        //        return SerliazeToJson(dt);


        //    }
        //}
        public DataTable SelectDataTable(NpgsqlCommand cmd)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connstr))
                {
                    con.Open();
                    cmd.Connection = con;
                    DataTable dt = new DataTable();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    // con.ClearPool();
                    cmd.Dispose();
                    return dt;


                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public DataTable SelectDataTable(string query)
        {
            try
            {
                using (NpgsqlConnection con = new NpgsqlConnection(connstr))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = query;
                    DataTable dt = new DataTable();
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    // con.ClearPool();
                    cmd.Dispose();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public async Task<DataTable> SelectRecord(string query)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(connstr))
            {



                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                //  con.ClearPool();
                cmd.Dispose();
                return dt;

            }
        }
        public int SelectSingleValue(NpgsqlCommand cmd)
        {
            int i = 0;
            using (NpgsqlConnection con = new NpgsqlConnection(connstr))
            {
                con.Open();
                cmd.Connection = con;

                i = Convert.ToInt32(cmd.ExecuteScalar());
                // con.ClearPool();
                cmd.Dispose();
                con.Close();
                return i;

            }
        }
        public string SelectSingleStringValue(NpgsqlCommand cmd)
        {
            string i = "";
            using (NpgsqlConnection con = new NpgsqlConnection(connstr))
            {
                con.Open();
                cmd.Connection = con;

                i = Convert.ToString(cmd.ExecuteScalar());
                // con.ClearPool();
                cmd.Dispose();
                con.Close();
                return i;

            }
        }
        //Function added by SYED MUHAMMAD FAIZ ALI
        public async Task<string> SelectSingleStringValue(string query)
        {
            string i = "";
            using (NpgsqlConnection con = new NpgsqlConnection(connstr))
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                cmd.Connection = con;
                i = Convert.ToString(cmd.ExecuteScalar());
                // con.ClearPool();
                cmd.Dispose();
                con.Close();
                return i;

            }
        }
        public int doinsertupdatedelete(NpgsqlCommand cmd)
        {
            try
            {
                int i = 0;

                using (NpgsqlConnection con = new NpgsqlConnection(connstr))
                {
                    con.Open();


                    cmd.Connection = con;
                    i = cmd.ExecuteNonQuery();
                    // con.ClearPool();
                    cmd.Dispose();
                    con.Close();
                    return i;

                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public async Task<int> doinsertupdatedelete(string query)
        {
            try
            {
                int i = 0;

                using (NpgsqlConnection con = new NpgsqlConnection(connstr))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Connection = con;
                    i = await cmd.ExecuteNonQueryAsync();
                    // con.ClearPool();
                    cmd.Dispose();
                    con.Close();
                    return i;

                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public int doinsertupdatedeleteNoAsync(string query)
        {
            try
            {
                int i = 0;

                using (NpgsqlConnection con = new NpgsqlConnection(connstr))
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                    cmd.Connection = con;
                    i = cmd.ExecuteNonQuery();
                    // con.ClearPool();
                    cmd.Dispose();
                    con.Close();
                    return i;

                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public int ExecuteScalar(NpgsqlCommand cmd)
        {
            try
            {
                using (NpgsqlConnection conn = new NpgsqlConnection(connstr))
                {
                    int result = 0;
                    conn.Open();
                    cmd.Connection = conn;
                    var val = cmd.ExecuteScalar();
                    conn.Close();
                    if (val == null)
                    { }
                    else
                    {
                        if (val.ToString() == "")
                        {
                            result = 0;
                        }
                        else
                        {
                            result = Convert.ToInt16(val);
                        }
                    }



                    return result;
                }


            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public void Dispose()
        {

        }

        internal SqlConnection getDatabaseConnection()
        {
            throw new NotImplementedException();
        }
    }
}




