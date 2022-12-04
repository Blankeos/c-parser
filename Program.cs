using System;
using System.IO;
using System.Collections.Generic;

namespace CParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("== Parser in C# by Taleon, Elizalde, Rubinos, Tolato ==");

            // 1. Get Input From File
            const string FILE_PATH = "./input.carlo";
            const string FILE_PATH_SAVE = "output.carlo";
            if (!File.Exists(FILE_PATH)) return;
            string[] lines = File.ReadAllLines(FILE_PATH);

            // 2. Perform Lexical Analysis
            List<Token> tokens = Lexer.LexLines(lines);
            // Console.WriteLine($"Lexed ({tokens.Count}) tokens");

            // 3. Parse Iteratively using 'Formatter'
            Formatter.ParseToFile(FILE_PATH_SAVE, tokens, true);
            Console.WriteLine($"Read '{FILE_PATH}' and Output saved to '{FILE_PATH_SAVE}'");

            // 4. Create AST with 'Parser'
            Parser.Parse(tokens);
        }
    }
}