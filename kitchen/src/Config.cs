using System;
using System.IO;
using System.Collections.Generic;
using NLog;
using CommandLine;

namespace Kitchen
{
    class Config
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public String projectPath;
        public String chilimakRoot;
        public List<PipelineStep> pipelineSteps;

        public void FindChilimakRoot()
        {
            Logger.Info("Searching for chilimak root.");
            Logger.Info("Current directory: {0}.", Directory.GetCurrentDirectory());

            Logger.Info("Going up the tree from current directory.");
            {
                var currentDir = Directory.GetCurrentDirectory();
                while (Directory.Exists(currentDir))
                {
                    currentDir = currentDir + "/..";
                    if (File.Exists(currentDir + "/chilimak_dir_beacon"))
                    {
                        Logger.Info("Root found by going up the tree.");
                        chilimakRoot = currentDir + "/";
                        return;
                    }
                }
            }
            Logger.Warn("Couldn't find root by going up the tree.");

            Logger.Info("Checking hardcoded paths.");
            {
                String hardcoded = Directory.GetCurrentDirectory() + "/../";

                if (File.Exists(hardcoded + "chilimak_dir_beacon"))
                {
                    Logger.Info("Hardcoded path to root is OK.");
                    chilimakRoot = hardcoded;
                    return;
                }
            }
            Logger.Warn("Hardcoded path either doesn't exist or isn't root.");
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

            Parser.Default.ParseArguments<CmdOptions>(args)
            .WithParsed<CmdOptions>(o =>
            {
                if (o.ProjectDir != null)
                {
                    projectPath = o.ProjectDir + "/";
                    Logger.Info("Received option 'proj': " + projectPath);

                    if (!Directory.Exists(projectPath))
                        Terminate.Now(101);

                    if (!File.Exists(projectPath + "/kitchen-recipe.lua"))
                        Terminate.Now(102);
                }
            });

        }

        public void LoadPipelineSteps()
        {
            try
            {
                pipelineSteps = new List<PipelineStep>();

                var scriptStr = File.ReadAllText(chilimakRoot + "/config/kitchen-pipeline.lua");

                var luaState = new NLua.Lua();
                luaState.DoString(scriptStr);

                var tablePipelineSteps = luaState.GetTable("PipelineSteps");

                foreach (KeyValuePair<object, object> itemKVP in tablePipelineSteps)
                {
                    var step = new PipelineStep();

                    var stepInTable = itemKVP.Value as NLua.LuaTable;

                    step.Name = stepInTable["Name"] as String;
                    Logger.Info("Step name: " + step.Name + ". Possible paths:");

                    var pathsTable = stepInTable["PossiblePaths"] as NLua.LuaTable;
                    foreach (KeyValuePair<object, object> pathKVP in pathsTable)
                    {
                        var pathStr = pathKVP.Value as String;
                        Logger.Info("    " + pathStr);
                        step.PossiblePaths.Add(pathStr);
                    }

                    pipelineSteps.Add(step);
                }

            }
            catch (Exception exc)
            {
                Logger.Fatal(exc);
                Terminate.Now(103);
            }

        }

        public void ProcessConfig()
        {

        }

    }
}
