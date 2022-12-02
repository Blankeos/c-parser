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
        INT_VAL, // 21
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