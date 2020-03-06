using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class PostUnaryExpNode : UnaryExpNode
    {
        public PostUnaryExpNode(string @operator, TokenString target) : base(@operator, target)
        {

        }


        public override string ToString()
        {
            return Target.ToString("") + Operator;
        }
        public override string ToString(string prevIndent)
        {
            return prevIndent + ToString();
        }
    }
}
