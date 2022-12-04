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
            if (!File.Exists(FILE_PATH)) return;
            string[] lines = File.ReadAllLines(FILE_PATH);

            // 2. Perform Lexical Analysis
            List<Token> tokens = Lexer.LexLines(lines);
            // Console.WriteLine($"Lexed ({tokens.Count}) tokens");

            // 3. Parse Iteratively using 'Formatter'
            Formatter.ParseToFile("output.carlo", tokens, true);

            // 4. Create AST with 'Parser'
            Parser.Parse(tokens);
        }
    }
}