namespace CParser
{
    enum TokenType
    {
        // keywords
        BOOLEAN,    // boolean
        INT,        // int
        STRING,     // string
        CHAR,       // char
        FLOAT,      // float
        DOUBLE,     // double
        IF,         // if
        ELSE,       // else
        RETURN,     // return
        BOOLEAN_VAL, // true | false,

        // symbols
        LBRACE,     // {
        RBRACE,     // }
        LPAR,       // (
        RPAR,       // )
        LBRACK,     // [
        RBRACK,     // ]
        EQUAL,      // =
        NOT,        // !
        SEMICOLON,  // ;
        COMMA,      // ,
        OPERATOR,   // + | - | * | / | %
        AMPERSAND,  // &

        // values (dynamic tokens that have specific rules)
        IDENTIFIER, // iAmaVariable | I___Iam_A_VARIABLE
        STRING_VAL, // "Hello"
        CHAR_VAL,   // 'c'
        INT_VAL,    // 21
        FLOAT_VAL,  // 21.1f
        DOUBLE_VAL, // 21.1d
    }

    struct Token
    {
        public TokenType tokenType;
        public string value;

        public Token(TokenType tokenType, string value)
        {
            this.tokenType = tokenType;
            this.value = value;
        }

        public override string ToString()
        {
            return $"<{this.tokenType}: {this.value} >";
        }
    }

    static class TokenConstants
    {
        public static readonly TokenType[] KEYWORDS = { TokenType.BOOLEAN, TokenType.INT, TokenType.STRING, TokenType.CHAR, TokenType.FLOAT, TokenType.DOUBLE, TokenType.IF, TokenType.ELSE, TokenType.RETURN, TokenType.BOOLEAN_VAL };

        public static readonly TokenType[] SYMBOLS = {
            TokenType.LBRACE, TokenType.RBRACE,
            TokenType.LPAR, TokenType.RPAR,
            TokenType.LBRACK, TokenType.RBRACK,
            TokenType.EQUAL, TokenType.NOT,
            TokenType.SEMICOLON, TokenType.COMMA,
            TokenType.OPERATOR, TokenType.AMPERSAND,
        };

        public static readonly TokenType[] VALUES = {
            TokenType.IDENTIFIER, TokenType.STRING_VAL,
            TokenType.CHAR_VAL, TokenType.INT_VAL,
            TokenType.FLOAT_VAL, TokenType.DOUBLE_VAL,
        };

    }
}





























/* Tokens for Math
    enum TokenEnum
    {
        NUMBER,
        PUNCTUATION,
        OPERATOR,
    }

    struct Token
    {
        public TokenEnum tokenType;
        public string value;

        public Token(TokenEnum tokenType, string value)
        {
            this.tokenType = tokenType;
            this.value = value;
        }

        public override string ToString()
        {
            return $"<{this.tokenType}: {this.value} >";
        }
    }
*/