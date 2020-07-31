using System;
using System.IO;
using System.Collections.Generic;

namespace Kitchen
{
    class PipelineStep
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public String Name { get; set; }
        public List<String> PossiblePaths { get; set; }


        public PipelineStep()
        {
            PossiblePaths = new List<String>();
        }

        public String GetValidPath()
        {
            foreach (var path in PossiblePaths)
                if (File.Exists(path))
                    return path;


            Logger.Fatal("For step name {0}:", Name);
            Terminate.Now(104);
            return "";
        }
    }
}
