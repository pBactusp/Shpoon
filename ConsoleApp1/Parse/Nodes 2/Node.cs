using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public abstract class Node
    {


        public virtual string ToString(string prevIndent)
        {
            return prevIndent + ToString();
        }
    }
}
