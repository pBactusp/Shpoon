using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class FieldNode : AccessibleNode
    {
        public static FieldNode Parse(TokenString tStr, ref int index)
        {
            var accessors = AccessibleNode.GetAccessors(tStr, ref index);

            if (!tStr.Match(index, TokenType.typeSpecifier, TokenType.identifier, TokenType.lineEnd))
                return null;

            string type = tStr[index].Value;
            index++;
            string name = tStr[index].Value;
            index++;

            FieldNode node = new FieldNode(type, name, accessors);

            return node;
        }


        public FieldNode(string type, string name, List<string> accessors) : base(accessors)
        {
            Name = name;
            Type = type;

            Class = null;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }

        public ClassNode Class { get; set; }

        public override string ToString(string prevIndent)
        {
            return prevIndent + AccessorsToString() + Type + ' ' + Name + ';' + Environment.NewLine;
        }
    }
}
