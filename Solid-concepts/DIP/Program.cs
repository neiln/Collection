using System;
using System.Configuration;
using System.Net.Mime;


namespace Solid_concepts.DIP
{
    internal class Program
    {
        //Requirement:
        //Establish a connection to db server, print connection status
        //connection failures should be written to a log file or application windows event log
        //add log format specifier
        //in production logs should be sent to windows event logs
        //in development logs should be in a log file

        //Dependency Inversion Principle: Object must depend on abstraction not on concretion
        private static void Main(string[] args)
        {

            ILogger logger;
            DbConnection dbConnection = new DbConnection();


            bool isInDev = String.Compare(ConfigurationManager.AppSettings["ReleaseMode"],"Development",StringComparison.OrdinalIgnoreCase)==0;

            if (isInDev)
            {
                logger = new FileLogger();
                dbConnection.ConnectionString = ConfigurationManager.AppSettings["DevServer"];
            }
            else
            {
                logger = new EventLogger();
                dbConnection.ConnectionString = ConfigurationManager.AppSettings["ProdServer"];
            }


            var da = new DataAccessFix(dbConnection, logger);

            Console.WriteLine(da.Connect() ? "Connection Successful!" : "Connection Failure!");
        }
    }
}