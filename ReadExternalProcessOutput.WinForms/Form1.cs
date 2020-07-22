using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Medallion.Shell;
using Timer = System.Timers.Timer;

namespace ReadExternalProcessOutput.WinForms
{
    public partial class Form1 : Form
    {
        private ObservableCollection<string> outputLines = new ObservableCollection<string>();

        public Form1()
        {
            InitializeComponent();
            this.txtOutput.ScrollBars = ScrollBars.Vertical;

        }


        private async void btnRun_Click(object sender, EventArgs e)
        {

            string exe = ConfigurationManager.AppSettings["exeFile"];
            string arg = ConfigurationManager.AppSettings["args"];

            this.txtOutput.Text = "";

            this.btnRun.Enabled = false;
            var progressIndicator = new Progress<string>(ReportProgress);
            await RunProcessAsync(exe, arg, progressIndicator);
            //await ProcessExe(progressIndicator);

            this.btnRun.Enabled = true;
        }

        void ReportProgress(string str)
        {
            txtOutput.AppendText($@"{str}{Environment.NewLine}");
            // txtOutput.ScrollToCaret();
        }

        async Task RunProcess(string executable, string arguments, IProgress<string> progress)
        {

            var command = Command.Run(executable, arguments);

            await Task.Run(() =>
            {
                while (true)
                {
                    //Task.Delay(500);
                    var line = command.StandardOutput.ReadLine();

                    if (line == null)
                        return;
                    progress.Report(line);

                }
            });

        }


        async Task RunProcessAsync(string exe, string arg, IProgress<string> progress)
        {

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = exe,
                    Arguments = arg,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,

                    CreateNoWindow = true
                },
                EnableRaisingEvents = true
            };

            process.Exited += (s, e) =>
            {
                process.OutputDataReceived -= null;
            };

            process.OutputDataReceived += (s, ev) =>
            {
                if (ev.Data == null)
                {
                    process.Dispose();
                    return;
                }

                progress.Report(ev.Data);
            };

            await Task.Run(() =>
                {

                    process.Start();

                    process.BeginOutputReadLine();
                   // process.BeginErrorReadLine();

                    process.WaitForExit();
                });

        }


    }
}



