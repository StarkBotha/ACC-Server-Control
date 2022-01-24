using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace api
{
    public class ProcessHost : IProcessHost
    {
        private readonly ILogger<ProcessHost> logger;
        public IConfiguration Configuration { get; }

        private Process process { get; set; }

        public ProcessHost(ILogger<ProcessHost> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.Configuration = configuration;
        }

        public void StartServer()
        {
            var procs = Process.GetProcessesByName("accServer");
            foreach (var proc in procs)
            {
                try
                {
                    proc.Kill();
                    proc.WaitForExit();
                }
                catch (System.Exception ex)
                {
                    logger.LogCritical(ex.ToString());
                }
            }

            string processName = Configuration["AppSettings:ProcessPath"];
            string processPath = Configuration["Appsettings:ProcessPath"];

            if (process != null)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch (System.Exception ex)
                {
                    logger.LogCritical(ex.ToString());
                }
            }

            try
            {
                process = new Process();
                process.StartInfo.FileName = $"{processPath}accserver.exe";
                process.StartInfo.WorkingDirectory = processPath;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.OutputDataReceived += (sender, data) => logger.LogInformation(data.Data);
                process.ErrorDataReceived += (sender, data) => logger.LogError(data.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (System.Exception ex)
            {
                logger.LogCritical(ex.ToString());
            }
        }

        public void StopServer()
        {
            if (process != null)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit();
                }
                catch (System.Exception ex)
                {
                    logger.LogCritical(ex.ToString());
                }
            }

            var procs = Process.GetProcessesByName("accServer");
            foreach (var proc in procs)
            {
                try
                {
                    proc.Kill();
                    proc.WaitForExit();
                }
                catch (System.Exception ex)
                {
                    logger.LogCritical(ex.ToString());
                }
            }
        }

        public string GetProcesses()
        {
            string output = string.Empty;
            var procs = Process.GetProcessesByName("accServer");
            //var procs = Process.GetProcesses();
            foreach (var proc in procs)
            {
                output += $"[{proc.ProcessName}]";
            }
            return output;
        }

        public bool IsServerRunning()
        {
            var procs = Process.GetProcessesByName("accServer");
            return procs.Length > 0;
        }        

        public string GetServerStatus()
        {
            var procs = Process.GetProcessesByName("accServer");
            return procs.Length > 0 ? "Online" : "Offline";
        }        

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    //dispose managed state
                    StopServer();
                }


                var procs = Process.GetProcessesByName("accServer");
                foreach (var proc in procs)
                {
                    try
                    {
                        proc.Kill();
                        proc.WaitForExit();
                    }
                    catch (System.Exception ex)
                    {
                        logger.LogCritical(ex.ToString());
                    }
                }
            }
        }
    }
}