using System;

namespace Serilog.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.AppSettings()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs\\logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Starting up");
            Log.Information("Shutting down");

            Log.CloseAndFlush();

            System.Console.ReadLine();
        }


    }
}
