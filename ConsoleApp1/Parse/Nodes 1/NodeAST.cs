using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shpoon.Lex;

namespace Shpoon.Parse.Nodes_1
{
    public class NodeAST
    {
        protected List<NodeAST> children;

        protected NodeAST()
        {
            children = new List<NodeAST>();
        }
        public NodeAST(Token token)
        {
            Token = token;
            children = new List<NodeAST>();
        }

        public Token Token { get; private set; }
        public NodeAST Parent { get; set; }
        public NodeAST this[int index]
        {
            get { return children[index]; }
            set { children[index] = value; }
        }

        public void AddChild(Token token)
        {
            var node = new NodeAST(token)
            {
                Parent = this
            };
            AddChild(node);
        }
        public void AddChild(NodeAST node)
        {
            node.Parent = this;
            children.Add(node);
        }


        public override string ToString()
        {
            string ret = '{' + Token.ToString() + ' ';

            foreach (var child in children)
                if (child == null)
                    ret += "{null} ";
                else
                    ret += child.ToString();

            ret += "} ";

            return ret;
        }
    }
}
