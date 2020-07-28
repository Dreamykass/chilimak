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
            Normal,
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
            ProcessArgs(args);
            ProcessConfig();
        }

        private void ProcessArgs(string[] args)
        {
            if (args.Length != 3) throw new Exception("Config.Config(), args.Length != 3");

            var verbosityInt = int.Parse(args[0]);
            if (verbosityInt == 0) m_verbosity = VerbosityLevel.Silent;
            else if (verbosityInt == 0) m_verbosity = VerbosityLevel.Silent;
            else if (verbosityInt == 0) m_verbosity = VerbosityLevel.Silent;
            else throw new Exception("Config.ProcessArgs(), args.Length != 3");

            var workingPath = args[1];
            var chilimakPath = args[2];

            if (!Directory.Exists(workingPath))
                throw new Exception("Config.ProcessArgs(),");
            if (!Directory.Exists(chilimakPath))
                throw new Exception("Config.ProcessArgs(),");
        }

        private void ProcessConfig()
        {
            var currentDir = System.IO.Directory.GetCurrentDirectory();
            if (currentDir == "p01-pretokenizer") ;

        }

    }
}
