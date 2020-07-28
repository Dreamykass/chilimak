using System;
using System.IO;
using Tomlyn;

namespace Pretokenizer
{
    class Config
    {
        public enum VerbosityLevel
        {
            Silent,
            Basic,
            Full,
        }

        private VerbosityLevel m_verbosity;
        public VerbosityLevel Verbosity { get { return m_verbosity; } }

        private String m_workingPath;
        public String WorkingPath { get { return m_workingPath; } }

        private String m_chilimakPath;
        public String ChilimakPath { get { return m_chilimakPath; } }

        public Config(string[] args)
        {
            if (args.Length == 0)
                InitFromConfig();
            else
                InitFromArgs(args);
        }

        private void InitFromArgs(string[] args)
        {
            if (args.Length != 3) throw new Exception("Config.Config(), args.Length != 3");

            var verbosityInt = int.Parse(args[0]);
            if (verbosityInt == 0) m_verbosity = VerbosityLevel.Silent;
            else if (verbosityInt == 0) m_verbosity = VerbosityLevel.Silent;
            else if (verbosityInt == 0) m_verbosity = VerbosityLevel.Silent;
            else throw new Exception("Config.Config(), args.Length != 3");

            var workingPath = args[1];
            var chilimakPath = args[2];
        }

        private void InitFromConfig()
        {
            var currentDir = System.IO.Directory.GetCurrentDirectory();
            if (currentDir == "p01-pretokenizer") ;

        }

    }
}
