using CIRCUIT.Model;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace CIRCUIT.Utilities
{
    public class Db
    {
        //Comment each of our local connection for local use
        //private string connectionString = "Server=LAPTOP-DK8TN1UP\\SQLEXPRESS01;Integrated Security=True;";
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

        // PRODUCTS QUERIES

        //Method to insert new products to the database
        public void InsertProduct(ProductModel product)
        {
            string query = @"INSERT INTO Products 
                            (product_name, model_number, brand, category, description, selling_price, min_stock_level, stock_quantity, unit_cost, sku, is_archived)
                            VALUES (@ProductName, @ModelNumber, @Brand, @Category, @Description, @SellingPrice, @MinStockLevel, @StockQuantity, @UnitCost, @SKU, @IsArchived)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Use parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@ModelNumber", product.ModelNumber);
                    command.Parameters.AddWithValue("@Brand", product.Brand);
                    command.Parameters.AddWithValue("@Category", product.Category);
                    command.Parameters.AddWithValue("@Description", product.Description);
                    command.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
                    command.Parameters.AddWithValue("@MinStockLevel", product.MinStockLevel);
                    command.Parameters.AddWithValue("@StockQuantity", product.StockQuantity);
                    command.Parameters.AddWithValue("@UnitCost", product.UnitCost);
                    command.Parameters.AddWithValue("@SKU", product.SKU);
                    command.Parameters.AddWithValue("@IsArchived", product.IsArchived);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        //Method to fetch data from the database like, SELECT * FROM
        public List<ProductModel> FetchData(string query)
        {
            var products = new List<ProductModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // product details, only limited rows for testing, add other details later like the min_stock_level
                                var product = new ProductModel
                                {
                                    ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                                    ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                                    Category = reader.GetString(reader.GetOrdinal("category")),
                                    Description = reader.GetString(reader.GetOrdinal("description")),
                                    Brand = reader.GetString(reader.GetOrdinal("brand")),
                                    ModelNumber = reader.GetString(reader.GetOrdinal("model_number")),
                                    StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                                    UnitCost = reader.GetDecimal(reader.GetOrdinal("unit_cost")),
                                    SellingPrice = reader.GetDecimal(reader.GetOrdinal("selling_price"))
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching data: " + ex.Message);
                }
            }

            return products;
        }

        //Method to fetch data by product ID
        public List<ProductModel> GetProductById(int productId)
        {
            var products = new List<ProductModel>();
            string query = "SELECT * FROM Products WHERE product_id = @productId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@productId", productId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var product = new ProductModel
                                {
                                    ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                                    ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                                    ModelNumber = reader.GetString(reader.GetOrdinal("model_number")),
                                    Brand = reader.GetString(reader.GetOrdinal("brand")),
                                    Category = reader.GetString(reader.GetOrdinal("category")),
                                    Description = reader.GetString(reader.GetOrdinal("description")),
                                    SellingPrice = reader.GetDecimal(reader.GetOrdinal("selling_price")),
                                    MinStockLevel = reader.GetInt32(reader.GetOrdinal("min_stock_level")),
                                    StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                                    UnitCost = reader.GetDecimal(reader.GetOrdinal("unit_cost")),
                                    SKU = reader.GetOrdinal("sku"),
                                    //IsArchived = reader.GetBoolean(reader.GetOrdinal("is_archived"))
                                };
                                products.Add(product);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}");
                }

            }
            return products;

        }

        //Method to update product by product Id
        public void UpdateProductData(ProductModel product)
        {
            string updateQuery = "UPDATE Products SET product_name = @productName, model_number = @modelNumber, brand = @brand, category = @category, description = @description, " +
                                 "selling_price = @sellingPrice, min_stock_level = @minStockLevel, stock_quantity = @stockQuantity, unit_cost = @unitCost WHERE product_id = @productId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@productName", product.ProductName);
                    command.Parameters.AddWithValue("@modelNumber", product.ModelNumber);
                    command.Parameters.AddWithValue("@brand", product.Brand);
                    command.Parameters.AddWithValue("@category", product.Category);
                    command.Parameters.AddWithValue("@description", product.Description);
                    command.Parameters.AddWithValue("@sellingPrice", product.SellingPrice);
                    command.Parameters.AddWithValue("@minStockLevel", product.MinStockLevel);
                    command.Parameters.AddWithValue("@stockQuantity", product.StockQuantity);
                    command.Parameters.AddWithValue("@unitCost", product.UnitCost);
                    //command.Parameters.AddWithValue("@sku", product.SKU);
                    //command.Parameters.AddWithValue("@isArchived", product.IsArchived);
                    command.Parameters.AddWithValue("@productId", product.ProductId);

                    command.ExecuteNonQuery();

                }
            }
        }

        //Method to archive a product by product Id
        public void ArchiveProduct(List<int> productId)
        {
            string archiveQuery = "UPDATE Products SET is_archived = 1 WHERE product_id IN (" +
                           string.Join(",", productId.Select(id => $"@Product{id}")) + ")";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(archiveQuery, connection))
                {
                    for (int i = 0; i < productId.Count; i++)
                    {
                        command.Parameters.AddWithValue($"@Product{productId[i]}", productId[i]);
                    }
                    command.ExecuteNonQuery();
                }
            }


        }

        //Recover archived products
        public void RecoverArchived(ProductModel product)
        {
            string recoverQuery = "UPDATE Products SET is_archived = 0 WHERE product_id = @productId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(recoverQuery, connection))
                {

                    command.Parameters.AddWithValue("@isArchived", product.IsArchived);
                    command.Parameters.AddWithValue("@productId", product.ProductId);

                    command.ExecuteNonQuery();

                }
            }

        }
        /*
        // USER ACCOUNTS QUERIES !!!

        //Method to insert users in the database
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

        //Method to update user account
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

        //Method to fetch users in the database
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

        //Method to fetch data by user ID, FetchUser overloaded
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

        //Method to fetch user for password verification
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
        */
        /*
        // SALES TRANSACTION QUERIES

        //Method to fetch sales data
        public List<SalesModel> FetchSales()
        {
            //This also fetches from users table
            string query = @"SELECT s.sale_id, s.date_time, s.cashier_id, s.total_amount, 
                                   s.payment_method, s.customer_payment, s.change_given, 
                                   u.username AS CashierName FROM sales s INNER JOIN users u ON s.cashier_id = u.user_id";

            var sales = new List<SalesModel>();

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var sale = new SalesModel
                                {
                                    SaleId = reader.GetInt32(reader.GetOrdinal("sale_id")),
                                    DateTime = reader.GetDateTime(reader.GetOrdinal("date_time")),
                                    CashierId = reader.GetInt32(reader.GetOrdinal("cashier_id")),
                                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                                    PaymentMethod = reader.GetString(reader.GetOrdinal("payment_method")),
                                    CustomerPayment = reader.GetDecimal(reader.GetOrdinal("customer_payment")),
                                    ChangeGiven = reader.GetDecimal(reader.GetOrdinal("change_given")),
                                    CashierName = reader.GetString(reader.GetOrdinal("CashierName"))
                                };

                                sales.Add(sale);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching sales: " + ex.Message + "\n" + ex.StackTrace);
                }
            }

            return sales;
        }

        //Method to fetch product names for fetching many data reducing the queries, this is an enchanced version of GetProductById method query but only gets the product name
        //Main used for sale item where i fetch the product name based on the product id from sale item table
        public Dictionary<int, string> GetProductNames(List<int> productIds)
        {
            var productNames = new Dictionary<int, string>();
            string query = "SELECT product_id, product_name FROM Products WHERE product_id IN (" + string.Join(",", productIds) + ")";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(reader.GetOrdinal("product_id"));
                                string productName = reader.GetString(reader.GetOrdinal("product_name"));
                                productNames[productId] = productName;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching product names: {ex.Message}");
                }
            }

            return productNames;
        }

        //Method to fetch sale items by sale id
        public List<SalesItemModel> FetchSaleItems(int saleId)
        {
            string query = "SELECT sale_item_id, product_id, quantity, item_total_price FROM Sale_Items WHERE sale_id = @saleId";
            var salesItems = new List<SalesItemModel>();

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    List<int> productIds = new List<int>();

                    // First query: collect product IDs
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@saleId", saleId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productIds.Add(reader.GetInt32(reader.GetOrdinal("product_id")));
                            }
                        }
                    }

                    // Get product names
                    var productNames = GetProductNames(productIds);

                    // Second query: fetch sale items with product names
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@saleId", saleId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = reader.GetInt32(reader.GetOrdinal("product_id"));

                                var saleItem = new SalesItemModel
                                {
                                    SaleItemId = reader.GetInt32(reader.GetOrdinal("sale_item_id")),
                                    ProductId = productId,
                                    ProductName = productNames.ContainsKey(productId) ? productNames[productId] : "Unknown",
                                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                    ItemTotalPrice = reader.GetDecimal(reader.GetOrdinal("item_total_price"))
                                };

                                salesItems.Add(saleItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching sale items: {ex.Message}");
                }
            }

            return salesItems;
        }

        //Fetch total quantity of products in sale
        public int FetchTotalProductSold()
        {
            string query = "SELECT quantity FROM Sale_Items";
            int total = 0;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                total += reader.GetInt32(reader.GetOrdinal("quantity"));
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}");
                }
            }
            return total;
        }

        */
    }
}
