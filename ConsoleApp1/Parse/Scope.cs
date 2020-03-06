using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse
{
    public class Scope
    {
        //               <name, index>
        public Dictionary<string, int> LocalVariables = new Dictionary<string, int>();

        private List<Scope> children;

        public Scope(string name)
        {
            Name = name;
            children = new List<Scope>();

            Parent = null;
        }

        public string Name { get; }
        public string FullName
        {
            get { return Parent != null ? $"{Parent.FullName}.{Name}" : Name; }
        }
        public Scope Parent { get; private set; }
        public Scope this[string path]
        {
            get
            {
                string[] scopeNames = path.Split('.');

                Scope ret = this;

                foreach (string scopeName in scopeNames)
                {
                    ret = ret.children.Find(s => s.Name == scopeName);

                    if (ret == null)
                    {
                        throw new Exception($"Invalid scope '{scopeName}'.{Environment.NewLine}Scope.FullName: {path}");
                    }
                }

                return ret;
            }
        }

        public Scope AddScope()
        {
            int nameIndex = 0;
            string name = "scope_0";

            while (children.Any(s => s.Name == name))
            {
                nameIndex++;
                name = "scope_" + nameIndex;
            }

            Scope scope = new Scope(name);

            children.Add(scope);
            scope.Parent = this;

            return scope;
        }
        public Scope AddScope(string name)
        {
            Scope scope = new Scope(name);

            children.Add(scope);
            scope.Parent = this;

            return scope;
        }
        public Scope AddScope(Scope scope)
        {
            children.Add(scope);
            scope.Parent = this;

            return scope;
        }

        public void AddLocalVariable(string name)
        {
            LocalVariables.Add(name, LocalVariables.Count);
        }
    }
}
