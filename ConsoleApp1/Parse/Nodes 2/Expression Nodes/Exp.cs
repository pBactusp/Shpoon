using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public static class Exp
    {
        public static ExpressionNode Parse(TokenString tStr)
        {
            if (tStr[0].Type == TokenType.rBraceOpen && tStr.Last().Type == TokenType.rBraceClose)
            {
                var trimmedLine = tStr.GetRangeInBrackets(0);

                if (trimmedLine != null)
                    tStr = trimmedLine;
            }

            if (tStr.Last().Type == TokenType.lineEnd)
                tStr.RemoveAt(tStr.Count - 1);

            if (tStr.Count == 1)
            {
                if (tStr[0].Type == TokenType.identifier)
                    return new IdentifierExpNode(tStr[0].Value);
                else if (tStr[0].Type == TokenType.constant)
                    return new LiteralExpNode("INTEGER", tStr[0].Value);
                else if (tStr[0].Type == TokenType.@string)
                    return new LiteralExpNode("STRING", tStr[0].Value);
            }


            if (tStr[0].Type == TokenType.typeSpecifier || tStr[0].Type == TokenType.identifier)
            {
                if (tStr[1].Type == TokenType.identifier)
                {
                    VariableDefExpNode vdNode = new VariableDefExpNode(tStr[0].Value);

                    var defs = tStr.GetRangeUntil(1, TokenType.lineEnd).Split(false, TokenType.comma);

                    foreach (var defTStr in defs)
                    {
                        string varName = defTStr[0].Value;
                        var varDef = Exp.Parse(defTStr);

                        vdNode.AddVariable(varName, varDef);
                    }

                    return vdNode;
                }
            }

            if ((tStr[0].Type == TokenType.prepostOperator || tStr[0].Type == TokenType.preOperator) && tStr[1].Type == TokenType.identifier)
            {
                return new PreUnaryExpNode(tStr[0].Value, new TokenString(tStr[1]));
            }
            if (tStr[0].Type == TokenType.identifier && (tStr[1].Type == TokenType.prepostOperator || tStr[1].Type == TokenType.postOperator))
            {
                return new PostUnaryExpNode(tStr[1].Value, new TokenString(tStr[0]));
            }

            int bracNum = 0;

            Token opToken = null;
            int opPriority = -1;

            // Get all operators
            for (int i = tStr.Count - 1; i >= 0; i--)
            {
                if (tStr[i].Type == TokenType.rBraceOpen)
                    bracNum++;
                else if (tStr[i].Type == TokenType.rBraceClose)
                    bracNum--;
                else if (bracNum == 0 && (tStr[i].Type == TokenType.binaryOperator || tStr[i].Type == TokenType.accessOperator))
                {
                    int tempOPPriority = Parser.GetOperatorPriority(tStr[i], opPriority);

                    if (tempOPPriority > opPriority)
                    {
                        opToken = tStr[i];
                        opPriority = tempOPPriority;
                    }
                }
            }


            if (opToken == null)
            {
                if (tStr.Match(0, TokenType.@new, TokenType.identifier, TokenType.rBraceOpen))
                {
                    ConstructionExpNode ceNode = new ConstructionExpNode(tStr[1].Value);

                    var argRangesUnsplit = tStr.GetRangeInBrackets(2);
                    
                    if (argRangesUnsplit.Count > 0)
                    {
                        var argRanges = argRangesUnsplit.Split(false, TokenType.comma);

                        foreach (var argRange in argRanges)
                            ceNode.AddArgument(Exp.Parse(argRange));
                    }

                    return ceNode;
                }
                else if (tStr.Match(0, TokenType.identifier, TokenType.rBraceOpen))
                {
                    MethodCallExpNode mceNode = new MethodCallExpNode(tStr[0].Value);

                    var argRangesUnsplit = tStr.GetRangeInBrackets(1);
                    
                    if (argRangesUnsplit.Count > 0)
                    {
                        var argRanges = argRangesUnsplit.Split(false, TokenType.comma);

                        foreach (var argRange in argRanges)
                            mceNode.AddArgument(Exp.Parse(argRange));
                    }

                    return mceNode;
                }

                return null;
            }

            int tIndex = tStr.IndexOf(opToken);

            var left = tStr.GetRangeTStr(0, tIndex);
            var right = tStr.GetRangeTStr(tIndex + 1);

            ExpressionNode node = null;

            switch (opToken.Type)
            {
                case TokenType.binaryOperator:
                    node = new BinaryExpNode(opToken.Value, left, right);
                    break;
                case TokenType.accessOperator:
                    node = new MemberExpNode(opToken.Value, left, right);
                    break;

                //case TokenType.preOperator:
                //    node = new PreUnaryExpNode(opToken.Value, right);
                //    break;
                //case TokenType.postOperator:
                //    node = new PostUnaryExpNode(opToken.Value, left);
                //    break;
                //case TokenType.prepostOperator:
                //    if (left.Count == 0)
                //        node = new PreUnaryExpNode(opToken.Value, right);
                //    else
                //        node = new PostUnaryExpNode(opToken.Value, left);
                //    break;

                default:
                    break;
            }

            return node;
        }
    }
}
