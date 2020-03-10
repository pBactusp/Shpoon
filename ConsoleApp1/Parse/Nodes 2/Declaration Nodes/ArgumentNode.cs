using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ArgumentNode : Node
    {
        public ArgumentNode(string type, string Name)
        {
            Type = type;
            this.Name = Name;
        }

        public string Type { get; private set; }
        public string Name { get; private set; }
    }
}
