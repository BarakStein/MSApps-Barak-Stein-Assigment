using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ServerApp
{
    //sealed class to operate all the functions that will send to the DB
    public sealed class DBService
    {
        private static DBService DbInstance = null;// static attribute for creating only
                                                   //one connection each time when the program runs

        private readonly MySqlConnection connection;

        //Connection string
        private static string conStr = "server=127.0.0.1;uid=root;password=318266376Barak;port=3306;database=MyDatabse";


        //Create new connection to DB
        private DBService(string connectionString) { connection = new MySqlConnection(connectionString); }

        //Static method that returns the DbInstace 
        public static DBService GetDBService()
        {
            if (DbInstance is null)
            {
                DbInstance = new DBService(conStr);
            }
            return DbInstance;
        }

        //Create MySQL command
        public MySqlCommand CreateCommand(string commandStr)
        {
            MySqlCommand cmd = new MySqlCommand(commandStr, connection);
            cmd.CommandTimeout = 10;
            cmd.CommandType = CommandType.Text;

            return cmd;
        }

        //Exec MySQL command
        public void ExecuteAndClose(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        //Get rows from DB
        public DataTable Select(MySqlCommand cmd)
        {
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(ds, "Table");
                DataTable dt = ds.Tables["Table"];

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                cmd.Connection.Close();
            }
        }

        //Add new row to table
        public void Insert(MySqlCommand cmd)
        {
            try
            {
                DataSet ds = new DataSet();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(ds, "Table");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                cmd.Connection.Close();
            }
        }


        //Update table row
        public int Update(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                DataSet ds = new DataSet();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(ds, "Table");
                return rowEffected;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                cmd.Connection.Close();
            }
        }


        //Delete table row
        public int Delete(MySqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                int rowEffected = cmd.ExecuteNonQuery();
                DataSet ds = new DataSet();
                MySqlDataAdapter mda = new MySqlDataAdapter(cmd);
                mda.Fill(ds, "Table");
                return rowEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            finally
            {
                cmd.Connection.Close();
            }
        }

        //Create a list of objects from a DataTable
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

        //Check for each column in each row 
        private T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}

