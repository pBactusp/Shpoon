using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ExpressionStaNode : StatementNode
    {
        public static ExpressionStaNode Parse(TokenString tStr, ref int index)
        {

            var expression = Exp.Parse(tStr.GetRangeUntil(ref index, TokenType.lineEnd));

            ExpressionStaNode node = new ExpressionStaNode(expression);

            index++;
            return node;
        }

        private ExpressionStaNode(ExpressionNode expression)
        {
            Expression = expression;
        }

        public ExpressionNode Expression { get; private set; }

        public override string ToString(string prevIndent)
        {
            return prevIndent + $"{Expression.ToString()};";
        }

    }
}
