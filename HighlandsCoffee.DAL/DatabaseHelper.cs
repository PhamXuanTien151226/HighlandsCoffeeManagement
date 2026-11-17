using Microsoft.Data.SqlClient;

namespace HighlandsCoffee.DAL
{
    public static class DatabaseHelper
    {
        public static readonly string ConnectionString = @"Server=.\SQLEXPRESS;Database=HighlandsCoffeeDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection() => new SqlConnection(ConnectionString);
    }
}