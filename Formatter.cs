namespace CParser
{
    class Formatter
    {
        private static readonly TokenType[] TOKENS_WITH_SPACE_AFTER = {
            TokenType.BOOLEAN, TokenType.INT, TokenType.STRING, TokenType.CHAR, TokenType.FLOAT,
            TokenType.DOUBLE, TokenType.IF, TokenType.ELSE, TokenType.COMMA
        };
        public static void ParseToFile(string path, List<Token> tokens, bool logTokens = false)
        {
            int indentCount = 0;
            List<string> lines = new List<string>();
            string currentLine = "";
            TokenStream ts = new TokenStream(tokens.ToArray());

            while (ts.IsEnd())
            {
                // 1. Indents
                var ctoken = ts.Next();
                if (currentLine == "")
                {
                    for (int j = 0; j < indentCount; j++)
                    {
                        currentLine += "\t";
                    }
                }

                // 2. Add token values to line string
                currentLine += ctoken.value;

                // 3. Determine Spaces
                if (TOKENS_WITH_SPACE_AFTER.Any(t => t == ctoken.tokenType)) currentLine += " ";

                if (ctoken.tokenType == TokenType.RETURN)
                {
                    if (ts.Peek().tokenType != TokenType.SEMICOLON) currentLine += " ";
                }

                // 3. New Lines using '{', '}', and ';'
                if (ts.HasNext() && ts.Peek().tokenType == TokenType.LBRACE)
                {
                    lines.Add(currentLine);
                    currentLine = "";
                }
                if (ts.HasNext() && ts.Peek().tokenType == TokenType.RBRACE)
                {
                    lines.Add(currentLine);
                    currentLine = "";
                    indentCount--;
                }
                if (ctoken.tokenType == TokenType.LBRACE)
                {
                    lines.Add(currentLine);
                    currentLine = "";
                    indentCount++;
                }
                if (ctoken.tokenType == TokenType.RBRACE)
                {
                    lines.Add(currentLine);
                    currentLine = "";
                }
                if (ctoken.tokenType == TokenType.SEMICOLON && ts.Peek().tokenType != TokenType.RBRACE)
                {
                    lines.Add(currentLine);
                    currentLine = "";
                }
            }
            if (currentLine != "") lines.Add(currentLine);



            if (logTokens)
            {
                List<Token> keywords = new List<Token>();
                List<Token> symbols = new List<Token>();
                List<Token> values = new List<Token>();

                lines.Add("");
                lines.Add($"// ============ Lexed ({tokens.Count}) tokens ============");
                string row = "";
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (TokenConstants.KEYWORDS.Any(type => type == tokens[i].tokenType)) keywords.Add(tokens[i]);
                    if (TokenConstants.SYMBOLS.Any(type => type == tokens[i].tokenType)) symbols.Add(tokens[i]);
                    if (TokenConstants.VALUES.Any(type => type == tokens[i].tokenType)) values.Add(tokens[i]);

                    row += tokens[i] + " ";
                    if (i % 5 == 0 && i != 0)
                    {
                        lines.Add($"// {row}");
                        row = "";
                    }
                }
                lines.Add($"// {row}");

                lines.Add("");
                lines.Add($"// ============ Keywords ({keywords.Count}/{tokens.Count}) tokens ============");
                for (int i = 0; i < keywords.Count; i++)
                {
                    lines.Add($"// {keywords[i].value}");
                }

                lines.Add("");
                lines.Add($"// ============ Symbols ({symbols.Count}/{tokens.Count}) tokens ============");
                row = "";
                int interval = 0;
                for (int i = 0; i < symbols.Count; i++)
                {
                    interval++;
                    row += $"{symbols[i].value}\t";
                    if (interval >= 10)
                    {
                        lines.Add($"// {row}");
                        row = "";
                        interval = 0;
                    }
                }

                lines.Add($"// {row}");
                lines.Add("");
                lines.Add($"// ============ Values ({values.Count}/{tokens.Count}) tokens ============");
                row = "";
                // interval = 0;
                for (int i = 0; i < values.Count; i++)
                {
                    lines.Add($"// {values[i].value}");
                    // interval++;
                    // row += $"{values[i].value}\t";
                    // if (interval >= 10)
                    // {
                    //     row = "";
                    //     interval = 0;
                    // }
                }
                // lines.Add($"// {row}");


            }

            File.WriteAllLines(path, lines);

            // for (int i = 0; i < tokens.Count; i++)
            // {
            //     // Indents
            //     for (int j = 0; j < indentCount; j++) {
            //         currentLine += "\t";
            //     }

            //     // New Line
            //     if (tokens[i].tokenType == TokenType.LBRACE)
            //     {
            //         indentCount++;
            //         currentLine = tokens[i].value;
            //         lines.Add(currentLine);
            //         currentLine = "";
            //     }

            //     if (tokens[i].tokenType == TokenType.RBRACE) {
            //         indentCount--;
            //     }

            //     currentLine += tokens[i].value;

            //     if (TOKENS_WITH_SPACE_AFTER.Any(t => t == tokens[i].tokenType))
            //     {
            //         currentLine += " ";
            //     }



            // }
        }
    }
}