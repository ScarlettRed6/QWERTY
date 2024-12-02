
using Microsoft.Data.SqlClient;
using System.Windows;

namespace CIRCUIT.Model.DataRepositories
{
    public class SalesRepository : ISalesRepository
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

        public List<SaleModel> FetchSales()
        {
            //This also fetches from users table
            string query = @"SELECT s.sale_id, s.date_time, s.cashier_id, s.total_amount, 
                                   s.payment_method, s.customer_payment, s.change_given, 
                                   u.username AS CashierName FROM sales s INNER JOIN users u ON s.cashier_id = u.user_id";

            var sales = new List<SaleModel>();

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
                                var sale = new SaleModel
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
    }
}
