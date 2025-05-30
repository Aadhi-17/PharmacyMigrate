using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KRATOS.DataAcessLayer
{
    public class GetDB
    {
        public static string Connection_String(string server, string dbname)
        {
            try
            {
                string config = "SSMSSerever";
                string connectionstring = ConfigurationManager.ConnectionStrings[config].ConnectionString;
                connectionstring = $@"Server={server};Database={dbname};" + connectionstring;
                return connectionstring;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static string MySQLConnection_String(string config)
        {
            try
            {
                string connectionstring = ConfigurationManager.ConnectionStrings[config.ToLower()].ConnectionString;
                return connectionstring;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static string Connection(string server)
        {
            try
            {
                string connectionstring = ConfigurationManager.ConnectionStrings[server.ToLower()].ToString();
                return connectionstring;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public static string GetAccountid(string connectionstring, string dbName)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                try
                {
                    connection.Open();
                    string sql = $@"USE {dbName} SELECT accountid FROM Account";
                    SqlCommand command = new SqlCommand(sql, connection);
                    var result = command.ExecuteScalar();
                    return result.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
            }
        }

        public static string GetAccountName(string connectionstring, string dbName)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                try
                {
                    connection.Open();
                    string sql = $@"USE {dbName} SELECT accountname FROM Account";
                    SqlCommand command = new SqlCommand(sql, connection);
                    var result = command.ExecuteScalar();
                    return result.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
            }
        }

        public static List<string> Get_Databse(string connectionString, string valueToSearch)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    List<string> dbList = new List<string>();

                    string sql = @"SELECT name FROM sys.databases 
                           WHERE name LIKE 'CS_Staging_%' AND name LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{valueToSearch}%");

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dbList.Add(dr[0].ToString());
                            }
                        }
                    }

                    return dbList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

    }
}
