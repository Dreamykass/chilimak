using System;
using System.IO;
using NLog;
using Tomlyn;

namespace Kitchen
{
    class Terminate
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private static String ChilimakRoot = "";

        public static void SetChilimakRoot(String path)
        {
            ChilimakRoot = path;

            if (!Directory.Exists(ChilimakRoot))
            {
                Logger.Fatal("Terminate.SetChilimakRoot(String path) called.");
                Logger.Fatal("Exiting with code {0:D3}: {1}", 4, "Given Chilimak directory isn't valid.");
                Environment.Exit(4);
            }

            if (!File.Exists(ChilimakRoot + "/chilimak_dir_beacon"))
            {
                Logger.Fatal("Terminate.SetChilimakRoot(String path) called.");
                Logger.Fatal("Exiting with code {0:D3}: {1}", 5, "Given Chilimak directory is valid, but doesn't contain chilimak_dir_beacon.");
                Environment.Exit(5);
            }
        }

        private static String GetEcodeString(int rcode)
        {
            SetChilimakRoot(ChilimakRoot);
            String rcodeString = "";

            try
            {
                var tomlDoc = Toml.Parse(ChilimakRoot + "/config/return-codes.toml");
                var tomlTable = tomlDoc.ToModel();
                rcodeString = (String)tomlTable[rcode.ToString()];
            }
            catch (Exception)
            {
                Logger.Fatal("Terminate.Now(rcode) called, encountered an error.");
                Logger.Fatal("Exiting with code {0:D3}: {1}", 6, "Error or exception when reading or parsing return-codes.toml.");
                Environment.Exit(6);
            }

            if (rcodeString == null || rcodeString == "")
            {
                Logger.Fatal("Terminate.Now(rcode) called, encountered an error.");
                Logger.Fatal("Exiting with code {0:D3}: {1}", 6, "Error or exception when reading or parsing return-codes.toml.");
                Environment.Exit(6);
            }

            return rcodeString;
        }

        public static void Now()
        {
            Logger.Fatal("Terminate.Now() called.");
            Logger.Fatal("Exiting with code {0:D3}: {1}", 2, "Terminated without specifying an rcode.");
            Environment.Exit(2);
        }

        public static void Now(int rcode)
        {
            Logger.Fatal("Terminate.Now(rcode) called.");
            Logger.Fatal("Exiting with code {0:D3}: {1}", 2, GetEcodeString(rcode));
            Environment.Exit(rcode);
        }
    }
}
