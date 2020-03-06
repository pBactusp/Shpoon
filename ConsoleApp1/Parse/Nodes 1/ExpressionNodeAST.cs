using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shpoon.Lex;

namespace Shpoon.Parse.Nodes_1
{
    public class ExpressionNodeAST : NodeAST
    {
        public static NodeAST Parse(List<Token> line)
        {
            if (line[0].Type == TokenType.rBraceOpen && line.Last().Type == TokenType.rBraceClose)
            {
                var trimmedLine = line.GetRangeContained(0, t => t.Type == TokenType.rBraceOpen, t => t.Type == TokenType.rBraceClose);
                if (trimmedLine.Count == line.Count - 2)
                    line = trimmedLine;
            }

            if (line.Count == 1)
                return new NodeAST(line[0]);

            int bracNum = 0;

            Token opToken = null;
            int opPriority = -1;

            // Get all operators
            for (int i = line.Count - 1; i >= 0; i--)
            {
                if (line[i].Type == TokenType.rBraceOpen)
                    bracNum++;
                else if (line[i].Type == TokenType.rBraceClose)
                    bracNum--;
                else if (bracNum == 0 && line[i].Type == TokenType.binaryOperator)
                {
                    int tempOPPriority = Parser.GetOperatorPriority(line[i], opPriority);

                    if (tempOPPriority > opPriority)
                    {
                        opToken = line[i];
                        opPriority = tempOPPriority;
                    }
                }
            }

            // Get highest priority operator

            if (opToken == null)
                return null;

            ExpressionNodeAST node = new ExpressionNodeAST(opToken);
            int nodeIndex = line.IndexOf(node.Token);

            node.Left = ExpressionNodeAST.Parse(line.GetRange(0, nodeIndex));
            node.Right = ExpressionNodeAST.Parse(line.GetRange(nodeIndex + 1));

            return node;
        }



        private ExpressionNodeAST(Token token) : base(token)
        {
            children.Add(null);
            children.Add(null);
        }

        public NodeAST Left
        {
            get { return children[0]; }
            set { children[0] = value; }
        }
        public NodeAST Right
        {
            get { return children[1]; }
            set { children[1] = value; }
        }

    }
}
