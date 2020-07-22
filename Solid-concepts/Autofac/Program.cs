using System;
using System.Configuration;
using System.Net.Mime;
using Autofac;


namespace Solid_concepts
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

            var container = Bootstrapper.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}