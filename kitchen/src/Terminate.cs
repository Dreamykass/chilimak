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
            Logger.Debug("Kitchen.Terminate.SetChilimakRoot(\"{0}\")", path);
            ChilimakRoot = path;

            if (!Directory.Exists(ChilimakRoot))
            {
                int rcode = 4;
                Logger.Fatal("Terminate.SetChilimakRoot(\"{0}\") called.", path);
                Logger.Fatal("Exiting with code {0:D3}: {1}", 4, "Given Chilimak root isn't valid.");
                DefinitelyExit(rcode);
            }

            if (!File.Exists(ChilimakRoot + "/chilimak_dir_beacon"))
            {
                int rcode = 5;
                Logger.Fatal("Terminate.SetChilimakRoot(\"{0}\") called.", path);
                Logger.Fatal("Exiting with code {0:D3}: {1}", 5, "Given Chilimak root is valid, but doesn't contain chilimak_dir_beacon.");
                DefinitelyExit(rcode);
            }
        }

        private static String GetEcodeString(int rcode)
        {
            SetChilimakRoot(ChilimakRoot);
            String rcodeString = "";

            try
            {
                var toml_text = File.ReadAllText(ChilimakRoot + "config/return-codes.toml");

                var toml_doc = Toml.Parse(toml_text);
                var toml_table = toml_doc.ToModel();
                rcodeString = (String)toml_table[rcode.ToString()];
            }
            catch (Exception e)
            {
                rcode = 6;
                Logger.Fatal("Terminate.Now(rcode) called, encountered an error.");
                Logger.Fatal("Exiting with code {0:D3}: {1}", rcode, "Error or exception when reading or parsing return-codes.toml.");
                Logger.Fatal("Exception message: " + e.Message);
                DefinitelyExit(rcode);
            }

            if (rcodeString == null || rcodeString == "")
            {
                rcode = 8;
                Logger.Fatal("Terminate.Now(rcode) called, encountered an error.");
                Logger.Fatal("Exiting with code {0:D3}: {1}", rcode, "Error when parsing return-codes.toml - got an invalid or empty string.");
                DefinitelyExit(rcode);
            }

            return rcodeString;
        }

        public static void Now()
        {
            int rcode = 2;
            Logger.Fatal("Terminate.Now() called.");
            Logger.Fatal("Exiting with code {0:D3}: {1}", rcode, "Terminated without specifying an rcode.");
            DefinitelyExit(rcode);
        }

        public static void Now(int rcode)
        {
            Logger.Fatal("Terminate.Now({0}) called.", rcode);
            Logger.Fatal("Exiting with code {0:D3}: {1}", rcode, GetEcodeString(rcode));
            DefinitelyExit(rcode);
        }

        private static void DefinitelyExit(int rcode)
        {
            Environment.Exit(rcode);
        }
    }
}
