using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Solid_concepts
{
    public class Application : IApplication
    {
        private readonly IAppConfig _appConfig;
        private readonly DataAccess _dataAccess;

        public Application(IAppConfig appConfig, DataAccess dataAccess)
        {
            _appConfig = appConfig;
            _dataAccess = dataAccess;
        }

        public void Run()
        {
            Console.WriteLine(_appConfig.IsInDev?"In Dev":"In Prod");
            Console.WriteLine(_dataAccess.Connect() ? "Connection Successful!" : "Connection Failure.");
        }

    }
}
