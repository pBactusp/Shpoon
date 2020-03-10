using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ReturnStaNode : StatementNode
    {
        public static ReturnStaNode Parse(TokenString tStr, ref int index)
        {
            int startIndex = index;

            if (!tStr.Match(index, TokenType.returnStatement))
                return null;

            index++;

            ExpressionNode expression = Exp.Parse(tStr.GetRangeUntil(ref index, TokenType.lineEnd));

            if (expression == null)
            {
                index = startIndex;
                return null;
            }

            ReturnStaNode node = new ReturnStaNode(expression);

            index++;
            return node;
        }



        private ReturnStaNode(ExpressionNode expression)
        {
            Expression = expression;
        }

        public ExpressionNode Expression { get; private set; }

        public override string ToString(string prevIndent)
        {
            return $"{prevIndent}return {Expression.ToString()};";
        }
    }
}
