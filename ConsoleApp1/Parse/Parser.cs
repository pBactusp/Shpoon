using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shpoon.Lex;
using Shpoon.Parse.Nodes_2;

namespace Shpoon.Parse
{
    public class Parser
    {
        public static string LocalVarName = "V_";
        public static string ArgumentName = "A_";


        private static List<string[]> operatorsPriority = new List<string[]>()
        {
            new string[]{"."},
            new string[]{"*", "/"},
            new string[]{"+", "-"},
            new string[]{"<", ">"},
            new string[]{"="},
            new string[]{"++", "--"}
        };


        public NamespaceNode GlobalNamespace;

        public Parser()
        {
            GlobalNamespace = null;
        }

        public NamespaceNode Parse(TokenString tStr, ref int index)
        {
            GlobalNamespace = NamespaceNode.CreateGlobalNamespaceNode();

            while (index < tStr.Count)
            {
                // try parsing namespace. if not null, add to GlobalNamespace
                NamespaceNode node = NamespaceNode.Parse(tStr, ref index);

                if (node != null)
                    GlobalNamespace.AddNamespace(node);
                else
                    index++;
            }

            return GlobalNamespace;
        }



        public static int GetOperatorPriority(Token operatorToken, int startPriority = 0)
        {
            startPriority = startPriority < 0 ? 0 : startPriority;

            for (int i = startPriority; i < operatorsPriority.Count; i++)
                if (operatorsPriority[i].Contains(operatorToken.Value))
                    return i;

            return -1;
        }
        public static int GetOperatorPriority(string operatorString, int startPriority = 0)
        {
            startPriority = startPriority < 0 ? 0 : startPriority;

            for (int i = startPriority; i < operatorsPriority.Count; i++)
                if (operatorsPriority[i].Contains(operatorString))
                    return i;

            return -1;
        }


        //public static bool IsExpression(Token token)
        //{
        //    if (token.Type == Token)
        //}

    }
}
