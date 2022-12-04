namespace CParser
{
    class Parser
    {
        private static readonly TokenType[] DATA_TYPES = {
            TokenType.BOOLEAN, TokenType.INT,
            TokenType.STRING, TokenType.CHAR,
            TokenType.FLOAT, TokenType.DOUBLE,
        };

        public static Node Parse(List<Token> tokens)
        {
            TokenStream ts = new TokenStream(tokens.ToArray());

            Token t = ts.Next();

            if (DATA_TYPES.Any(type => type == t.tokenType))
            {
                var dataType = Parse_DataType(t);

                Token id_token = ts.Next();
                if (id_token.tokenType == TokenType.IDENTIFIER)
                {
                    var identifier = Parse_Identifier(id_token);

                    var peekedToken = ts.Peek();

                    switch (peekedToken.tokenType)
                    {
                        case TokenType.LPAR:
                            Console.WriteLine(Parse_FuncDec(ts, dataType, identifier));
                            break;
                        case TokenType.EQUAL:
                            Parse_FuncDec(ts, dataType, identifier);
                            break;
                        default:
                            throw new SyntaxError(peekedToken, "'(' or '='");
                    }
                }
                else
                {
                    throw new SyntaxError(id_token, "identifier");
                }
            }
            return default;
        }

        // Recursive Parsers
        private static FuncDecNode Parse_FuncDec(TokenStream ts, DataTypeNode dataTypeNode, IdentifierNode identifierNode)
        {
            var t = ts.Next(); // Assumes it's an LPAR

            return new FuncDecNode(dataTypeNode, identifierNode);
        }

        private static DataTypeNode Parse_DataType(Token token)
        {
            return new DataTypeNode(token.value);
        }

        private static IdentifierNode Parse_Identifier(Token token)
        {
            return new IdentifierNode(token.value);
        }
    }

    [Serializable]
    class SyntaxError : Exception
    {
        public SyntaxError()
        {

        }

        public SyntaxError(Token token, string expected) : base($"Token ( {token.value} ) invalid. ( {expected} ) expected.")
        {

        }

        /// <summary>
        /// Don't display call stack as it's irrelevant
        /// </summary>
        public override string StackTrace
        {
            get
            {
                return "";
            }
        }
    }

}
