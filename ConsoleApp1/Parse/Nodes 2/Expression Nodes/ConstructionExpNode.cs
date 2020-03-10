using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ConstructionExpNode : MethodCallExpNode
    {
        public string ClassName;

        public ConstructionExpNode(string className) : base("ctor")
        {
            ClassName = className;
        }


        public override string ToString()
        {
            return "new " + ClassName + '(' + string.Join(", ", arguments) + ")";
        }
    }

}
