using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shpoon.Lex
{
    public class Lexer
    {
        private List<TokenDefinition> definitions;

        public Lexer()
        {
            definitions = new List<TokenDefinition>();
        }

        public void AddDefinition(TokenDefinition definition)
        {
            definitions.Add(definition);
        }

        public IEnumerable<Token> Tokenize(string source)
        {
            int line = 0,
                index = 0;

            index++;

            string[] sourceLines = source.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var currentLine in sourceLines)
            {
                for (int col = 0; col < currentLine.Length; col++)
                {
                    while (currentLine[col] == ' ')
                        col++;

                    foreach (var def in definitions)
                    {
                        var match = def.Match(currentLine, col);

                        if (match.Success && match.Index == col)
                        {
                            Token token = new Token()
                            {
                                Type = def.Type,
                                Value = match.Value,
                                Position = new TokenPosition()
                                {
                                    Column = col,
                                    Index = index,
                                    Line = line
                                }
                            };

                            yield return token;

                            index++;
                            col += match.Value.Length - 1;

                            break;
                        }
                    }
                }

                line++;
            }

            //yield return new Token()
            //{
            //    Type = TokenType.end,
            //    Value = "END",
            //    Position = new TokenPosition()
            //    {
            //        Column = 0,
            //        Index = index,
            //        Line = line
            //    }
            //};
        }

    }
}
