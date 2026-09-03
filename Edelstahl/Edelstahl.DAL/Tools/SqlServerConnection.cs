using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;



namespace Edelstahl.DAL.Tools
{
    public static class SqlServerConnection
    {
        private const string ConnectionString =
            @"Data Source=localhost\SQLEXPRESS;
              Initial Catalog=Edelstahl;
              Integrated Security=True";

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}