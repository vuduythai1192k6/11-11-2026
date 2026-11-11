using System.Configuration;
using System.Data.SqlClient;

namespace RetailSalesSystem.DAL
{
    public static class DbHelper
    {
        public static string ConnString => ConfigurationManager.ConnectionStrings["SalesDB"]?.ConnectionString 
            ?? @"Data Source=.;Initial Catalog=SalesDB;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnString);
        }
    }
}
