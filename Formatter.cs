namespace CParser
{
    class Formatter
    {
        private static readonly TokenType[] TOKENS_WITH_SPACE_AFTER = {
            TokenType.BOOLEAN, TokenType.INT, TokenType.STRING, TokenType.CHAR, TokenType.FLOAT,
            TokenType.DOUBLE, TokenType.IF, TokenType.ELSE, TokenType.COMMA
        };
        public static void ParseToFile(List<Token> tokens)
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
                Console.WriteLine(ctoken);
            }
            if (currentLine != "") lines.Add(currentLine);

            File.WriteAllLines("output_carlo.txt", lines);

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