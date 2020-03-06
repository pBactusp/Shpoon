using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public static class Sta
    {
        public static StatementNode Parse(TokenString tStr, ref int index)
        {
            switch (tStr[index].Type)
            {
                case TokenType.cBraceOpen:
                    return CompoundStaNode.Parse(tStr, ref index);

                case TokenType.ifStatement:
                    return ConditionalStaNode.Parse(tStr, ref index);

                case TokenType.doStatement:
                    return DoLoopStaNode.Parse(tStr, ref index);

                case TokenType.whileStatement:
                    return WhileLoopStaNode.Parse(tStr, ref index);

                case TokenType.forStatement:
                    return ForLoopStaNode.Parse(tStr, ref index);

                case TokenType.returnStatement:
                    return ReturnStaNode.Parse(tStr, ref index);

                case TokenType.lineEnd:
                    return NopStaNode.Parse(tStr, ref index);

                default:
                    return ExpressionStaNode.Parse(tStr, ref index);
            }

        }

    }
}
