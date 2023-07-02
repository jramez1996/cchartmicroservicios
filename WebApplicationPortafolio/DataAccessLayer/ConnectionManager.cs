using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplicationPortafolio.DataAccessLayer
{
    public class ConnectionManager
    {

        //private static readonly Lazy<SqlConnection> _lazyConnection = new Lazy<SqlConnection>(CreateConnection);

        private static string connectionString = "Data Source=localhost;Initial Catalog=basedatosprueba;User ID=sa;Password=AdminSqlServer2019";



        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }
}