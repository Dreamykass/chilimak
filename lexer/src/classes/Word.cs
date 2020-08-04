using System;
using System.IO;
using NLog;
using MoonSharp.Interpreter;

namespace Lexer
{
    public class Word
    {
        public String value = "";

        public String file = "";
        public Int32 line = -1;
        public Int32 column = -1;
    }
}