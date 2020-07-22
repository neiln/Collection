using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Solid_concepts
{
    /// <summary>
    /// This class violates Single Responsibility Principle 
    /// This class has 2 responsibilities., connecting to server and logging write output
    /// </summary>
    public class DataAccess
    {
        public bool Connect()
        {
            try
            {
                var connectionString = "Data Source=(local);Initial Catalog=Absorb-Global;Integrated Security=SSPI;";

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"c:\temp\logs.txt", ex.ToString());
            }

            return false;
        }
    }
}