using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Solid_concepts
{
    public class AppConfig : IAppConfig
    {
        public ILogger Logger { get; }
        public string ConnectionString { get; }

        public AppConfig(IFileLogger fileLogger, IEventLogger eventLogger)
        {
            
            IsInDev = String.Compare(ConfigurationManager.AppSettings["ReleaseMode"],"Development",StringComparison.OrdinalIgnoreCase)==0;

            if (IsInDev)
            {
                Logger = fileLogger;
                ConnectionString = ConfigurationManager.AppSettings["DevServer"];
            }
            else
            {
                Logger = eventLogger;
                ConnectionString = ConfigurationManager.AppSettings["ProdServer"];
            }
        }

        public bool IsInDev { get; set; }
    }

    public interface IAppConfig
    {
        public bool IsInDev { get; set; }
        public ILogger Logger { get; }

        public string ConnectionString { get; }
    }
}
