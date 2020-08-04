using System;
using System.Diagnostics;
using NLog;

namespace Kitchen
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        static int Main(string[] args)
        {
            try
            {
                SetUpNLog.Now();
                Logger.Info("Starting up the Kitchen.");

                Config config = new Config();
                config.FindChilimakRoot();
                Terminate.SetChilimakRoot(config.chilimakRoot);

                config.ProcessArgs(args);
                config.LoadPipelineSteps();

                config.ProcessConfig();

                foreach (var step in config.pipelineSteps)
                {
                    var proc = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            Arguments = "-p \"" + config.projectPath + "\" -c \"" + config.chilimakRoot + "\"",
                            FileName = step.GetValidPath(),
                            UseShellExecute = false,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true
                            // UseShellExecute = true,
                            // CreateNoWindow = false,
                            // WindowStyle = ProcessWindowStyle.Normal,
                            // FileName = step.GetValidPath(),
                        }
                    };

                    proc.Start();

                    while (!proc.StandardOutput.EndOfStream)
                    {
                        string line = proc.StandardOutput.ReadLine();
                        // do something with line
                        Console.WriteLine(line);
                    }

                    if (proc.ExitCode == 0)
                    {
                        Logger.Warn("Step '{0}' finished OK.", step.Name);
                    }
                    else
                    {
                        Logger.Fatal("Step '{0}' failed with {1}.", step.Name, proc.ExitCode);
                        Terminate.Now(proc.ExitCode);
                    }
                }

                Logger.Info("Config is ok.");
                Logger.Info("Chilimak root: [{0}]; Working path: [{1}]", config.chilimakRoot, config.projectPath);

            }
            catch (Exception exc)
            {
                Logger.Fatal("--- ---\n Exception! Message: " + exc.Message);
                Logger.Fatal("Kitchen finishing with code 1.");
                return 1;
            }

            Logger.Info("Kitchen finishing normally with code 0.");
            return 0;
        }
    }
}
