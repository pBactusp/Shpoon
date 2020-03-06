using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class BinaryExpNode : ExpressionNode
    {
        public BinaryExpNode(string @operator, TokenString left, TokenString right)
        {
            Operator = @operator;

            Left = Exp.Parse(left);
            Right = Exp.Parse(right);
        }

        public string Operator { get; private set; }
        public ExpressionNode Left { get; private set; }
        public ExpressionNode Right { get; private set; }

        public override string ToString()
        {
            StringBuilder expStringB = new StringBuilder();

            if (Left is BinaryExpNode leftExp && Parser.GetOperatorPriority(leftExp.Operator) > Parser.GetOperatorPriority(Operator))
                expStringB.Append($"({Left.ToString()})");
            else
                expStringB.Append(Left.ToString());

            expStringB.Append($" {Operator} ");

            if (Right is BinaryExpNode rightExp && Parser.GetOperatorPriority(rightExp.Operator) > Parser.GetOperatorPriority(Operator))
                expStringB.Append($"({Right.ToString()})");
            else
                expStringB.Append(Right.ToString());

            return expStringB.ToString(); //$"{Left.ToString()} {Operator} {Right.ToString()}";
        }
    }
}
