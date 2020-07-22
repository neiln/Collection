using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts.LSP
{
    /// <summary>
    /// This class fixes Liskov Substitution Principle 
    /// FilePath property is separated as an interface
    /// FileLogger substituted with ILogger base - no more forcing the base methods
    /// </summary>
    public class DataAccessFix
    {
        public bool Connect()
        {

            ILogger fileLogger = new FileLogger();


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
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry("Log message example", EventLogEntryType.Information, 101, 1);
            }
        }
    }

}