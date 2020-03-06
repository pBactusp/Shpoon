using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public abstract class UnaryExpNode : ExpressionNode
    {
        public UnaryExpNode(string @operator, TokenString target)
        {
            Operator = @operator;
            Target = Exp.Parse(target);
        }

        public string Operator { get; private set; }
        public ExpressionNode Target { get; private set; }
    }
}
