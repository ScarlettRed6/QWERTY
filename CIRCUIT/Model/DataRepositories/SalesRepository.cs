
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
                                   u.username AS CashierName FROM tbl_sales s INNER JOIN tbl_users u ON s.cashier_id = u.user_id";

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
            string query = "SELECT product_id, product_name FROM tbl_Products WHERE product_id IN (" + string.Join(",", productIds) + ")";

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
            string query = "SELECT sale_item_id, product_id, quantity, item_total_price FROM tbl_Sale_Items WHERE sale_id = @saleId";
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
            string query = "SELECT quantity FROM tbl_Sale_Items";
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

        public List<SaleHistoryModel> GetSalesHistoryWithItems()
        {
            string query = @"
    SELECT s.sale_id, s.date_time, COALESCE(s.cashier_id, 0) AS cashier_id, 
           s.total_amount, s.payment_method, s.customer_payment, s.change_given, 
           s.is_void, s.VoidReason,
           p.product_name, p.selling_price, 
           si.product_id, si.quantity, si.item_total_price
    FROM tbl_sales s
    INNER JOIN tbl_Sale_Items si ON s.sale_id = si.sale_id
    INNER JOIN tbl_products p ON si.product_id = p.product_id";

            var salesHistory = new List<SaleHistoryModel>();
            var salesDict = new Dictionary<int, SaleHistoryModel>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int saleId = reader.GetInt32(reader.GetOrdinal("sale_id"));

                            if (!salesDict.TryGetValue(saleId, out var sale))
                            {
                                sale = new SaleHistoryModel
                                {
                                    SaleId = saleId,
                                    DateTime = reader.GetDateTime(reader.GetOrdinal("date_time")),
                                    CashierId = reader.IsDBNull(reader.GetOrdinal("cashier_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("cashier_id")),
                                    TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount")),
                                    PaymentMethod = reader.GetString(reader.GetOrdinal("payment_method")),
                                    CustomerPaid = reader.GetDecimal(reader.GetOrdinal("customer_payment")),
                                    ChangeGiven = reader.GetDecimal(reader.GetOrdinal("change_given")),
                                    IsVoid = reader.GetBoolean(reader.GetOrdinal("is_void")),
                                    VoidReason = reader.IsDBNull(reader.GetOrdinal("VoidReason")) ? null : reader.GetString(reader.GetOrdinal("VoidReason")),
                                    SaleItems = new List<SaleItemModel>()
                                };
                                salesDict[saleId] = sale;
                                salesHistory.Add(sale);
                            }

                            var saleItem = new SaleItemModel
                            {
                                SaleId = saleId,
                                ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                                ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                ItemTotalPrice = reader.GetDecimal(reader.GetOrdinal("item_total_price")),
                                UnitPrice = reader.GetDecimal(reader.GetOrdinal("selling_price"))
                            };
                            sale.SaleItems.Add(saleItem);
                        }
                    }
                }
            }
            return salesHistory;
        }

        public void MarkSaleAsRefunded(int saleId, string refundReason)
        {
            string query = "UPDATE tbl_sales SET is_void = 1, VoidReason = @Reason WHERE sale_id = @SaleId";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SaleId", saleId);
                        command.Parameters.AddWithValue("@Reason", refundReason);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to mark sale as refunded: {ex.Message}");
                }
            }
        }


        public void MarkItemAsRefunded(int saleItemId, int refundQuantity)
        {
            string query = "UPDATE tbl_Sale_Items SET is_refunded = 1 WHERE sale_item_id = @saleItemId";
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@saleItemId", saleItemId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error marking item as refunded: {ex.Message}");
                }
            }
        }

        // Adjust inventory for refunded items
        public void RestockItem(int productId, int quantity)
        {
            string query = "UPDATE tbl_Products SET stock_quantity = stock_quantity + @quantity WHERE product_id = @productId";
            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error restocking item: {ex.Message}");
                }
            }
        }

        public void RefundSale(string saleId, string reason)
        {
            string query = "UPDATE tbl_sales SET is_void = 1, VoidReason = @Reason WHERE sale_id = @SaleId";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SaleId", saleId);
                        command.Parameters.AddWithValue("@Reason", reason);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error processing refund: {ex.Message}");
                }
            }
        }


        public void UpdateTotalAmountAfterRefund(int saleId, decimal totalRefundAmount)
        {
            string query = "UPDATE tbl_sales SET total_amount = total_amount - @RefundAmount WHERE sale_id = @SaleId";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SaleId", saleId);
                        command.Parameters.AddWithValue("@RefundAmount", totalRefundAmount);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating total amount: {ex.Message}");
                }
            }
        }


        //INSERT DATA DI KO ALAM IF GAGAMITIN KO PA TO OR HINDI NA, pwede naman kasi sya ma filter
        /*
        public void InsertRefundItem(int saleId, int productId, int quantity, decimal refundAmount, string refundReason, DateTime refundDateTime)
        {
            string query = "INSERT INTO RefundedItems (sale_item_id, refund_quantity, refund_datetime, refund_amount, refund_reason, product_id) " +
                           "VALUES (@saleItemId, @quantity, @refundDateTime, @refundAmount, @refundReason, @productId)";

            using (var connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@saleItemId", saleId);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@refundAmount", refundAmount);
                        command.Parameters.AddWithValue("@refundReason", refundReason);
                        command.Parameters.AddWithValue("@refundDateTime", refundDateTime);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inserting refund item: {ex.Message}");
                }
            }
        }
        */

    }
}
