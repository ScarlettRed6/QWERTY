using Microsoft.Data.SqlClient;
using System.Windows;

namespace CIRCUIT.Model.DataRepositories
{
    public class SessionRepository : ISessionRepository
    {
        private string connectionString = "Data Source=localhost;Initial Catalog = Pos_db; Persist Security Info=True;User ID = carl; Password=carlAmbatunut;" +
                                         "Trust Server Certificate=True";

        //Method to execute non queries like INSERT or UPDATE, might change this code later idk
        public void ExecuteNonQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error executing query: " + ex.Message);
                }
            }
        }
        //Gets the sql connection
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public void LogSessionStart(int userId)
        {
            string query = @"INSERT INTO sessions (user_id, login_time) VALUES (@userId, GETDATE());";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error logging session start: " + ex.Message);
                }
            }
        }

        public void LogSessionEnd(int userId)
        {
            string query = @"UPDATE sessions SET logout_time = GETDATE() WHERE user_id = @userId AND logout_time IS NULL;";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error logging session end: " + ex.Message);
                }
            }
        }

        public int? GetLoggedInUserId()
        {
            string query = @"SELECT TOP 1 user_id FROM sessions WHERE logout_time IS NULL ORDER BY login_time DESC;";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        var result = command.ExecuteScalar();
                        return result != null ? (int?)result : null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking logged-in user: " + ex.Message);
                    return null;
                }
            }
        }

    }
}
