using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts.DIP
{
    /// <summary>
    /// This class fixes Dependency Inversion Principle 
    /// Concrete dependency of FileLogger, EventLogger provided by the parent during ctor time.
    /// </summary>
    public class DataAccessFix
    {
        private readonly DbConnection _connection;
        private readonly ILogger _logger;

        public DataAccessFix(DbConnection connection, ILogger logger)
        {
            _connection = connection;
            _logger = logger;
        }


        public bool Connect()
        {

            try
            {
                var connectionString = _connection.ConnectionString;

                using (IDbConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.Log(ex.ToString());
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

    public interface ILogger
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
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry("Log message example", EventLogEntryType.Information, 101, 1);
            }
        }
    }

    public sealed class DbConnection
    {
        public string ConnectionString { get; set; }
    }

}