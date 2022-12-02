namespace CParser
{
    class TokenStream
    {
        public Token[] tokens;
        private int pos = 0;

        public TokenStream(Token[] tokens)
        {
            this.tokens = tokens;
        }

        public bool HasNext()
        {
            if (pos >= tokens.Length) return false;
            return true;
        }

        public bool IsEnd()
        {
            if (pos < tokens.Length) return true;
            return false;
        }

        // public bool IsEnd()
        // {
        //     if (pos >= tokens.Length) return true;
        //     return false;
        // }

        public Token Next()
        {
            var token = tokens[pos++];

            return token;
        }

        public Token Peek()
        {
            return tokens[pos];
        }
    }
}