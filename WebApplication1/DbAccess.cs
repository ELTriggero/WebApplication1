using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication1
{
    public class DbAccess
    {
        private static SqlConnection connection = new SqlConnection();
        private static SqlCommand command = new SqlCommand();
        private static SqlDataReader DbReader;
        private static SqlDataAdapter adapter = new SqlDataAdapter();
        public SqlTransaction DbTran;

        private static string strConnString = "";

        public int executeDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }
                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(adapter);
                DbCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AliExecuteDataAdapter(DataTable tblName, string strSelectSql)
        {
            try
            {
                if (connection.State == 0)
                {
                    createConn();
                }
                adapter.SelectCommand.CommandText = strSelectSql;
                adapter.SelectCommand.CommandType = CommandType.Text;
                SqlCommandBuilder DbCommandBuilder = new SqlCommandBuilder(adapter);
                DbCommandBuilder.ConflictOption = ConflictOption.OverwriteChanges;

                string inser = DbCommandBuilder.GetInsertCommand().ToString();
                string update = DbCommandBuilder.GetUpdateCommand().CommandText.ToString();
                string delete = DbCommandBuilder.GetDeleteCommand().CommandText.ToString();

                return adapter.Update(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void beginTrans()
        {
            try
            {
                if (DbTran == null)
                {
                    if (connection.State == 0)
                    {
                        createConn();
                        DbTran = connection.BeginTransaction();
                        adapter.SelectCommand.Transaction = DbTran;
                    }
                    else
                    {

                        DbTran = connection.BeginTransaction();
                        adapter.SelectCommand.Transaction = DbTran;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void commitTrans()
        {
            try
            {
                if (DbTran != null)
                {
                    DbTran.Commit();
                    DbTran = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void rollbackTrans()
        {
            try
            {
                if (DbTran != null)
                {
                    DbTran.Rollback();
                    DbTran = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void createConn()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = strConnString;
                    connection.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void closeConn()
        {
            connection.Close();

        }

        public void readDatathroughAdapter(string query, DataTable tblName)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }
                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                adapter = new SqlDataAdapter(command);
                adapter.Fill(tblName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader readDatathroughReader(string query)
        {
            //DataReader used to sequentially read data from a data source
            SqlDataReader reader;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }
                command.Connection = connection;
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int executeQuery(SqlCommand dbCommand)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    createConn();
                }
                dbCommand.Connection = connection;
                dbCommand.CommandType = CommandType.Text;

                return dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}