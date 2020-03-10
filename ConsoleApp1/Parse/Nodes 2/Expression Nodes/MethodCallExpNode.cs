using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class MethodCallExpNode : ExpressionNode
    {
        protected List<ExpressionNode> arguments;

        public MethodCallExpNode(string name)
        {
            Name = name;
            arguments = new List<ExpressionNode>();
        }

        public string Name { get; protected set; }
        public ExpressionNode this[int index]
        {
            get { return arguments[index]; }
            set { arguments[index] = value; }
        }

        public void AddArgument(ExpressionNode argument)
        {
            arguments.Add(argument);
        }


        //public override string ToString(string prevIndent)
        //{
        //    return prevIndent + Name + '(' + string.Join(", ", arguments) + ");";
        //}

        public override string ToString()
        {
            return Name + '(' + string.Join(", ", arguments) + ");";
        }

    }
}
