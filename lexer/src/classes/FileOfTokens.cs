using System;
using System.IO;
using System.Collections.Generic;
using NLog;
using MoonSharp.Interpreter;

namespace Lexer
{
    public class FileOfTokens
    {
        public String name = "";

        public List<Token> tokens = new List<Token>();
    }
}