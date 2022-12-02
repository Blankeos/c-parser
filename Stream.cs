// namespace CParser
// {
//     abstract class Stream<T>
//     {
//         public abstract T Next();
//         public abstract T Peek();
//         public abstract bool IsEnd();
//     }

//     class InputStream : Stream<char>
//     {
//         public string[] inputLines;
//         public int pos = 0;
//         public int line = 0;
//         public int col = 0;

//         public InputStream(string[] inputLines)
//         {
//             this.inputLines = inputLines;
//         }

//         /// <summary>
//         /// Returns the next character in the input stream and moves the pointer to the next.
//         /// </summary>
//         public override char Next()
//         {
//             var ch = inputLines[line][pos++];
//             if (pos >= inputLines[line].Length)
//             {
//                 line++;
//                 pos = 0;
//                 Console.WriteLine("New Line:" + line);
//             }
//             else
//             {
//                 col++;
//             }
//             return ch;
//         }

//         /// <summary>
//         /// Returns the next character but does not move the pointer.
//         /// </summary>
//         public override char Peek()
//         {
//             return inputLines[line][pos];
//         }

//         /// <summary>
//         /// Check if we reached the end of the input stream.
//         /// </summary>
//         public override bool IsEnd()
//         {
//             return line >= inputLines.Length - 1 && pos >= inputLines[line].Length - 1;
//         }

//         public void DisplayError(string message)
//         {
//             Console.WriteLine($"{message} ({line + 1} : {col})");
//         }
//     }

//     class TokenStream : Stream<Token>
//     {
//         public Token[] tokens;
//         public int pos = 0;

//         public TokenStream(Token[] tokens)
//         {
//             this.tokens = tokens;
//         }

//         public override Token Next()
//         {
//             throw new NotImplementedException();
//         }

//         public override Token Peek()
//         {
//             throw new NotImplementedException();
//         }

//         public override bool IsEnd()
//         {
//             throw new NotImplementedException();
//         }
//     }
// }