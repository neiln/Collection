using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts
{
    public class DataAccess
    {
        private readonly IAppConfig _appConfig;
        private readonly ILogger _logger;

        public DataAccess(IAppConfig appConfig)
        {
            _appConfig = appConfig;
            _logger = appConfig.Logger;
        }


        public bool Connect()
        {

            try
            {
                var connectionString = _appConfig.ConnectionString;

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

    public interface IEventLogger: ILogger
    {
    }

    public interface IFileLogger: ILogger
    {
    }

    public class FileLogger : IFileLogger, IFileCsvLogger, ILogFormatter
    {

        public string FilePath { get; } = @"c:\temp\logs.csv";


        public void Log(string error)
        {
            File.WriteAllText(FilePath, error);
        }

        public string Format { get; set; } = "Log_yyyy-MM-dd";
    }

    public class EventLogger : IEventLogger
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