using System.Data.SqlClient;

namespace Compact.Database
{
    internal class SqlClient
    {
        private string connectionString;

        public SqlClient(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void ExecuteNonQuery(string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        public int ExecuteScalar(string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                command.Connection.Open();
                var result = (int) command.ExecuteScalar();
                command.Connection.Close();

                return result;
            }
        }
    }
}
