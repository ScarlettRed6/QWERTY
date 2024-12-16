using LiveCharts.Wpf;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace CIRCUIT.Model.DataRepositories
{
    public class AccountRepository : IAccountRepository
    {
        //private string connectionString = "Server=LAPTOP-DK8TN1UP\\SQLEXPRESS01;Database=Pos_db;Integrated Security=True;Trust Server Certificate=True";

        private string connectionString = "Data Source=localhost;Initial Catalog = Pos_db; Persist Security Info=True;User ID = carl; Password=carlAmbatunut;" +
                                          "Trust Server Certificate=True";

        //Try mo to 
        //private string connectionString = "Server=localhost;Database=Pos_db;Integrated Security=True;Trust Server Certificate=True"

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

        // Method to deactivate user account(s)
        public void DeactivateUserAccount(List<int> userIds)
        {
            string updateQuery = "UPDATE tbl_users SET status = 'Inactive' WHERE user_id IN (";

            // Add a parameter for each user ID
            var parameters = new List<SqlParameter>();
            for (int i = 0; i < userIds.Count; i++)
            {
                updateQuery += $"@UserId{i},";
                parameters.Add(new SqlParameter($"@UserId{i}", userIds[i]));
            }

            // Remove the last comma
            updateQuery = updateQuery.TrimEnd(',');

            // Close the IN clause
            updateQuery += ")";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(updateQuery, connection))
                    {
                        // Add the parameters to the command
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        MessageBox.Show($"{rowsAffected} user account(s) deactivated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deactivating user accounts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Method to update user account to active
        public void ActivateUserAccount(List<int> userIds)
        {
            string updateQuery = "UPDATE tbl_users SET status = 'Active' WHERE user_id IN (";

            // Add a parameter for each user ID
            var parameters = new List<SqlParameter>();
            for (int i = 0; i < userIds.Count; i++)
            {
                updateQuery += $"@UserId{i},";
                parameters.Add(new SqlParameter($"@UserId{i}", userIds[i]));
            }

            // Remove the last comma
            updateQuery = updateQuery.TrimEnd(',');

            // Close the IN clause
            updateQuery += ")";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(updateQuery, connection))
                    {
                        // Add the parameters to the command
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }

                        int rowsAffected = command.ExecuteNonQuery();

                        MessageBox.Show($"{rowsAffected} user account(s) activated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error activating user accounts: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public List<UsersModel> FetchUser()
        {
            string queryUser = "SELECT fullname, user_id, username, role, status, user_image FROM tbl_users";
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
                                    FullName = reader.GetString(reader.GetOrdinal("fullname")),
                                    UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                                    Username = reader.GetString(reader.GetOrdinal("username")),
                                    Role = reader.GetString(reader.GetOrdinal("role")),
                                    UserStatus = reader.GetString(reader.GetOrdinal("status")),
                                    UserImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reader.GetString(reader.GetOrdinal("user_image")))

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
            string query = "SELECT * FROM tbl_users WHERE user_id = @userId";

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
                                    UserImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reader.GetString(reader.GetOrdinal("user_image")))
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

        public bool HasAdminAccounts()
        {
            string query = "SELECT COUNT(*) FROM tbl_users WHERE role = @role";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@role", "Admin");

                        int adminCount = (int)command.ExecuteScalar();
                        return adminCount > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error checking admin accounts: {ex.Message}");
                    return false;
                }
            }
        }

        public UsersModel FetchUserPassAndSalt(string username)
        {
            string queryUser = "SELECT * FROM tbl_Users WHERE username = @username";
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
                                    Salt = reader.GetString(reader.GetOrdinal("salt")),
                                    UserStatus = reader.GetString(reader.GetOrdinal("status"))
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
            string queryCheck = "SELECT COUNT(*) FROM tbl_users WHERE username = @Username";
            string queryInsert = @"INSERT INTO tbl_users (fullname, username, password, salt, role, user_image) 
                                 VALUES (@FullName, @Username, @Password, @Salt, @Role, @UserImagePath)";

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
                    insertCommand.Parameters.AddWithValue("@FullName", user.FullName);
                    insertCommand.Parameters.AddWithValue("@Username", user.Username);
                    insertCommand.Parameters.AddWithValue("@Password", hashedPassword);
                    insertCommand.Parameters.AddWithValue("@Salt", salt);
                    insertCommand.Parameters.AddWithValue("@Role", user.Role);
                    insertCommand.Parameters.AddWithValue("@UserImagePath", user.UserImagePath);

                    insertCommand.ExecuteNonQuery();
                }
            }
            return true;
        }

        public void UpdateUserAccount(UsersModel user)
        {
            string updateQuery = "UPDATE tbl_users SET fullname = @FullName, username = @Username, role = @Role, password = @Password, salt = @Salt, user_image = @UserImagePath WHERE user_id = @UserId";

            using (var connection = GetConnection())
            {
                connection.Open();

                string salt;

                string hashedPassword = PasswordHelper.HashPassword(user.Password, out salt);
                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@Password", hashedPassword);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@UserImagePath", user.UserImagePath);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateAccountWithoutPassword(UsersModel user)
        {
            string updateQuery = "UPDATE tbl_users SET fullname = @FullName, username = @Username, role = @Role, user_image = @UserImagePath WHERE user_id = @UserId";

            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@FullName", user.FullName);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Role", user.Role);
                    command.Parameters.AddWithValue("@UserId", user.UserId);
                    command.Parameters.AddWithValue("@UserImagePath", user.UserImagePath);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void LogUserIn(int userId)
        {
            string query = @"
                            UPDATE tbl_users 
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
                            UPDATE tbl_users 
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
                            FROM tbl_users 
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
