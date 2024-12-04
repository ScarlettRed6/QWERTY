using CIRCUIT.Model;
using CIRCUIT.Model.NewSaleModel;
using Microsoft.Data.SqlClient;
using System.Windows;

namespace CIRCUIT.Utilities
{
    public class Db
    {
        //Comment each of our local connection for local use
        private string connectionString = "Server=LAPTOP-DK8TN1UP\\SQLEXPRESS01;Database=Pos_db;Integrated Security=True;Trust Server Certificate=True";

        //private string connectionString = "Data Source=localhost;Initial Catalog = Pos_db; Persist Security Info=True;User ID = carl; Password=carlAmbatunut;" +
        //                               "Trust Server Certificate=True";

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

        public List<ProductModel> FetchProducts(string query)
        {
            var products = new List<ProductModel>();    

            using (var connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new ProductModel
                            {
                                ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                                ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                                Category = reader.GetString(reader.GetOrdinal("category")),
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

            return products;
        }

        //Method to insert new products to the database
        public void InsertProduct(ProductModel product)
        {
            string query = @"INSERT INTO Products 
                            (product_name, model_number, brand, category, description, selling_price, min_stock_level, stock_quantity, unit_cost, sku, is_archived, image_path)
                            VALUES (@ProductName, @ModelNumber, @Brand, @Category, @Description, @SellingPrice, @MinStockLevel, @StockQuantity, @UnitCost, @SKU, @IsArchived, @ImagePath)";

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
                    command.Parameters.AddWithValue("@ImagePath", product.ImagePath);

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
                                    UnitCost = (decimal)reader.GetDecimal(reader.GetOrdinal("unit_cost")),
                                    SellingPrice = (decimal)reader.GetDecimal(reader.GetOrdinal("selling_price")),
                                    ImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, reader.GetString(reader.GetOrdinal("image_path")))
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
                                 "selling_price = @sellingPrice, min_stock_level = @minStockLevel, stock_quantity = @stockQuantity, unit_cost = @unitCost, image_path = @ImagePath WHERE product_id = @productId";

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
                    command.Parameters.AddWithValue("@ImagePath", product.ImagePath);
                    command.Parameters.AddWithValue("@productId", product.ProductId);

                    command.ExecuteNonQuery();

                }
            }
        }

        // Method to update only the product's stock quantity by product Id
        public void UpdateProductStockQuantity(int productId, int quantitySold)
        {
            string updateQuery = "UPDATE Products SET stock_quantity = stock_quantity - @quantitySold WHERE product_id = @productId";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@quantitySold", quantitySold);
                    command.Parameters.AddWithValue("@productId", productId);

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

        //Method to fetch day for the last 7 days
        public List<DailyRevenue> FetchLast7DaysRevenue()
        {
            string query = @"
                            SELECT 
                                CAST(s.date_time AS DATE) AS SaleDate, 
                                SUM(s.total_amount) AS TotalRevenue
                            FROM 
                                sales s
                            WHERE 
                                s.date_time >= DATEADD(DAY, -7, GETDATE())
                            GROUP BY 
                                CAST(s.date_time AS DATE)
                            ORDER BY 
                                SaleDate;";

            var revenues = new List<DailyRevenue>();

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
                                var revenue = new DailyRevenue
                                {
                                    SaleDate = reader.GetDateTime(reader.GetOrdinal("SaleDate")),
                                    TotalRevenue = reader.GetDecimal(reader.GetOrdinal("TotalRevenue"))
                                };

                                revenues.Add(revenue);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching revenue: " + ex.Message + "\n" + ex.StackTrace);
                }
            }

            return revenues;
        }

        //Method to get the grossprofit and total revenue
        public (decimal TotalRevenue, decimal TotalCOGS, decimal GrossProfit) FetchGrossProfit(DateTime? startDate = null, DateTime? endDate = null)
        {
            string query = @"
                            SELECT 
                                SUM(s.total_amount) AS TotalRevenue, 
                                SUM(si.quantity * p.unit_cost) AS TotalCOGS
                            FROM 
                                sales s
                            JOIN 
                                sale_items si ON s.sale_id = si.sale_id
                            JOIN 
                                products p ON si.product_id = p.product_id";

            // Add date filter if specified
            if (startDate.HasValue && endDate.HasValue)
            {
                query += " WHERE s.date_time >= @StartDate AND s.date_time <= @EndDate";
            }

            decimal totalRevenue = 0;
            decimal totalCOGS = 0;

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        if (startDate.HasValue && endDate.HasValue)
                        {
                            command.Parameters.AddWithValue("@StartDate", startDate.Value);
                            command.Parameters.AddWithValue("@EndDate", endDate.Value);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalRevenue = reader.IsDBNull(reader.GetOrdinal("TotalRevenue")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalRevenue"));
                                totalCOGS = reader.IsDBNull(reader.GetOrdinal("TotalCOGS")) ? 0 : reader.GetDecimal(reader.GetOrdinal("TotalCOGS"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error calculating gross profit: " + ex.Message);
                }
            }

            decimal grossProfit = totalRevenue - totalCOGS;
            return (totalRevenue, totalCOGS, grossProfit);
        }

        //Method to fetch revenue per category
        public Dictionary<string, decimal> FetchRevenuePerCategory()
        {
            string query = @"SELECT p.category AS Category, SUM(si.quantity * si.item_total_price) AS TotalRevenue
                            FROM products p JOIN sale_items si ON p.product_id = si.product_id
                            JOIN sales s ON si.sale_id = s.sale_id
                            GROUP BY p.category";

            var revenuePerCategory = new Dictionary<string, decimal>();

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var category = reader.GetString(reader.GetOrdinal("Category"));
                                var totalRevenue = reader.GetDecimal(reader.GetOrdinal("TotalRevenue"));

                                revenuePerCategory[category] = totalRevenue;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error fetching revenue data: " + ex.Message);
                }
            }

            return revenuePerCategory;
        }

        //ETO UNG BAGO 

        public int InsertSale(SaleModel sale)
        {
            string query = @"INSERT INTO sales (date_time, total_amount, payment_method, customer_payment, change_given, cashier_id)
                    VALUES (@DateTime, @TotalAmount, @PaymentMethod, @CustomerPaid, @ChangeGiven, @CashierId);
                    SELECT SCOPE_IDENTITY();";

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
                    int newSaleId = Convert.ToInt32(command.ExecuteScalar());
                    return newSaleId;
                }
            }
        }

        public List<SaleHistoryModel> GetSalesHistory()
        {
            string query = @"SELECT sale_id, date_time, COALESCE(cashier_id, 0) AS cashier_id, 
                             total_amount, payment_method, customer_payment, change_given, 
                             is_void, VoidReason
                     FROM sales";

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
                                    CashierId = reader.IsDBNull(reader.GetOrdinal("cashier_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("cashier_id")),
                                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                                    PaymentMethod = reader.GetString(reader.GetOrdinal("payment_method")),
                                    CustomerPaid = reader.GetDecimal(reader.GetOrdinal("customer_payment")),
                                    ChangeGiven = reader.GetDecimal(reader.GetOrdinal("change_given")),
                                    IsVoid = reader.GetBoolean(reader.GetOrdinal("is_void")),
                                    VoidReason = reader.IsDBNull(reader.GetOrdinal("VoidReason")) ? null : reader.GetString(reader.GetOrdinal("VoidReason"))
                                };

                                salesHistory.Add(sale);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching sales history: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return salesHistory;
        }





        public void InsertSaleItem(SaleItemModel saleItem)
        {
            string query = @"INSERT INTO Sale_Items (sale_id, product_id, quantity, item_total_price)
                     VALUES (@SaleId, @ProductId, @Quantity, @ItemTotalPrice)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Use parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@SaleId", saleItem.SaleId);
                    command.Parameters.AddWithValue("@ProductId", saleItem.ProductId);
                    command.Parameters.AddWithValue("@Quantity", saleItem.Quantity);
                    command.Parameters.AddWithValue("@ItemTotalPrice", saleItem.ItemTotalPrice);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<SaleItemModel> GetSaleItemsBySaleId(int saleId)
        {
            string query = @"SELECT 
                        sale_item_id, 
                        sale_id, 
                        product_id, 
                        quantity, 
                        item_total_price
                     FROM Sale_Items
                     WHERE sale_id = @SaleId";

            var saleItems = new List<SaleItemModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SaleId", saleId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var saleItem = new SaleItemModel
                                {
                                    SaleItemId = reader.GetInt32(reader.GetOrdinal("sale_item_id")),
                                    SaleId = reader.GetInt32(reader.GetOrdinal("sale_id")),
                                    ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                    ItemTotalPrice = reader.GetDecimal(reader.GetOrdinal("item_total_price"))
                                };

                                saleItems.Add(saleItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching sale items: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return saleItems;
        }

        public bool UpdateVoidOrder(int saleId, string voidReason)
        {
            string query = @"UPDATE sales
                     SET is_void = 1, VoidReason = @VoidReason
                     WHERE sale_id = @SaleId";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VoidReason", voidReason);
                        command.Parameters.AddWithValue("@SaleId", saleId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;  // Return true if update was successful
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating void order: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }


        // HANGGANG DITO



    }
}