using CIRCUIT.Model;
using CIRCUIT.Model.NewSaleModel;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace CIRCUIT.Utilities
{
    public class Db
    {
        //Comment each of our local connection for local use
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
                                    //nilagya ko ng description dito tapos dun sa catalogviewmodel mo pre
                                    Description = reader.GetString(reader.GetOrdinal("description")),
                                    Brand = reader.GetString(reader.GetOrdinal("brand")),
                                    ModelNumber = reader.GetString(reader.GetOrdinal("model_number")),
                                    StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                                    UnitCost = (decimal)reader.GetDecimal(reader.GetOrdinal("unit_cost")),
                                    SellingPrice = (decimal)reader.GetDecimal(reader.GetOrdinal("selling_price"))
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
                                    SellingPrice = (decimal)reader.GetDecimal(reader.GetOrdinal("selling_price")),
                                    MinStockLevel = reader.GetInt32(reader.GetOrdinal("min_stock_level")),
                                    StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                                    UnitCost = (decimal)reader.GetDecimal(reader.GetOrdinal("unit_cost")),
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

        //ETO UNG BAGO 

        public void InsertSale(SaleModel sale)
        {
            //string query = @"INSERT INTO sales (date_time, cashier_id, total_amount, payment_method, customer_payment, change_given)
            //        VALUES (@DateTime, @CashierId, @TotalAmount, @PaymentMethod, @CustomerPaid, @ChangeGiven)";
            string query = @"INSERT INTO sales (date_time, total_amount, payment_method, customer_payment, change_given)
                    VALUES (@DateTime, @TotalAmount, @PaymentMethod, @CustomerPaid, @ChangeGiven)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DateTime", sale.DateTime);
                    command.Parameters.AddWithValue("@CashierId", sale.CashierId);
                    command.Parameters.AddWithValue("@TotalAmount", sale.TotalAmount);
                    command.Parameters.AddWithValue("@PaymentMethod", sale.PaymentMethod);
                    command.Parameters.AddWithValue("@CustomerPaid", sale.CustomerPaid);
                    command.Parameters.AddWithValue("@ChangeGiven", sale.ChangeGiven);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<SaleHistoryModel> GetSalesHistory()
        {
            string query = @"SELECT sale_id, date_time, COALESCE(cashier_id, 0) AS cashier_id, total_amount, payment_method, customer_payment, change_given FROM sales";
            List<SaleHistoryModel> salesHistory = new List<SaleHistoryModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var sale = new SaleHistoryModel
                                {
                                    SaleId = reader.GetInt32(reader.GetOrdinal("sale_id")),
                                    DateTime = reader.GetDateTime(reader.GetOrdinal("date_time")),
                                    //papallitan nalang to if ok n ung login cashierID bawal kasi magsend here ng may null
                                    CashierId = reader.IsDBNull(reader.GetOrdinal("cashier_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("cashier_id")),
                                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                                    PaymentMethod = reader.GetString(reader.GetOrdinal("payment_method")),
                                    CustomerPaid = reader.GetDecimal(reader.GetOrdinal("customer_payment")),
                                    ChangeGiven = reader.GetDecimal(reader.GetOrdinal("change_given"))
                                };

                                salesHistory.Add(sale);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Consider using a proper logging mechanism
                MessageBox.Show($"Error fetching sales history: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return salesHistory;
        }




        // HANGGANG DITO

        

    }
}