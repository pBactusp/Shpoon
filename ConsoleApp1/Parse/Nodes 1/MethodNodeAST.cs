using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_1
{
    public class MethodNodeAST : NodeAST
    {
        /// <summary>
        /// Assumes the first line is the declaration line.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static MethodNodeAST Parse(List<List<Token>> lines)
        {
            MethodNodeAST methodNode = ParseDeclaration(lines[0]);

            methodNode.ParseBody(lines.GetRange(1));

            return methodNode;
        }
        private static MethodNodeAST ParseDeclaration(List<Token> line)
        {
            int index = line.FindIndex(t => t.Type == TokenType.typeSpecifier);
            MethodNodeAST methodNode = new MethodNodeAST(line[index], line[index + 1]);
            index += 2;

            //int paramsStartIndex = 1 + line.FindNextIndex(index, t => t.Type == TokenType.cBraceOpen);
            //index = line.FindNextIndex(paramsStartIndex, t => t.Type == TokenType.cBraceClose);
            //methodNode._parameters = ParseParameters(line.GetRangeBetween(paramsStartIndex, index));

            index = line.FindNextIndex(index, t => t.Type == TokenType.rBraceOpen);
            var expressionRange = line.GetRangeContained(index, t => t.Type == TokenType.rBraceOpen, t => t.Type == TokenType.rBraceClose);

            methodNode._parameters = ParseParameters(expressionRange);

            return methodNode;
        }

        /// <summary>
        /// Assumes line isn't wrapped in parentheses.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static Dictionary<string, (string, int)> ParseParameters(List<Token> line)
        {
            //                             < name , (type, index)>
            var parameters = new Dictionary<string, (string, int)>();

            if (line.Count > 0)
            {
                var parameterTokens = line.Split(t => t.Type == TokenType.binaryOperator && t.Value == ",", false);

                for (int i = 0; i < parameterTokens.Count; i++)
                {
                    parameters.Add(parameterTokens[i][1].Value, (parameterTokens[i][0].Value, i));
                }
            }

            return parameters;
        }


        //                < name , (type, index)>
        private Dictionary<string, (string, int)> _parameters;
        private Dictionary<string, (string, int)> _localVars;
        //private NodeAST _return;

        private MethodNodeAST(Token type, Token name) : base(name)
        {
            ReturnType = type;

            _parameters = new Dictionary<string, (string, int)>();
        }

        public Token ReturnType { get; private set; }


        private void RegisterVariable(Token name, Token type)
        {
            _localVars.Add(name.Value, (type.Value, _localVars.Count));
        }

        private void ParseBody(List<List<Token>> lines)
        {
            _localVars = new Dictionary<string, (string, int)>();
            children = new List<NodeAST>();


            foreach (var line in lines)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    if (line[i].Type == TokenType.typeSpecifier)
                    {
                        RegisterVariable(line[i + 1], line[i]);
                    }
                    else if (line[i].Type == TokenType.binaryOperator)
                    {
                        var expressionRange = line.GetRangeWhile(i - 1, t => t.Type != TokenType.lineEnd);

                        AddChild(ExpressionNodeAST.Parse(expressionRange));

                        i += expressionRange.Count;
                    }
                    else if (line[i].Type == TokenType.cBraceClose) //TokenType.statement)
                    {
                        NodeAST statementNode = new NodeAST(line[i]);

                        List<Token> expressionRange = null;

                        if (line[i + 1].Type == TokenType.rBraceOpen)
                            expressionRange = line.GetRangeContained(i + 1, t => t.Type == TokenType.rBraceOpen, t => t.Type == TokenType.rBraceClose);
                        else
                            expressionRange = line.GetRangeWhile(i + 1, t => t.Type != TokenType.lineEnd);

                        statementNode.AddChild(ExpressionNodeAST.Parse(expressionRange));

                        AddChild(statementNode);

                        i += expressionRange.Count;
                    }
                    else if (line[i].Type == TokenType.identifier)
                    {
                        NodeAST identifierNode = new NodeAST(line[i]);
                        AddChild(identifierNode);

                        if (line[i + 1].Type == TokenType.rBraceOpen)
                        {
                            var expressionRange = line.GetRangeContained(i + 1, t => t.Type == TokenType.rBraceOpen, t => t.Type == TokenType.rBraceClose);
                            identifierNode.AddChild(ExpressionNodeAST.Parse(expressionRange));

                            i += 1 + expressionRange.Count;
                        }
                    }

                }
            }
        }
    }
}
