using System;
using System.Data.SqlClient;
using CIRCUIT.Model;
using Microsoft.Data.SqlClient;

namespace CIRCUIT.Utilities
{
    internal class Db
    {

        //Comment each of our local connection for local use
        //private string connectionString = "Server=LAPTOP-DK8TN1UP\\SQLEXPRESS01;Integrated Security=True;";
        private string connectionString = "Data Source=localhost;Initial Catalog = Pos_db; Persist Security Info=True;User ID = carl; Password=carlAmbatunut;" +
                                           "Trust Server Certificate=True";

        //Method to execute non queries like INSERT or UPDATE, might this code later idk
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
                                    Brand = reader.GetString(reader.GetOrdinal("brand")),
                                    ModelNumber = reader.GetString(reader.GetOrdinal("model_number")),
                                    StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                                    UnitCost = (double)reader.GetDecimal(reader.GetOrdinal("unit_cost")),
                                    SellingPrice = (double)reader.GetDecimal(reader.GetOrdinal("selling_price"))
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

    }
}
