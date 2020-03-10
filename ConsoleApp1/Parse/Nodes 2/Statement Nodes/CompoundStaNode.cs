using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class CompoundStaNode : StatementNode
    {
        public static CompoundStaNode Parse(TokenString tStr, ref int index)
        {
            if (tStr.Match(index, TokenType.cBraceOpen))
                index++;

            CompoundStaNode node = new CompoundStaNode();

            while (!tStr.Match(index, TokenType.cBraceClose))
            {
                var statementNode = Sta.Parse(tStr, ref index);

                if (statementNode != null)
                    node.AddStatement(statementNode);
                else
                    index++;
            }

            index++;
            return node;
        }


        private List<StatementNode> statements;

        private CompoundStaNode()
        {
            statements = new List<StatementNode>();
        }

        public StatementNode this[int index]
        {
            get { return statements[index]; }
            set { statements[index] = value; }
        }


        public void AddStatement(StatementNode statement)
        {
            statements.Add(statement);
        }

        public override string ToString(string prevIndent)
        {
            string indent = prevIndent + "    ";
            StringBuilder ret = new StringBuilder();
            ret.AppendLine(prevIndent + "{");

            //for (int i = 0; i < statements.Count - 1; i++)
            //{
            //    ret.Append(statements[i].ToString(indent));
            //}

            foreach (var sta in statements)
                ret.AppendLine(sta.ToString(indent));

            ret.AppendLine(prevIndent + "}");

            return ret.ToString();
        }
    }
}
