using System;
using System.IO;
using Tomlyn;
using NLog;

namespace Kitchen
{
    class Config
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public String workingPath;
        public String chilimakRoot;

        public Config(string[] args)
        {
            if (args.Length <= 0)
            {
                PrintUsage();
                throw new Exception("Launched with no commandline args.");
            }

            ProcessArgs(args);
            ProcessConfig();
        }

        private void PrintUsage()
        {
            Logger.Info("Running the kitchen.");
            Logger.Warn("Received no args, so printing usage:");
            Logger.Info("---kitchen.exe <chilimakRoot> <workingPath>");
            Logger.Info("Exiting the kitchen.");
            Terminate.Now(100);
        }

        private void ProcessArgs(string[] args)
        {
            if (args.Length != 2) throw new Exception("Config.ProcessArgs(), args.Length != 2, ==" + args.Length.ToString());

            var verbosityInt = int.Parse(args[0]);

            var workingPath = args[1];
            if (!Directory.Exists(workingPath))
                throw new Exception("Config.ProcessArgs(), !Directory.Exists(" + workingPath + ")");

            var currentDir = System.IO.Directory.GetCurrentDirectory();
            chilimakRoot = currentDir + "/../";
            if (!Directory.Exists(chilimakRoot))
                throw new Exception("Config.ProcessArgs(), !Directory.Exists(" + chilimakRoot + ")");
        }

        private void ProcessConfig()
        {
            var currentDir = System.IO.Directory.GetCurrentDirectory();
            // if (currentDir == "p01-pretokenizer") ;

        }

    }
}
