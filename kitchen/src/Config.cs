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

        public Config()
        {

        }

        public void FindChilimakRoot()
        {
            Logger.Info("Searching for chilimak root.");

            Logger.Info("Checking hardcoded path.");
            {
                String hardcoded = Directory.GetCurrentDirectory() + "/../";
                // Logger.Fatal(hardcoded);

                if (File.Exists(hardcoded + "chilimak_dir_beacon"))
                {
                    Logger.Info("Hardcoded path to root is OK.");
                    chilimakRoot = hardcoded;
                    return;
                }
            }
            Logger.Warn("Hardcoded path either doesn't exist or isn't root.");

            Logger.Info("Going up the tree from current directory.");
            {
                var currentDir = Directory.GetCurrentDirectory();
                while (Directory.Exists(currentDir))
                {
                    currentDir = currentDir + "/..";
                }
            }
            Logger.Warn("Couldn't find root by going up the tree.");
        }

        public void PrintUsage()
        {
            Logger.Info("Running the kitchen.");
            Logger.Warn("Received no args, so printing usage:");
            Logger.Info("---kitchen.exe <chilimakRoot> <workingPath>");
            Logger.Info("Exiting the kitchen.");
            Terminate.Now(100);
        }

        public void ProcessArgs(string[] args)
        {
            if (args.Length <= 0)
                PrintUsage();

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

        public void ProcessConfig()
        {
            var currentDir = System.IO.Directory.GetCurrentDirectory();
            // if (currentDir == "p01-pretokenizer") ;

        }

    }
}
