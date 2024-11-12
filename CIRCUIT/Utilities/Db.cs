using System;
using System.Data.SqlClient;


namespace CIRCUIT.Utilities
{
    internal class Db
    {
        private string connectionString = "Server=LAPTOP-DK8TN1UP\\SQLEXPRESS01;Integrated Security=True;";

        public bool ConnectToDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    Console.WriteLine("Connection successful!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                return false;
            }
        }
    }
}
