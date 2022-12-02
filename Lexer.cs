using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CParser
{
    class Lexer
    {
        # region Static Functions
        public static List<Token> LexLines(string[] inputLines)
        {
            List<Token> tokens = new List<Token>();
            List<InvalidTokenException> tokenExceptions = new List<InvalidTokenException>();

            for (int i = 0; i < inputLines.Length; i++)
            {
                tokens = tokens.Concat(LexString(inputLines[i], i, tokenExceptions)).ToList();
            }

            if (tokenExceptions.Count > 0)
            {
                if (tokenExceptions.Count == 1) throw tokenExceptions[0];
                else throw new MultipleInvalidTokenException("Multiple Errors occured:", tokenExceptions);
            }
            return tokens;
        }

        // For one line/string
        public static List<Token> LexString(string charStream, int currentLine, List<InvalidTokenException> tokenExceptions)
        {
            List<Token> tokens = new List<Token>();

            // Check if comment == %, returns an empty token list
            if (charStream == "" || charStream[0] == '%') return tokens;

            for (int i = 0; i < charStream.Length; i++)
            {
                char c = charStream[i];

                if (charStream[i] == ' ')
                {
                    continue;
                };

                // Keywords
                string? boolean_kw = LookAheadKeyword("boolean", ref i, charStream);
                if (boolean_kw != null)
                {
                    tokens.Add(new Token(TokenType.BOOLEAN, "boolean"));
                    continue;
                }

                string? int_kw = LookAheadKeyword("int", ref i, charStream);
                if (int_kw != null)
                {
                    tokens.Add(new Token(TokenType.INT, "int"));
                    continue;
                }

                string? string_kw = LookAheadKeyword("string", ref i, charStream);
                if (string_kw != null)
                {
                    tokens.Add(new Token(TokenType.STRING, "str"));
                    continue;
                }

                string? char_kw = LookAheadKeyword("char", ref i, charStream);
                if (char_kw != null)
                {
                    tokens.Add(new Token(TokenType.CHAR, "char"));
                    continue;
                }

                string? float_kw = LookAheadKeyword("float", ref i, charStream);
                if (char_kw != null)
                {
                    tokens.Add(new Token(TokenType.FLOAT, "float"));
                    continue;
                }

                string? double_kw = LookAheadKeyword("double", ref i, charStream);
                if (double_kw != null)
                {
                    tokens.Add(new Token(TokenType.DOUBLE, "double"));
                    continue;
                }

                string? if_kw = LookAheadKeyword("if", ref i, charStream);
                if (if_kw != null)
                {
                    tokens.Add(new Token(TokenType.IF, "if"));
                    continue;
                }

                string? else_kw = LookAheadKeyword("else", ref i, charStream);
                if (else_kw != null)
                {
                    tokens.Add(new Token(TokenType.ELSE, "else"));
                    continue;
                }

                string? return_kw = LookAheadKeyword("return", ref i, charStream);
                if (return_kw != null)
                {
                    tokens.Add(new Token(TokenType.RETURN, "return"));
                    continue;
                }

                string? boolVal_true_kw = LookAheadKeyword("true", ref i, charStream);
                if (boolVal_true_kw != null)
                {
                    tokens.Add(new Token(TokenType.BOOLEAN_VAL, "true"));
                    continue;
                }
                string? boolVal_false_kw = LookAheadKeyword("false", ref i, charStream);
                if (boolVal_false_kw != null)
                {
                    tokens.Add(new Token(TokenType.BOOLEAN_VAL, "false"));
                    continue;
                }

                // Symbols
                if (c == '{')
                {
                    tokens.Add(new Token(TokenType.LBRACE, "{"));
                    continue;
                }
                if (c == '}')
                {
                    tokens.Add(new Token(TokenType.RBRACE, "}"));
                    continue;
                }
                if (c == '(')
                {
                    tokens.Add(new Token(TokenType.LPAR, "("));
                    continue;
                }
                if (c == ')')
                {
                    tokens.Add(new Token(TokenType.RPAR, ")"));
                    continue;
                }
                if (c == '[')
                {
                    tokens.Add(new Token(TokenType.LBRACK, "["));
                    continue;
                }
                if (c == ']')
                {
                    tokens.Add(new Token(TokenType.RBRACK, "]"));
                    continue;
                }
                if (c == '=')
                {
                    // Add lookahead for equality '==' here
                    tokens.Add(new Token(TokenType.EQUAL, "="));
                    continue;
                }
                if (c == '!')
                {
                    tokens.Add(new Token(TokenType.NOT, "!"));
                    continue;
                }
                if (c == ';')
                {
                    tokens.Add(new Token(TokenType.SEMICOLON, ";"));
                    continue;
                }
                if (c == ',')
                {
                    tokens.Add(new Token(TokenType.COMMA, ","));
                    continue;
                }
                if (Regex.Match(c.ToString(), @"\+|-|\*|\/|%").Success)
                {
                    tokens.Add(new Token(TokenType.OPERATOR, c.ToString()));
                    continue;
                }
                if (c == '&')
                {
                    tokens.Add(new Token(TokenType.AMPERSAND, "&"));
                    continue;
                }

                // Values
                string QUOTE_SYMBOL = @"""";
                string? string_val = LookAheadUntil(ref i, charStream, QUOTE_SYMBOL, QUOTE_SYMBOL);
                if (string_val != null)
                {
                    tokens.Add(new Token(TokenType.STRING_VAL, string_val));
                    continue;
                }

                string? identifier = LookAhead(ref i, charStream, @"[a-z]|[A-Z]|_", @"[a-z]|[A-Z]|_|[0-9]");
                if (identifier != null)
                {
                    tokens.Add(new Token(TokenType.IDENTIFIER, identifier));
                    continue;
                }

                string? number = LookAhead(ref i, charStream, @"[0-9]", @"[0-9]");
                if (number != null)
                {
                    tokens.Add(new Token(TokenType.INT_VAL, number));
                    continue;
                }

                // DEFAULT CASE
                tokenExceptions.Add(new InvalidTokenException(c.ToString(), currentLine, i));
            }
            return tokens;
        }
        #endregion

        # region Utility Functions
        private static bool LookAheadKeywordIsMatched(string keyword, int currentIndex, string charStream)
        {
            for (int i = 0; i < keyword.Length; i++)
            {
                if (i >= charStream.Length) break;
                if (charStream[currentIndex + i] != keyword[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static string? LookAheadKeyword(string keyword, ref int currentIndex, string charStream)
        {
            int startIndex = currentIndex;

            // if (startIndex + charStream.Length - 1 < keyword.Length - 1)
            // {
            //     return null;
            // }
            // Break Case: Remaining length of charStream, cannot match word

            // Trigger for LookAhead
            if (charStream[startIndex] == keyword[0])
            {
                // Break Case: Remaining Length of charStream, does not match word length
                if (charStream.Length - startIndex < keyword.Length)
                {
                    currentIndex = startIndex;
                    return null;
                }
                for (int i = 0; i < keyword.Length; i++)
                {
                    // Break Case: DID NOT MATCH
                    if (charStream[currentIndex] != keyword[i])
                    {
                        currentIndex = startIndex;
                        return null;
                    }
                    currentIndex++;
                    if (currentIndex >= charStream.Length) break;
                }
                // Every index matched, so successful, let's return keyword.
                currentIndex--; // last is an index above the keyword. So it wasn't necessary. Put it back.
                return keyword;
            }

            currentIndex = startIndex;
            return null;
        }

        private static string? LookAhead(ref int currentIndex, string charStream, string startPattern, string loopPattern)
        {
            string result = "";

            // Look Ahead Entry
            if (!Regex.Match(charStream[currentIndex].ToString(), startPattern).Success) return null;

            // Look Ahead Loop
            for (int j = currentIndex; j < charStream.Length; j++)
            {
                if (Regex.Match(charStream[j].ToString(), loopPattern).Success)
                {
                    result += charStream[j];
                }
                else
                {
                    break;
                }
            }

            // Success
            if (result.Length > 0)
            {
                currentIndex += result.Length - 1;
                return result;
            }
            return null;
        }

        private static string? LookAheadUntil(ref int currentIndex, string charStream, string startPattern, string endPattern)
        {
            string result = "";

            // Look Ahead Entry
            if (!Regex.Match(charStream[currentIndex].ToString(), startPattern).Success) return null;

            // Found Opening "
            result += charStream[currentIndex];
            currentIndex++;

            // Look Ahead Loop
            for (int j = currentIndex; j < charStream.Length; j++)
            {
                if (Regex.Match(charStream[j].ToString(), endPattern).Success)
                {
                    result += charStream[j];
                    break;
                }
                result += charStream[j];
            }

            // Success
            if (result.Length > 0)
            {
                currentIndex += result.Length - 2;
                return result;
            }
            return null;
        }

        public static void PrintTokens(List<Token> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                Console.WriteLine(tokens[i]);
            }
        }
        # endregion
    }
}

[Serializable]
class InvalidTokenException : Exception
{
    public InvalidTokenException() { }

    public InvalidTokenException(string token, int? line, int? col) : base($"Invalid Token '{token}' at line {line + 1} column {col}")
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

[Serializable]
class MultipleInvalidTokenException : AggregateException
{
    public MultipleInvalidTokenException() { }

    public MultipleInvalidTokenException(string message, List<InvalidTokenException> exceptions) : base(message, exceptions)
    {

    }

    public override string StackTrace
    {
        get
        {
            return "";
        }
    }
}
// /* Lex for Math
// for (int i = 0; i < charStream.Length; i++)
// {
//     char c = charStream[i];

//     if (c == ' ' || c == '\n') continue;
//     else if (Regex.Match(c.ToString(), @"\+|-|\*|\/").Success)
//     {
//         tokens.Add(new Token(TokenEnum.OPERATOR, c.ToString()));
//     }
//     else if (Regex.Match(c.ToString(), @"[0-9]").Success)
//     {
//         string number = "";
//         while (Regex.Match(c.ToString(), @"[0-9]").Success && i < charStream.Length)
//         {
//             number += c;
//             i++;
//             if (i >= charStream.Length) break;
//             c = charStream[i];
//         }
//         tokens.Add(new Token(TokenEnum.NUMBER, number));
//         i--; // Subtract the indexer because that means the last lookAhead failed, so put the indexer back
//     }
//     else if (Regex.Match(c.ToString(), @"\(|\)").Success)
//     {
//         tokens.Add(new Token(TokenEnum.PUNCTUATION, c.ToString()));
//     }
// }
// */