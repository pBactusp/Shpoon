﻿using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class DoLoopStaNode : LoopStatementNode
    {
        public static DoLoopStaNode Parse(TokenString tStr, ref int index)
        {
            int startIndex = index;

            if (!tStr.Match(index, TokenType.doStatement, TokenType.cBraceOpen))
                return null;

            index++;


            StatementNode body = Sta.Parse(tStr, ref index);

            if (!tStr.Match(index, TokenType.whileStatement, TokenType.rBraceOpen))
            {
                index = startIndex;
                return null;
            }

            index++;
            ExpressionNode condition = Exp.Parse(tStr.GetRangeInBrackets(ref index));
            
            if (!tStr.Match(index, TokenType.lineEnd))
            {
                index = startIndex;
                return null;
            }

            index++;
            DoLoopStaNode node = new DoLoopStaNode(condition, body);

            return node;
        }


        private DoLoopStaNode(ExpressionNode condition, StatementNode body)
        {
            Condition = condition;
            Body = body;
        }


        public override string ToString(string prevIndent)
        {
            StringBuilder ret = new StringBuilder();
            ret.AppendLine(prevIndent + "do");
            ret.Append(Body.ToString(Body is CompoundStaNode ? prevIndent : prevIndent + "    "));
            ret.Append(prevIndent + $"while({Condition.ToString()});");

            return ret.ToString();
        }

    }
}
