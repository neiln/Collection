using System;

namespace Solid_concepts
{
    internal class Program
    {
        //Requirement:
        //Establish a connection to db server, print connection status
        //connection failures should be written to a log file or application windows event log
        //add log format specifier

        //Interface Segregation principle: clients should not force to implement unnecessary methods
        private static void Main(string[] args)
        {
            var da = new DataAccess();
            Console.WriteLine(da.Connect() ? "Connection Successful!" : "Connection Failure!");
        }
    }
}