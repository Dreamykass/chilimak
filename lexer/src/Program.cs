using System;
using System.IO;
using NLog;
using CommandLine;
using MoonSharp.Interpreter;

namespace lexer
{
    class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public class CmdOptions
        {
            [Option('p', "proj", Required = true, HelpText = "Project directory to work on, that contains a 'kitchen-recipe.toml'.")]
            public String ProjectDir { get; set; }

            [Option('c', "chilimakroot", Required = true, HelpText = "Chilimak root path, that contains a folder config with config files.")]
            public String ChilimakRoot { get; set; }

            [Option('l', "loggersetting", Required = false, HelpText = "Logger verbosity setting. Default: normal.", Default = "normal")]
            public String LoggerSetting { get; set; }


            public void ProcessArgs(string[] args)
            {
                Parser.Default.ParseArguments<CmdOptions>(args)
                .WithParsed<CmdOptions>(parsedOptions =>
                {
                    ProjectDir = parsedOptions.ProjectDir;
                    ChilimakRoot = parsedOptions.ChilimakRoot;
                    LoggerSetting = parsedOptions.LoggerSetting;


                    Console.WriteLine("siema: " + ProjectDir);

                    if (!Directory.Exists(ProjectDir))
                        Environment.Exit(101);

                    if (!File.Exists(ProjectDir + "/kitchen-recipe.lua"))
                        Environment.Exit(102);

                    {
                        var logConfig = new NLog.Config.LoggingConfiguration();
                        var logConsole = new NLog.Targets.ColoredConsoleTarget("logconsole");

                        var luaState = new Script();
                        luaState.DoFile(ChilimakRoot + "/config/loggers-config.lua");

                        var levelTable = luaState.Globals[System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(LoggerSetting)] as Table;

                        logConfig.AddRule(LogLevel.FromString(levelTable["CSharpMin"] as String),
                                          LogLevel.FromString(levelTable["CSharpMax"] as String),
                                          logConsole);
                        logConsole.Layout = levelTable["CSharpLayout"] as String;

                        NLog.LogManager.Configuration = logConfig;
                    }
                });
            }
        }

        static int Main(string[] args)
        {
            try
            {
                CmdOptions options = new CmdOptions();
                options.ProcessArgs(args);
                Logger.Info("Lexer starts.");
                Console.WriteLine("SIEMA");
            }
            catch (Exception exc)
            {
                // Logger.Fatal(exc, "Exception caught at Parser Main.");
                Console.WriteLine("Exception caught at Parser Main: " + exc.Message);

            }
            Logger.Fatal("Narka.");
            return 0;
        }
    }
}
