using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class MemberExpNode : BinaryExpNode
    {
        public MemberExpNode(string @operator, TokenString left, TokenString right) : base(@operator, left, right)
        {

        }
    }
}
