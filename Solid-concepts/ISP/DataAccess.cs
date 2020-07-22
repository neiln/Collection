using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts
{
    /// <summary>
    /// This class violates Interface segregation Principle 
    /// Adding Format property muddies the existing interface
    /// This practice may lead to out grown interface
    /// </summary>
    public class DataAccess
    {
        public bool Connect()
        {

            var fileLogger = new FileLogger();

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
                fileLogger.Log(ex.ToString());
            }

            return false;
        }
    }


    interface IFileCsvLogger
    {
        public string FilePath { get; }
    }

    interface ILogger
    {
        //public string Format { get; set; }
        public void Log(string error);
    }

    class FileLogger : ILogger, IFileCsvLogger
    {

        public string FilePath { get; } = @"c:\temp\logs.csv";

        public void Log(string error)
        {
            File.WriteAllText(FilePath, error);
        }

    }

    class EventLogger : ILogger
    {
        public void Log(string error)
        {
            throw new NotImplementedException();
        }
    }

}