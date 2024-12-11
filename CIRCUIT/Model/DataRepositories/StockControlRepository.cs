using iText.Layout.Borders;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace CIRCUIT.Model.DataRepositories
{
    public class StockControlRepository
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

        //Methd to fetch suppliers list
        public List<SuppliersModel> FetchSuppliers(string query)
        {
            var suppliers = new List<SuppliersModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var supp = new SuppliersModel
                            {
                                SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                                SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                                ContactName = reader.GetString(reader.GetOrdinal("ContactName")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address"))
                            };
                            suppliers.Add(supp);
                        }
                    }

                }
            }
            return suppliers;

        }

        //Overloaded method to fetchsuppliers, where it fetches based on suppliername
        public List<SuppliersModel> FetchSuppliers(string query, string selectedSupplier)
        {
            var suppliers = new List<SuppliersModel>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@SupplierName", selectedSupplier);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var supp = new SuppliersModel
                            {
                                SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                                SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                                ContactName = reader.GetString(reader.GetOrdinal("ContactName")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address"))
                            };
                            suppliers.Add(supp);
                        }
                    }
                }
            }
            return suppliers;

        }

        //Method to fetch only the supplier name
        public List<string> FetchSupplierNames()
        {
            var supplierNames = new List<string>();
            string query = "SELECT SupplierName FROM tbl_suppliers";

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            supplierNames.Add(reader.GetString(reader.GetOrdinal("SupplierName")));
                        }
                    }
                }
            }
            return supplierNames;
        }

        //Method to fetch only a partial details from products table 
        public List<ProductModel> FetchPartialProductDetails(string query)
        {
            var prods = new List<ProductModel>();
            using (SqlConnection connection = GetConnection())
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
                                Brand = reader.GetString(reader.GetOrdinal("brand")),
                                ModelNumber = reader.GetString(reader.GetOrdinal("model_number")),
                                UnitCost = reader.GetDecimal(reader.GetOrdinal("unit_cost"))
                            };
                            prods.Add(product);
                        }
                    }
                }
            }

            return prods;
        }

        //Method to fetch orders
        public List<PurchaseOrderModel> FetchPurchaseOrders(string query)
        {
            var orders = new List<PurchaseOrderModel>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new PurchaseOrderModel
                            {
                                OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                                SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                                SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")), // Fetch from joined table
                                OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate")),
                                Status = reader.GetString(reader.GetOrdinal("Status")),
                                TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                                ShippingFee = reader.GetDecimal(reader.GetOrdinal("ShippingFee"))
                            };
                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }

        //Method to fetch purchaseorder items and also product name based on product id
        public List<PurchaseOrderDetailModel> FetchPurchaseOrderItems(int orderid)
        {
            string query = "SELECT pod.OrderDetailID, pod.OrderID, pod.ProductID, pod.Quantity, pod.UnitPrice, p.product_name FROM tbl_purchaseorderdetails pod INNER JOIN tbl_products p ON pod.ProductID = p.product_id WHERE pod.OrderID = @OrderID";

            var orderDetails = new List<PurchaseOrderDetailModel>();
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderID", orderid);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var orderDetail = new PurchaseOrderDetailModel
                            {
                                OrderDetailID = reader.GetInt32(reader.GetOrdinal("OrderDetailID")),
                                OrderID = reader.GetInt32(reader.GetOrdinal("OrderID")),
                                ProductID = reader.GetInt32(reader.GetOrdinal("ProductID")),
                                Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                                UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                                ProductName = reader.GetString(reader.GetOrdinal("product_name"))
                            };

                            orderDetails.Add(orderDetail);
                        }
                    }
                }
            }

            return orderDetails;

        }

        //Insert values to purchaseorders and return the id
        public int InsertOrder(int supplierId, decimal totalAmount, decimal shippingFee)
        {
            string query = "INSERT INTO tbl_PurchaseOrders (SupplierID, OrderDate, Status, TotalAmount, ShippingFee) " +
                   "OUTPUT INSERTED.OrderID " +
                   "VALUES (@SupplierID, @OrderDate, @Status, @TotalAmount, @ShippingFee)";

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierID", supplierId);
                    command.Parameters.AddWithValue("@OrderDate", DateTime.Now.Date);
                    command.Parameters.AddWithValue("@Status", "Pending");
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@ShippingFee", shippingFee);

                    return (int)command.ExecuteScalar();
                }
            }
        }

        //Insert values to orderdetails for list of products in the purchase order
        public void InsertOrderDetails(int orderId, ObservableCollection<ProductModel> products)
        {
            string query = "INSERT INTO tbl_PurchaseOrderDetails (OrderID, ProductID, Quantity, UnitPrice) " +
                           "VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice)";

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                foreach (var product in products)
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderID", orderId);
                        command.Parameters.AddWithValue("@ProductID", product.ProductId);
                        command.Parameters.AddWithValue("@Quantity", product.OrderQuantity);
                        command.Parameters.AddWithValue("@UnitPrice", product.UnitCost);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        //Updates the stock quantity of affected product and changes the status of the orders
        public void UpdateStock(ObservableCollection<PurchaseOrderDetailModel> orders, int OrderId)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Update stock quantities for each product
                foreach (var item in orders)
                {
                    using (SqlCommand command = new SqlCommand("UPDATE tbl_products SET stock_quantity = stock_quantity + @Quantity WHERE product_id = @product_id", connection))
                    {
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@product_id", item.ProductID);

                        command.ExecuteNonQuery();
                    }
                }

                // Updates the order status
                using (SqlCommand updateOrderCommand = new SqlCommand("UPDATE tbl_purchaseorders SET Status = 'Completed' WHERE OrderID = @OrderID", connection))
                {
                    updateOrderCommand.Parameters.AddWithValue("@OrderID", OrderId);
                    updateOrderCommand.ExecuteNonQuery();
                }
            }

        }

    }
}
