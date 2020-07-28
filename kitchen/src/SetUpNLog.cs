using System;
using System.IO;
using NLog;

namespace Kitchen
{
    class SetUpNLog
    {
        public static void Now()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var logconsole = new NLog.Targets.ColoredConsoleTarget("logconsole");

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);

            logconsole.Layout = "${longdate}|${level:uppercase=true}|${logger}|---|${message}";

            NLog.LogManager.Configuration = config;

        }
    }
}
