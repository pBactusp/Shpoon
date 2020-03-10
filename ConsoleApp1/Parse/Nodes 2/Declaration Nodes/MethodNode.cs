using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class MethodNode : MemberNode
    {
        public static MethodNode Parse(TokenString tStr, ref int index)
        {
            int startIndex = index;

            var accessors = AccessibleNode.GetAccessors(tStr, ref index);

            if (!tStr.Match(index, TokenType.typeSpecifier, TokenType.identifier, TokenType.rBraceOpen))
                return null;

            string type = tStr[index].Value;
            index++;
            string name = tStr[index].Value;
            index++;

            var args = new List<ArgumentNode>();

            var argTokens = tStr.GetRangeInBrackets(ref index).Split(false, TokenType.comma);

            foreach (var argTs in argTokens)
                args.Add(new ArgumentNode(argTs[0].Value, argTs[1].Value));

            index = tStr.FindNextIndex(index, TokenType.cBraceOpen);

            var sta = CompoundStaNode.Parse(tStr, ref index);

            if (sta == null)
            {
                index = startIndex;
                return null;
            }

            MethodNode node = new MethodNode(type, name, accessors, args, sta);

            return node;
        }


        public List<ArgumentNode> Arguments;

        protected MethodNode(string type, string name, List<string> accessors, List<ArgumentNode> arguments, CompoundStaNode compoundStatement) : base(accessors)
        {
            Name = name;
            Type = type;
            Accessors = accessors;
            Arguments = arguments;

            Statements = compoundStatement;

            Class = null;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }

        public CompoundStaNode Statements { get; private set; }

        public ClassNode Class { get; set; }

        public void AddStatement(StatementNode statement)
        {
            Statements.AddStatement(statement);
        }




        public override string ToString(string prevIndent)
        {
            StringBuilder ret = new StringBuilder();
            ret.Append(prevIndent);
            ret.Append(AccessorsToString());
            ret.Append($"{Type} {Name}(");

            if (Arguments.Count > 0)
            {
                ret.Append($"{Arguments[0].Type} {Arguments[0].Name}");

                for (int i = 1; i < Arguments.Count; i++)
                    ret.Append($", {Arguments[i].Type} {Arguments[i].Name}");
            }

            ret.AppendLine(")");

            ret.Append(Statements.ToString(prevIndent));

            return ret.ToString();
        }
    }
}
