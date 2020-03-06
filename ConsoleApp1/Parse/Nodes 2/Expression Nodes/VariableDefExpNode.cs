using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class VariableDefExpNode : ExpressionNode
    {
        private List<string> variables;
        private List<ExpressionNode> definitions;

        public VariableDefExpNode(string type)
        {
            Type = type;
            variables = new List<string>();
            definitions = new List<ExpressionNode>();
        }

        public string Type { get; private set; }


        public void AddVariable(string variableName, ExpressionNode definition)
        {
            variables.Add(variableName);
            definitions.Add(definition);
        }

        public override string ToString()
        {
            string ret = "";

            if (definitions.Count > 0)
            {
                ret += $"{Type} {definitions[0].ToString()}";

                for (int i = 1; i < definitions.Count; i++)
                    ret += $", {definitions[i].ToString()}";
            }

            return ret;
        }
    }
}
