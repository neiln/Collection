using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Solid_concepts.SRP
{
    /// <summary>
    /// This class fixes Single Responsibility Principle violation
    /// The log write output work is separated out as another class
    /// </summary>
    public class DataAccessFix
    {
        public bool Connect()
        {

            var fileLogger = new FileLogger();

            var connectionString = "Data Source=(local);Initial Catalog=Absorb-Global;Integrated Security=SSPI;";
            try
            {
                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                fileLogger.Log(ex.ToString());
            }

            return false;
        }
    }


    internal class FileLogger
    {
        public void Log(string error)
        {
            File.WriteAllText(@"c:\temp\logs.txt", error);
        }
    }
}