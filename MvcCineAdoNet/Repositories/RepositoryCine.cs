using System.Data.SqlClient;

namespace MvcCineAdoNet.Repositories
{
    public class RepositoryCine
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryCine()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=CINEBBDD;Persist Security Info=True;User ID=SA;Password=MCSD2023;";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }
    }
}
