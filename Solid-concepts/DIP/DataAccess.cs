using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts
{
    /// <summary>
    /// This class violates Dependency Inversion Principle 
    /// Concrete dependency of FileLogger, EventLogger are created, should be provided by the parent
    /// </summary>
    public abstract class DataAccess
    {
        public abstract bool IsDevelopment { set; get; }

        public bool Connect()
        {
            ILogger logger;

            if (IsDevelopment)
            {
                logger = new FileLogger();
            }
            else //in prod
            {
                logger = new EventLogger();
            }


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
                logger.Log(ex.ToString());
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