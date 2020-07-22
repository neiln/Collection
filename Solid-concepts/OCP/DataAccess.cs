using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts
{
    /// <summary>
    /// This class violates Open/Close Principle 
    /// To extend the file logger types, FileLogger class has to be modified.
    /// </summary>
    public class DataAccess
    {
        public bool Connect()
        {

            var fileLogger = new FileLogger(LogTypes.File);

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

    internal enum LogTypes
    {
        File, EventLog
    }

    internal class FileLogger
    {
        private readonly LogTypes _logType;

        public FileLogger(LogTypes whereToLog)
        {
            _logType = whereToLog;
        }

        public void Log(string error)
        {
            switch (_logType)
            {
                case LogTypes.File:
                    File.WriteAllText(@"c:\temp\logs.txt", error);
                    break;
                case LogTypes.EventLog:
                    using (EventLog eventLog = new EventLog("Application"))
                    {
                        eventLog.Source = "Application";
                        eventLog.WriteEntry("Log message example", EventLogEntryType.Information, 101, 1);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}