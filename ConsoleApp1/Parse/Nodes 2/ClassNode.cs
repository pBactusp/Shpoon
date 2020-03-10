using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ClassNode : AccessibleNode
    {
        public static ClassNode Parse(TokenString tStr, ref int index)
        {
            var accessors = AccessibleNode.GetAccessors(tStr, ref index);

            if (!tStr.Match(index, TokenType.@class, TokenType.identifier, TokenType.cBraceOpen))
                return null;

            int startIndex = index;

            index++;

            ClassNode node = new ClassNode(tStr[index].Value, accessors);

            index++;

            if (tStr[index].Type != TokenType.cBraceOpen)
            {
                index = startIndex;
                return null;
            }

            // Get internals
            var internalRange = tStr.GetRangeInBrackets(ref index);
            int iRangeIndex = 0;

            while (iRangeIndex < internalRange.Count)
            {
                Node tempNode;


                if ((tempNode = CtorNode.Parse(node.Name, internalRange, ref iRangeIndex)) != null)
                {
                    node.AddCtor(tempNode as CtorNode);
                }
                else if ((tempNode = MethodNode.Parse(internalRange, ref iRangeIndex)) != null)
                {
                    node.AddMethod(tempNode as MethodNode);
                }
                else if ((tempNode = FieldNode.Parse(internalRange, ref iRangeIndex)) != null)
                {
                    node.AddField(tempNode as FieldNode);
                }
                else
                    iRangeIndex++;

                //if (internalRange.Match(iRangeIndex, TokenType.typeSpecifier, TokenType.identifier))
                //{
                //    if (internalRange.Match(iRangeIndex + 2, TokenType.rBraceOpen))
                //        node.AddMethod(MethodNode.Parse(internalRange, ref iRangeIndex));
                //    else
                //        node.AddField(FieldNode.Parse(internalRange, ref iRangeIndex));
                //}
                //else
                //    iRangeIndex++;
            }

            index += iRangeIndex;
            return node;
        }


        private List<FieldNode> fields;
        private List<MethodNode> methods;
        private List<CtorNode> ctors;


        public ClassNode(string name, List<string> accessors) : base(accessors)
        {
            Name = name;
            fields = new List<FieldNode>();
            methods = new List<MethodNode>();
            ctors = new List<CtorNode>();

            Namespace = null;
        }

        public string Name { get; private set; }
        public NamespaceNode Namespace { get; set; }

        public FieldNode this[int index]
        {
            get { return fields[index]; }
            set { fields[index] = value; }
        }

        public void AddField(FieldNode field)
        {
            field.Class = this;
            fields.Add(field);
        }
        public void AddMethod(MethodNode method)
        {
            method.Class = this;
            methods.Add(method);
        }
        public void AddCtor(CtorNode ctor)
        {
            ctor.Class = this;
            ctors.Add(ctor);
        }


        public override string ToString(string prevIndent)
        {
            string indent = prevIndent + "    ";
            StringBuilder ret = new StringBuilder();
            ret.Append(prevIndent);
            ret.Append(AccessorsToString());
            ret.Append("class " + Name + Environment.NewLine + prevIndent + '{' + Environment.NewLine);

            if (fields.Count > 0)
            {
                foreach (var field in fields)
                    ret.Append(field.ToString(indent) + Environment.NewLine);

                ret.AppendLine();
            }

            foreach (var method in methods)
                ret.Append(method.ToString(indent) + Environment.NewLine);

            ret.Append(prevIndent + '}' + Environment.NewLine);

            return ret.ToString();
        }
    }
}
