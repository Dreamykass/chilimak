using System;
using NLog;

namespace Kitchen
{
    class Program
    {
        static int Main(string[] args)
        {
            SetUpNLog.Now();
            NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

            try
            {
                Logger.Info("Starting up the Kitchen.");
                Config config = new Config(args);

                Terminate.SetChilimakRoot(config.chilimakRoot);

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
