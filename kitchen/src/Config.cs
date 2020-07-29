using System;
using System.IO;
using NLog;
using CommandLine;

namespace Kitchen
{
    class Config
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public String projectPath;
        public String chilimakRoot;

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

        private class CmdOptions
        {
            [Option('p', "proj", Required = true, HelpText = "Project path to work on, that contains a 'kitchen-recipe.toml'.")]
            public String ProjectDir { get; set; }
        }

        public void ProcessArgs(string[] args)
        {
            if (args.Length <= 0)
                PrintUsage();


        }

        public void ProcessConfig()
        {

        }

    }
}
