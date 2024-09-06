using System;
using System.Linq;
using System.Data;
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
using HBL_MLDV_API.Repository;

namespace HBL_MLDV_API.App_Start
{
    public class ADOConnection : IDisposable
    {
        string constr = null;
        UniversalRepository un = new UniversalRepository();
        public ADOConnection()
        {

            // HBL 
            //constr = "Data Source=10.200.71.100;Initial Catalog=RTGS;User ID=sa;Password=hbl@1234";

            //FOR DEVELOPMENT
            //constr = "Data Source=Ali-Dell;Initial Catalog=Email_Intimation; Integrated Security = true";


            //TO READ FROM FILE
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
            SqlCommand insert_command = new SqlCommand(str_sql, sqlconn);
            i = insert_command.ExecuteNonQuery();
            return i;
        }
        public SqlDataReader getData(String str_sql, SqlConnection sqlconn)
        {
            SqlCommand selectcommand = sqlconn.CreateCommand();
            selectcommand.CommandText = str_sql;
            SqlDataReader selectreader = selectcommand.ExecuteReader();
            return selectreader;
        }
        public DataTable getDataTable(String str_sql, SqlConnection sqlconn)
        {
            SqlCommand selectedcommand = new SqlCommand();
            selectedcommand = sqlconn.CreateCommand();
            selectedcommand.CommandText = str_sql;
            selectedcommand.CommandType = CommandType.Text;
            DataTable dt = new DataTable("DataTable");
            SqlDataAdapter da = new SqlDataAdapter(selectedcommand);
            da.Fill(dt);
            if (dt != null)
            {
                return dt;
            }
            else
            {
                return null;
            }
        }
        public void closeReader(SqlDataReader sqlreader)
        {

            sqlreader.Close();
            sqlreader.Dispose();
        }
        public void closeConn(SqlConnection sqlconn)
        {
            sqlconn.Close();
            sqlconn.Dispose();
        }
        public int GetMasterID(String str_sql, SqlConnection sqlconn)
        {
            int i = 0;
            SqlCommand get_command = new SqlCommand(str_sql, sqlconn);
            i = Convert.ToInt32(get_command.ExecuteScalar());
            if (i == 0) { i = 1; }
            return i;

        }
        private readonly Type IEnumerableType = typeof(IEnumerable);
        private readonly Type StringType = typeof(string);
        public CustomObject SaveChanges(string schema, object Model, bool is_msg_create = false)
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
                            _pK = _Modelkey;
                            //SqlConnection connection, SqlTransaction transaction
                            cmd = GenerateUpdateComand(schema, Model);
                            cmd.Connection = con;
                            cmd.Connection.Open();
                            int i = cmd.ExecuteNonQuery();
                            //Iterate Detail one by one
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
                                            cmd = GenerateUpdateComand(schema, entity);
                                            cmd.Connection = con;
                                            cmd.ExecuteNonQuery();

                                        }
                                        else if (_EntityKey != 0 && _EntityState == EntitySate.Deleted.ToString())
                                        {
                                            cmd = Delete(schema, entity);
                                            cmd.Connection = con;
                                            cmd.ExecuteNonQuery();

                                        }
                                        else if (_EntityState == "")
                                        {
                                            entity.GetType().GetProperty(_mskey).SetValue(entity, _Modelkey);
                                            Insertion(schema, entity);
                                            //cmd = GenerateColAndParam(schema, entity);
                                            //cmd.Connection = con;
                                            //cmd.ExecuteNonQuery();
                                        }
                                    }
                                }

                            }
                            //_globalTrans.Commit();
                            return new CustomObject
                            {
                                Data = SelectRecord(schema, Model.GetType().Name.ToLower(), _mskey, _pK),
                                Message = "Record has been Updated successfully",
                                status = true
                            };
                        }
                        catch (Exception e)
                        {
                            //_globalTrans.Rollback();
                            cmd.Dispose();
                            _globalTrans.Dispose();
                            un.WriteException(e.ToString(), "SaveChanges");
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
                        un.WriteException(ex.ToString(), "SaveChanges");
                        return new CustomObject { Data = null, Message = ex.ToString(), status = false };
                    }
                }

            }
            return Insertion(schema, Model, is_msg_create);

        }


        public CustomObject Insertion(string schema, object Model, bool is_msg_create = false)
        {
            UniversalRepository  un = new UniversalRepository();
            //un.WriteException(Model.GetType().Name.ToString().ToLower(),"custom");
            var query = "";
            bool isUserMAnagement = false;
            bool isRoleMAnagement = false;
            //is_msg_create = true;
            //Reterive Details Of Model
            var _Child = Model.GetType()
             .GetProperties()
             .Where(p => IEnumerableType.IsAssignableFrom(p.PropertyType) && !StringType.IsAssignableFrom(p.PropertyType));
            //Get Name Of primary Key of Model
            string _mskey = Model.GetType().GetProperties().FirstOrDefault().Name;
            //Get Primary Key Of  Model
            int _pK = 0;

            using (SqlConnection con = new SqlConnection(constr))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlTransaction _globalTrans = null;
                    string model_name = Model.GetType().Name.ToString().ToLower();
                    string doc_typ ="";
                    con.Open();
                    try
                    {
                        if (is_msg_create)
                        {
                            if (model_name.ToLower().Contains("txn_dtl_input"))
                            {
                                doc_typ = Model.GetType().GetProperty("MSG_TYP").GetValue(Model).ToString().Contains("103") ? "1001" : Model.GetType().GetProperty("MSG_TYP").GetValue(Model).ToString().Contains("202") ? "1003" : "1002";
                            }
                            else if (model_name.ToLower().Contains("ach"))
                            {
                                doc_typ = "1004";
                            }
                            else if (model_name.ToLower().Contains("inbound"))
                            {
                                doc_typ = Model.GetType().GetProperty("Cbuae_MSG_TYP").GetValue(Model).ToString().Contains("103") ? "1001" : Model.GetType().GetProperty("Cbuae_MSG_TYP").GetValue(Model).ToString().Contains("202") ? "1003" : "1002";
                            }
                            string snder_ref = "";
                            if (doc_typ != "1004")
                            {
                                if (model_name.ToLower().Contains("txn_dtl_input"))
                                {
                                    if (Model.GetType().GetProperty("C20_SNDR_REF").GetValue(Model, null) != null)
                                    {
                                        snder_ref = Model.GetType().GetProperty("C20_SNDR_REF").GetValue(Model, null).ToString();
                                    }
                                }
                                else
                                {
                                    if (Model.GetType().GetProperty("SNDers_REF").GetValue(Model, null) != null)
                                    {
                                        snder_ref = Model.GetType().GetProperty("SNDers_REF").GetValue(Model, null).ToString();
                                    }
                                }
                            }
                            else if (!string.IsNullOrEmpty(Model.GetType().GetProperty("msg_id").GetValue(Model, null).ToString()))
                            {
                                snder_ref = Model.GetType().GetProperty("msg_id").GetValue(Model, null).ToString();
                            }
                            else
                            {
                                snder_ref = "";
                            }
                            query = "insert into tr_app.DOC_ID_MSTR (DOC_TYP_SK,SNDER_REF)values('" + doc_typ + "','" + snder_ref + "')";
                            doInsertUpdate(query, con);
                            _globalTrans = con.BeginTransaction();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select max(Doc_sK) from tr_app.Vu_DOC_ID_MSTR where SNDER_REF in ('" + snder_ref + "') and doc_typ_sk in ('" + doc_typ + "')");
                        }
                        else if (model_name.ToLower() == ("vu_user_br_mapping"))
                        {
                            _globalTrans = con.BeginTransaction();
                            //SqlCommand cmd = con.CreateCommand();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select max({0}) from {1}.user_br_mapping", _mskey, schema, Model.GetType().Name.ToLower());                            
                        }
                        else if (model_name.ToLower() == ("vu_user_role_mapping"))
                        {
                            _globalTrans = con.BeginTransaction();
                            //SqlCommand cmd = con.CreateCommand();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select max({0}) from {1}.user_role_mapping", _mskey, schema, Model.GetType().Name.ToLower());
                        }
                        else if (model_name.ToLower() == ("vu_user_lvl_can_do"))
                        {
                            _globalTrans = con.BeginTransaction();
                            //SqlCommand cmd = con.CreateCommand();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select max({0}) from {1}.user_lvl_can_do", _mskey, schema, Model.GetType().Name.ToLower());
                        }
                        else if (model_name.ToLower() == ("vu_role_lvl_can_do"))
                        {
                            _globalTrans = con.BeginTransaction();
                            //SqlCommand cmd = con.CreateCommand();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select max({0}) from {1}.role_lvl_can_do", _mskey, schema, Model.GetType().Name.ToLower());
                        }                        
                        else if (model_name.ToLower() == ("vu_role_mst"))
                        {
                            _globalTrans = con.BeginTransaction();
                            //SqlCommand cmd = con.CreateCommand();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select * from security.vu_role_mst_aprv where role_desc = '"+Model.GetType().GetProperty("role_desc").GetValue(Model, null).ToString()+ "'");
                            isRoleMAnagement = true;
                        }
                        else if (model_name.ToLower() == ("users"))
                        {
                            _globalTrans = con.BeginTransaction();
                            //SqlCommand cmd = con.CreateCommand();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select * from security.vu_users_aprv where user_full_name = '" + Model.GetType().GetProperty("user_ful_name").GetValue(Model, null).ToString() + "'");
                            isUserMAnagement = true;
                        }
                        else
                        {

                            _globalTrans = con.BeginTransaction();
                            //SqlCommand cmd = con.CreateCommand();
                            cmd = new SqlCommand(query, con, _globalTrans);
                            query = string.Format("select max({0}) from \"{1}\".{2}", _mskey, schema, Model.GetType().Name.ToLower());
                        }
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
                        if (is_msg_create)
                        {
                            if (model_name.ToLower().Contains("txn_dtl_input"))
                            {
                                Model.GetType().GetProperty("Doc_SK").SetValue(Model, _pK);
                            }
                            else if (model_name.ToLower().Contains("ach"))
                            {
                                Model.GetType().GetProperty("ach_mst_Id").SetValue(Model, _pK);
                            }
                            else if (model_name.ToLower().Contains("inbound"))
                            {
                                Model.GetType().GetProperty("DOC_SK").SetValue(Model, _pK);
                            }
                        }
                        else if (isUserMAnagement)
                        {
                            Model.GetType().GetProperty(_mskey).SetValue(Model, _pK);
                        }
                        else if (isRoleMAnagement)
                        {
                            Model.GetType().GetProperty(_mskey).SetValue(Model, _pK);
                        }
                        else
                        {
                            Model.GetType().GetProperty(_mskey).SetValue(Model, _pK + 1);
                        }
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
                                        if (is_msg_create)
                                        {
                                            entry.GetType().GetProperty(_mskey).SetValue(entry, _pK);
                                        }
                                        else
                                        {
                                            entry.GetType().GetProperty(_mskey).SetValue(entry, _pK + 1);
                                        }
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
                            Data = SelectRecord(schema, Model.GetType().Name.ToLower(), _mskey, (is_msg_create||isUserMAnagement||isRoleMAnagement) ? _pK : _pK + 1),
                            Message = "Record has been saved successfully",
                            status = true
                        };
                    }
                    catch (Exception e)
                    {
                        //_globalTrans.Rollback();
                        cmd.Dispose();
                        //_globalTrans.Dispose();
                        un.WriteException(e.ToString(), "Insertion");
                        return new CustomObject { Data = null, Message = e.ToString(), status = false };
                    }
                    finally
                    {
                        con.Close();
                        cmd.Dispose();
                        //_globalTrans.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    un.WriteException(ex.ToString(), "Insertion");
                    return new CustomObject { Data = null, Message = ex.ToString(), status = false };
                }
            }

        }
        public DataTable GetProcedureRecord(string schema, string tablename, string Param, int value)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(schema + '.' + tablename.ToLower(), con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(Param, value);
                DataTable tb = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Dispose();
                con.Close();
                return dt;
            }
        }
        private SqlCommand Delete(string schema, object Model)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = String.Format("DELETE FROM  \"{0}\".{1}  WHERE {2}=@param", schema, Model.GetType().Name.ToLower(), Model.GetType().GetProperties().FirstOrDefault().Name);
            cmd.Parameters.AddWithValue("@param", Model.GetType().GetProperties().FirstOrDefault().GetValue(Model));
            return cmd;
        }
        //Return Parameterize Update Command Of Model
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
        public DataTable SelectRecord(string schema, string tablename, string col, int key)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand();
                DataTable tb = new DataTable("DataTable");
                SqlDataAdapter da;
                var q = string.Format("select *  from {0}.{1} WHERE {2} =@key", schema, tablename.ToLower(), col);
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
        public void Dispose()
        {

        }
    }
}