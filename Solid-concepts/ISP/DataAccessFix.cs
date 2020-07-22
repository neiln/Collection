using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts.ISP
{
    /// <summary>
    /// This class fixes Interface segregation Principle 
    /// Adding Format property into a separate interface
    /// </summary>

    public class DataAccessFix
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


    interface ILogFormatter
    {
        public string Format { get; set; }
    }

    interface IFileCsvLogger
    {
        public string FilePath { get; }
    }

    interface ILogger
    {

        public void Log(string error);
    }

    class FileLogger : ILogger, IFileCsvLogger, ILogFormatter
    {

        public string FilePath { get; } = @"c:\temp\logs.csv";

        public void Log(string error)
        {
            File.WriteAllText(FilePath, error);
        }

        public string Format { get; set; } = "Log_yyyy-MM-dd";
    }

    class EventLogger : ILogger
    {
        public void Log(string error)
        {
            throw new NotImplementedException();
        }
    }

}