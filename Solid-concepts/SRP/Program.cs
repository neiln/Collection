using System;
using Solid_concepts.SRP;

namespace Solid_concepts
{

    //Requirement:
    //Establish a connection to db server, print connection status
    //connection failures should be written to a log file

    //Single Responsibility: a class should have only a single responsibility
    internal class Program
    {
        private static void Main(string[] args)
        {
            //SRP Violation
            //var da = new DataAccess();
            //Console.WriteLine(da.Connect() ? "Connection Successful!" : "Connection Failure!");

            //SRP Fix
            var da = new DataAccessFix();
            Console.WriteLine(da.Connect() ? "Connection Successful!" : "Connection Failure!");

        }
    }
}