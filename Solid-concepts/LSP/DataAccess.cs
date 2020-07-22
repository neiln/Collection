using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace Solid_concepts
{
    /// <summary>
    /// This class violates Liskov Substitution Principle 
    /// Init method is forced in the base class
    /// derived types must implement all virtual methods from base class.
    /// </summary>
    public class DataAccess
    {
        public bool Connect()
        {

            LogBase eventLogger = new EventLogger();
            eventLogger.Init(); //this will throw exception

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
                eventLogger.Log(ex.ToString());
            }

            return false;
        }
    }


    internal abstract class LogBase
    {
        public abstract void Init();
        public abstract void Log(string error);
    }
    

    internal class FileLogger : LogBase
    {
        private string filename;
        public override void Init()
        {
            //initialise default file name
            filename = @"c:\temp\logs.txt";
        }

        public override void Log(string error)
        {
            File.WriteAllText(filename, error);
        }
    }

    internal class EventLogger : LogBase
    {
        public override void Init()
        {
            throw new NotImplementedException(); //forced because of base
        }

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