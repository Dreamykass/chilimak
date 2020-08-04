using System;
using System.IO;
using System.Collections.Generic;
using NLog;
using MoonSharp.Interpreter;

namespace Lexer
{
    public class FileOfWords
    {
        public String name = "";

        public List<Word> tokens = new List<Word>();
    }
}