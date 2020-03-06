using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class LiteralExpNode : ExpressionNode
    {
        public LiteralExpNode(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; private set; }
        public string Value { get; private set; }

        public override string ToString()
        {
            return Value;
        }
    }
}
