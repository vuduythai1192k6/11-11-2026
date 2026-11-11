using System.Configuration;
using System.Data.SqlClient;

namespace HRMPayrollSystem.DAL
{
    public static class DbHelper
    {
        public static string ConnString => ConfigurationManager.ConnectionStrings["HRMDB"]?.ConnectionString 
            ?? @"Data Source=.;Initial Catalog=HRMDB;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnString);
        }
    }
}
