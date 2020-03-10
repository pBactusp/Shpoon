using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ConditionalStaNode : StatementNode
    {
        public static ConditionalStaNode Parse(TokenString tStr, ref int index)
        {
            int startIndex = index;

            if (!tStr.Match(index, TokenType.ifStatement, TokenType.rBraceOpen))
                return null;

            index++;

            ExpressionNode condition = Exp.Parse(tStr.GetRangeInBrackets(ref index));
            StatementNode ifTrue = Sta.Parse(tStr, ref index);
            StatementNode ifFalse = null;

            if (tStr.Match(index, TokenType.elseStatement))
            {
                index++;
                ifFalse = Sta.Parse(tStr, ref index);
            }

            ConditionalStaNode node = new ConditionalStaNode(condition, ifTrue, ifFalse);

            return node;
        }


        private ConditionalStaNode(ExpressionNode condition, StatementNode ifTrue, StatementNode ifFalse)
        {
            Condition = condition;
            BodyTrue = ifTrue;
            BodyFalse = ifFalse;
        }

        public ExpressionNode Condition { get; private set; }
        
        public StatementNode BodyTrue { get; private set; }
        public StatementNode BodyFalse { get; private set; }

        public override string ToString(string prevIndent)
        {
            string indent = prevIndent + "    ";
            StringBuilder ret = new StringBuilder();
            ret.AppendLine(prevIndent + $"if ({Condition.ToString()})");

            ret.Append(BodyTrue.ToString(BodyTrue is CompoundStaNode ? prevIndent : indent));
            
            if (BodyFalse != null)
            {
                ret.AppendLine(prevIndent + "else");
                ret.AppendLine(BodyFalse.ToString(BodyFalse is CompoundStaNode ? prevIndent : indent));
            }

            return ret.ToString();
        }
    }
}
