using System;
using Solid_concepts.OCP;

namespace Solid_concepts
{
    internal class Program
    {
        //Requirement:
        //Establish a connection to db server, print connection status
        //connection failures should be written to a log file or application windows event log
        
        //Open/Close: a class should be open for extension but closed for modification
        private static void Main(string[] args)
        {
            //OCP Violation
            //var da = new DataAccess();
            //Console.WriteLine(da.Connect() ? "Connection Successful!" : "Connection Failure!");

            //OCP Fix
            var da = new DataAccessFix();
            Console.WriteLine(da.Connect() ? "Connection Successful!" : "Connection Failure!");

        }
    }
}