using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shpoon.Lex;
using Shpoon.Parse;
using Shpoon.Parse.Nodes_2;

namespace Shpoon
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexer lexer = new Lexer();
            Parser parser = new Parser();

            lexer.AddDefinition(new TokenDefinition(TokenType.accessor, "(public|private)")); // ([^a-zA-Z0-9])

            lexer.AddDefinition(new TokenDefinition(TokenType.typeSpecifier, "(bool|byte|short|int|float|double|char|string)")); // ([^a-zA-Z0-9])
            lexer.AddDefinition(new TokenDefinition(TokenType.constant, @"\d*[\.]?\d+"));
            lexer.AddDefinition(new TokenDefinition(TokenType.@string, "\"(\\.|[^\"\\\\]*)\""));

            lexer.AddDefinition(new TokenDefinition(TokenType.accessOperator, "[.]"));
            lexer.AddDefinition(new TokenDefinition(TokenType.preOperator, "[!]"));
            //lexer.AddDefinition(new TokenDefinition(TokenType.postOperator, ""));
            lexer.AddDefinition(new TokenDefinition(TokenType.prepostOperator, @"(\+\+|\-\-)"));
            lexer.AddDefinition(new TokenDefinition(TokenType.comma, "[,]"));
            lexer.AddDefinition(new TokenDefinition(TokenType.binaryOperator, "(\\+|-|=|\\*|\\/|\\<|\\>)"));
            lexer.AddDefinition(new TokenDefinition(TokenType.@new, "new"));

            lexer.AddDefinition(new TokenDefinition(TokenType.lineEnd, "[;]"));

            lexer.AddDefinition(new TokenDefinition(TokenType.rBraceOpen, "[(]"));
            lexer.AddDefinition(new TokenDefinition(TokenType.rBraceClose, "[)]"));
            lexer.AddDefinition(new TokenDefinition(TokenType.cBraceOpen, "[{]"));
            lexer.AddDefinition(new TokenDefinition(TokenType.cBraceClose, "[}]"));

            lexer.AddDefinition(new TokenDefinition(TokenType.@namespace, "namespace"));
            lexer.AddDefinition(new TokenDefinition(TokenType.@class, "class"));

            lexer.AddDefinition(new TokenDefinition(TokenType.ifStatement, "if"));
            lexer.AddDefinition(new TokenDefinition(TokenType.elseStatement, "else"));
            lexer.AddDefinition(new TokenDefinition(TokenType.doStatement, "do"));
            lexer.AddDefinition(new TokenDefinition(TokenType.whileStatement, "while"));
            lexer.AddDefinition(new TokenDefinition(TokenType.forStatement, "for"));
            lexer.AddDefinition(new TokenDefinition(TokenType.returnStatement, "return"));

            lexer.AddDefinition(new TokenDefinition(TokenType.identifier, "[a-zA-Z_][a-zA-Z0-9_]*"));
            //string source = "a = b + c.d";//"int a = 5;";

            //Console.WriteLine(source);

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            //foreach (var token in lexer.Tokenize(source))
            //{
            //    Console.WriteLine($"{token.Value} {token.Type.ToString()}");
            //}

            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            //List<Token> tokens = lexer.Tokenize(source).ToList();
            //Console.WriteLine(parser._ParseExpression(tokens).ToString());

            string source = System.IO.File.ReadAllText(@"D:\C#\Shpoon\TestFile.txt"); //"namespace Shpoon{class TestClass{int temp(int v){return v + 5;}}}";

            Console.WriteLine(source);

            for (int i = 0; i < 3; i++)
                Console.WriteLine();

            TokenString tokens = new TokenString();

            foreach (var token in lexer.Tokenize(source))
            {
                tokens.Add(token);
                Console.WriteLine($"{token.Value} {token.Type.ToString()}");
            }

            for (int i = 0; i < 3; i++)
                Console.WriteLine();


            int zero = 0;
            var node = NamespaceNode.Parse(tokens, ref zero);
            zero = 0;

            Console.WriteLine(node);

            Console.ReadLine();
        }
    }
}
