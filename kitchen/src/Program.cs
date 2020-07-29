using System;
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
                config.ProcessConfig();

                Logger.Info("Config is ok. Verbosity level: ");
                Logger.Debug("chilimak path: [{0}]; working path: [{1}]", config.chilimakRoot, config.workingPath);

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
