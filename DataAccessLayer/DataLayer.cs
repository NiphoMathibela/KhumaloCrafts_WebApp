using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class DataLayer
    {
        public static string connectionString = @"Server=tcp:st10460431.database.windows.net,1433;Initial Catalog=KhumaloDB;Persist Security Info=False;User ID=nipho;Password=Excellent28@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        SqlConnection conn = new(connectionString);

        SqlCommand? cmd;

        void OpenCloseDatabase()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
            else
            {
                conn.Close();
            }
        }

        public void CreateUser(string name, string email, string passwordHash, string shippingAddress, string billingAddress)
        {
            OpenCloseDatabase();
            string sql = $"insert into Users(name, email, passwordHash, shippingAddress, billingAddress) values('{name}', '{email}', '{passwordHash}', '{shippingAddress}', '{billingAddress}')";
            cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
