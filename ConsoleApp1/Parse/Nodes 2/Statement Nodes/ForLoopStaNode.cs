using Shpoon.Lex;
using Shpoon.Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ForLoopStaNode : LoopStatementNode
    {
        public static ForLoopStaNode Parse(TokenString tStr, ref int index)
        {
            int startIndex = index;

            if (!tStr.Match(index, TokenType.forStatement, TokenType.rBraceOpen))
                return null;

            index++;

            var forDef = tStr.GetRangeInBrackets(ref index).Split(false, TokenType.lineEnd);

            if (forDef.Count != 3)
            {
                index = startIndex;
                return null;
            }

            VariableDefExpNode declarations = Exp.Parse(forDef[0]) as VariableDefExpNode;
            ExpressionNode condition = Exp.Parse(forDef[1]);

            if (condition == null)
            {
                index = startIndex;
                return null;
            }

            StatementNode body = Sta.Parse(tStr, ref index);

            ForLoopStaNode node = new ForLoopStaNode(declarations, condition, body);

            var exps = forDef[2].Split(false, TokenType.comma);

            foreach (var exp in exps)
                node.AddExpression(Exp.Parse(exp));

            return node;
        }


        private List<ExpressionNode> expressions;

        private ForLoopStaNode(VariableDefExpNode definitions, ExpressionNode condition, StatementNode body)
        {
            Definitions = definitions;
            Condition = condition;
            Body = body;

            expressions = new List<ExpressionNode>();
        }

        public VariableDefExpNode Definitions { get; private set; }
        public ExpressionNode this[int index]
        {
            get { return expressions[index]; }
            set { expressions[index] = value; }
        }

        private void AddExpression(ExpressionNode expression)
        {
            expressions.Add(expression);
        }

        public override string ToString(string prevIndent)
        {
            StringBuilder ret = new StringBuilder();
            ret.AppendLine(prevIndent + $"for({Definitions.ToString()}; {Condition.ToString()}; {string.Join(", ", expressions)})");
            ret.Append(Body.ToString(Body is CompoundStaNode ? prevIndent : prevIndent + "    "));

            return ret.ToString();
        }

    }
}
