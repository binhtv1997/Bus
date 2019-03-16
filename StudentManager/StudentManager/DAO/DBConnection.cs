using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.DAO
{
    public class DBConnection
    {
        private  SqlDataAdapter myAdapter;
        private SqlConnection conn;
        public DBConnection()
        {
            string strConn = ConfigurationManager.
                     ConnectionStrings["dbConnectionString"].ConnectionString;
            conn = new SqlConnection(strConn);
        }

        private SqlConnection OpenConnection()
        {
            if (conn.State == System.Data.ConnectionState.Closed || conn.State == System.Data.ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        public DataTable ExecuteSelectQuery(String _query,
            SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            myAdapter = new SqlDataAdapter();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = OpenConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                Trace.TraceError(e.Message);
                return null;
            }
            finally
            {
                conn.Close();
                myCommand.Dispose();
            }
            return dataTable;
        }

        public bool ExecuteInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = OpenConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.InsertCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Trace.TraceError(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
                myCommand.Dispose();
            }
            return true;
        }

        public bool ExecuteUpdateQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = OpenConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Trace.TraceError(e.Message);
                return false;
            }
            finally
            {
                conn.Close();
                myCommand.Dispose();
            }
            return true;
        }
    }
}
