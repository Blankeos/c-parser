# 🤓 The "Carlo" Parser in C#

A programming language parser implemented in C# as a final requirement for my CCS 238 Programming Languages class for **Dr. Felipe P. Vista IV**.

This program parses an "almost C/C++"-like programming language called `Carlo`.

It demonstrates compiler design theory from:

```
Scanner (Lexical Analysis) -> Parser (Syntax Analysis)
```

```
Written by:
- 🤓 Carlo Antonio T. Taleon
- 👧 Glecy S. Elizalde
- 🤠 Christopher Joseph T. Rubinos
```

## Get started

> Make sure to install [.Net SDK](https://dotnet.microsoft.com/en-us/download) if you haven't yet

1. Clone this repo and change your directory

```sh
# >
git clone https://github.com/Blankeos/cs-parser
cd cs-parser
# cs-parser>
```

2. Put your inputs inside `input.carlo`.

3. Run the project in your terminal

```sh
# cs-parser>
dotnet run
```

4. Open `output.carlo` to check the output!

## Introducing: The 🤓 "Carlo" Programming Language

It's an "almost C/C++"-like language made for our programming languages class. As the name suggests, it has similar syntax to C/C++.

### Features

### Example usage

```c#
/* ----------input.carlo---------- */

boolean fMusta(char aNgaln[15], int num) {
Printf("Hello world!! Si '%s' na ewan ito! Pang-%d \n ::) \r",aNgaln, &num);
if (num!= len(aNgaln)) {return true;} else {printf("\r\n Tsamba!! \n"); return false;}
return ;}


/* ---------output.carlo---------- */

boolean fMusta(char aNgaln[15], int num)
{
    Printf("Hello world!! Si '%s' na ewan ito! Pang-%d \n ::) \r", aNgaln, &num);
    if (num!= len(aNgaln))
    {
        return true;
    }
    else
    {
        printf("\r\n Tsamba!! \n");
        return false;
    }
    return ;
}

/*
Lexed (48) tokens
<BOOLEAN: boolean >
<IDENTIFIER: fMusta >
<LPAR: ( >
<CHAR: char >
<IDENTIFIER: aNgaln >
<LBRACK: [ >
<INT_VAL: 15 >
<RBRACK: ] >
<COMMA: , >
<INT: int >
<IDENTIFIER: num >
<RPAR: ) >
<LBRACE: { >
<IDENTIFIER: Printf >
<LPAR: ( >
<STRING_VAL: "Hello world!! Si '%s' na ewan ito! Pang-%d \n ::) \r" >
<IDENTIFIER: aNgaln >
<COMMA: , >
<AMPERSAND: & >
<IDENTIFIER: num >
<RPAR: ) >
<SEMICOLON: ; >
<IF: if >
<LPAR: ( >
<IDENTIFIER: num >
<NOT: ! >
<EQUAL: = >
<IDENTIFIER: len >
<LPAR: ( >
<IDENTIFIER: aNgaln >
<RPAR: ) >
<RPAR: ) >
<LBRACE: { >
<RETURN: return >
<BOOLEAN_VAL: true >
<RBRACE: } >
<ELSE: else >
<LBRACE: { >
<IDENTIFIER: printf >
<LPAR: ( >
<STRING_VAL: "\r\n Tsamba!! \n" >
<SEMICOLON: ; >
<RETURN: return >
<BOOLEAN_VAL: false >
<RBRACE: } >
<RETURN: return >
<SEMICOLON: ; >
<RBRACE: } >
*/
```

### Grammar

```
Node -> FunctionDef | Assign | FunctionCall | IfStatement;

Function -> DataType + Identifier + FunctionArgs + FunctionBody;
DataType -> 'boolean' | 'int' | 'string' | 'char';
Identifier -> '';
FunctionArgs -> '(' +

```
