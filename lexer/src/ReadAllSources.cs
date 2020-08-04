using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NLog;
using MoonSharp.Interpreter;

namespace Lexer
{
    class ReadAllSources
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static List<FileOfWords> Now()
        {
            String buildDir = Program.Config.ProjectDir + "/build/";
            Directory.CreateDirectory(buildDir);

            List<FileOfWords> files = new List<FileOfWords>();

            Script state = new Script();
            state.DoFile(Program.Config.ProjectDir + "/kitchen-recipe.lua");

            var sourcesTable = state.Globals["Sources"] as Table;

            foreach (var filenameDynVal in sourcesTable.Values)
            {
                var filenameStr = filenameDynVal.String;

                List<String> rawLines = new List<String>(
                    File.ReadAllLines(Program.Config.ProjectDir + "/" + filenameStr));

                List<String> lines = new List<String>();
                rawLines.ForEach(line => lines.Add(line.Trim()));
                lines.RemoveAll(line => line.StartsWith("//"));

                File.WriteAllLines(buildDir + filenameStr + ".step01", lines);
            }

            return files;
        }
    }
}