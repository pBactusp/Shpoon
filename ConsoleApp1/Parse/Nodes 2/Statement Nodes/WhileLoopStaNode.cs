using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class WhileLoopStaNode : LoopStatementNode
    {
        public static WhileLoopStaNode Parse(TokenString tStr, ref int index)
        {
            int startIndex = index;

            if (!tStr.Match(index, TokenType.whileStatement, TokenType.rBraceOpen))
            {
                index = startIndex;
                return null;
            }

            index++;
            ExpressionNode condition = Exp.Parse(tStr.GetRangeInBrackets(ref index));
            StatementNode body = Sta.Parse(tStr, ref index);
            WhileLoopStaNode node = new WhileLoopStaNode(condition, body);

            return node;
        }


        private WhileLoopStaNode(ExpressionNode condition, StatementNode body)
        {
            Condition = condition;
            Body = body;
        }

        public override string ToString(string prevIndent)
        {
            StringBuilder ret = new StringBuilder();
            ret.AppendLine(prevIndent + $"while({Condition.ToString()})");
            ret.Append(Body.ToString(Body is CompoundStaNode ? prevIndent : prevIndent + "    "));

            return ret.ToString();
        }
    }
}
