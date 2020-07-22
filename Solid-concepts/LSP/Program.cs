using System;
using Solid_concepts.LSP;

namespace Solid_concepts
{
    internal class Program
    {
        //Requirement:
        //Establish a connection to db server, print connection status
        //connection failures should be written to a log file or application windows event log
        
        //Liskov Substitution: subtypes should be able to substitute base types
        private static void Main(string[] args)
        {
            var da = new DataAccess();
            Console.WriteLine(da.Connect() ? "Connection Successful!" : "Connection Failure!");
        }
    }
}