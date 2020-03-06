using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shpoon.Lex
{
    public class TokenDefinition
    {
        private Regex regex;

        public TokenDefinition(TokenType type, string pattern)
        {
            Type = type;
            regex = new Regex(pattern);
        }

        public TokenType Type { get; }
        public bool IsIgnored { get; set; }

        public Match Match(string input)
        {
            return regex.Match(input);
        }
        public Match Match(string input, int startat)
        {
            return regex.Match(input, startat);
        }
        public Match Match(string input, int startat, int length)
        {
            return regex.Match(input, startat, length);
        }

    }
}
