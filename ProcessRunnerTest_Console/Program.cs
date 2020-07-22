using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessRunnerTest_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Gets an output from another console exe and routes it to this app
            string exeFilePath = ConfigurationManager.AppSettings["exeFile"];
            string arguments = ConfigurationManager.AppSettings["args"];

            using (Process process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exeFilePath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            })
            {
                process.OutputDataReceived += (s, e) =>
                {
                    Console.WriteLine(e.Data);
                };

                process.Start();
                process.BeginOutputReadLine();
                process.WaitForExit();
            }

        }
    }
}
