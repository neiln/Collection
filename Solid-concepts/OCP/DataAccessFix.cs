using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts.OCP
{
    /// <summary>
    /// This class fixes Open/Close Principle 
    /// File logger types can be extended with new type from inheriting base type.
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


    abstract class LogBase
    {
        public virtual void Log(string error)
        { }
    }
    

    class FileLogger : LogBase
    {
        public override void Log(string error)
        {
            File.WriteAllText(@"c:\temp\logs.txt", error);
        }
    }

    class EventLogger : LogBase
    {
        public override void Log(string error)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Application";
                eventLog.WriteEntry("Log message example", EventLogEntryType.Information, 101, 1);
            }
        }
    }

}