using Microsoft.Data.SqlClient;
using System.Windows;

namespace CIRCUIT.Model.DataRepositories
{
    public class AccountRepository : IAccountRepository
    {
        //private string connectionString = "Server=LAPTOP-DK8TN1UP\\SQLEXPRESS01;Database=Pos_db;Integrated Security=True;Trust Server Certificate=True";

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

        public void DeleteUserAccount(int userId)
        {
            string deleteQuery = "DELETE FROM users WHERE user_id = @UserId";

            using (var connection = GetConnection())  // Assuming GetConnection() returns the connection object
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);

                        // Execute the query and check how many rows were affected
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("User deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("No user found with the specified ID.", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Delete Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public List<UsersModel> FetchUser()
        {
            string queryUser = "SELECT user_id, username, role FROM users";
            var users = new List<UsersModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryUser, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new UsersModel()
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Role = reader.GetString(reader.GetOrdinal("role"))

                                };
                                users.Add(user);
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}");
                }
            }
            return users;
        }

        public List<UsersModel> FetchUser(int userId)
        {
            var user = new List<UsersModel>();
            string query = "SELECT * FROM users WHERE user_id = @userId";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var account = new UsersModel
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    FullName = reader.GetString(reader.GetOrdinal("fullname")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    Role = reader.GetString(reader.GetOrdinal("role")),
                                    Salt = reader.GetString(reader.GetOrdinal("salt")),
                                };
                                user.Add(account);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}");
                }

            }
            return user;
        }

        public UsersModel FetchUserPassAndSalt(string username)
        {
            string queryUser = "SELECT * FROM Users WHERE username = @username";
            UsersModel user = null;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(queryUser, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())  // Check if the user exists
                            {
                                user = new UsersModel()
                                {
                                    UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Role = reader.GetString(reader.GetOrdinal("role")),
                                    Password = reader.GetString(reader.GetOrdinal("password")),
                                    Salt = reader.GetString(reader.GetOrdinal("salt"))
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}");
                }
            }
            return user;
        }

        public bool InsertUser(UsersModel user)
        {
            string queryCheck = "SELECT COUNT(*) FROM users WHERE username = @Username";
            string queryInsert = @"INSERT INTO users (username, password, salt, role) 
                           VALUES (@Username, @Password, @Salt, @Role)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand checkCommand = new SqlCommand(queryCheck, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Username", user.Username);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show($"Username {user.Username} already exists!", "Username duplicate", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }

                string salt;
                string hashedPassword = PasswordHelper.HashPassword(user.Password, out salt);

                using (SqlCommand insertCommand = new SqlCommand(queryInsert, connection))
                {
                    insertCommand.Parameters.AddWithValue("@Username", user.Username);
                    insertCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    insertCommand.Parameters.AddWithValue("@Salt", salt);
                    insertCommand.Parameters.AddWithValue("@Role", user.Role);

                    insertCommand.ExecuteNonQuery();
                }
            }
            return true;
        }

        public void UpdateUserAccount(UsersModel user)
        {
            string updateQuery = "UPDATE users SET username = @Username, role = @Role, password = @Password, salt = @Salt WHERE user_id = @UserId";

            using (var connection = GetConnection())
            {
                connection.Open();

                string salt;
                MessageBox.Show($"Test {user.Password}");
                string hashedPassword = PasswordHelper.HashPassword(user.Password, out salt);
                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LogUserIn(int userId)
        {
            string query = @"
                            UPDATE users 
                            SET is_logged_in = 1, last_login_time = GETDATE() 
                            WHERE user_id = @userId;";

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
                    MessageBox.Show("Error logging in user: " + ex.Message);
                }
            }
        }

        public void LogUserOut(int userId)
        {
            string query = @"
                            UPDATE users 
                            SET is_logged_in = 0, last_logout_time = GETDATE() 
                            WHERE user_id = @userId;";

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
                    MessageBox.Show("Error logging out user: " + ex.Message);
                }
            }
        }

        public bool IsUserLoggedIn(int userId)
        {
            string query = @"
                            SELECT is_logged_in 
                            FROM users 
                            WHERE user_id = @userId;";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        var result = command.ExecuteScalar();
                        return result != null && (bool)result;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error checking user login status: " + ex.Message);
                    return false;
                }
            }
        }

    }
}
