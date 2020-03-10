using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class CtorNode : MethodNode
    {
        public static CtorNode Parse(string className, TokenString tStr, ref int index)
        {
            int startIndex = index;

            var accessors = new List<string>();

            while (tStr[index].Type == TokenType.accessor)
            {
                accessors.Add(tStr[index].Value);
                index++;
            }

            if (!(tStr.Match(index, TokenType.identifier, TokenType.rBraceOpen) && tStr[index].Value == className))
                return null;

            string type = tStr[index].Value;
            string name = tStr[index].Value;
            index++;

            var args = new List<ArgumentNode>();

            var argTokens = tStr.GetRangeInBrackets(ref index)?.Split(false, TokenType.comma);

            if (argTokens != null)
                foreach (var argTs in argTokens)
                    args.Add(new ArgumentNode(argTs[0].Value, argTs[1].Value));

            index = tStr.FindNextIndex(index, TokenType.cBraceOpen);

            var sta = CompoundStaNode.Parse(tStr, ref index);

            if (sta == null)
            {
                index = startIndex;
                return null;
            }

            CtorNode node = new CtorNode(type, name, accessors, args, sta);

            return node;
        }


        private CtorNode(string type, string name, List<string> accessors, List<ArgumentNode> arguments, CompoundStaNode compoundStatement) : base(type, name, accessors, arguments, compoundStatement)
        {

        }
    }
}
