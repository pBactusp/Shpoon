using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Lex
{
    public class Token
    {
        public TokenPosition Position { get; set; }
        public string Value { get; set; }
        public TokenType Type { get; set; }

        public override string ToString()
        {
            return $"{Value} {Type}";



        }
    }
}
