using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class NopStaNode : StatementNode
    {
        public static NopStaNode Parse(TokenString tStr, ref int index)
        {
            if (!tStr.Match(index, TokenType.lineEnd))
                return null;


            NopStaNode node = new NopStaNode(tStr[index].Value);
            index++;

            return node;
        }


        private NopStaNode(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public override string ToString(string prevIndent)
        {
            return prevIndent + Value + Environment.NewLine;
        }

    }
}
