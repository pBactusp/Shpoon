﻿using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class ClassNode : Node
    {
        public static ClassNode Parse(TokenString tStr, ref int index)
        {
            if (!tStr.Match(index, TokenType.@class, TokenType.identifier, TokenType.cBraceOpen))
                return null;

            int startIndex = index;

            index++;

            ClassNode node = new ClassNode(tStr[index].Value);

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
                if (internalRange.Match(iRangeIndex, TokenType.typeSpecifier, TokenType.identifier))
                {
                    if (internalRange.Match(iRangeIndex + 2, TokenType.rBraceOpen))
                        node.AddMethod(MethodNode.Parse(internalRange, ref iRangeIndex));
                    else
                        node.AddField(FieldNode.Parse(internalRange, ref iRangeIndex));
                }
                else
                    iRangeIndex++;
            }

            index += iRangeIndex;
            return node;
        }


        private List<FieldNode> fields;
        private List<MethodNode> methods;


        public ClassNode(string name) : base()
        {
            Name = name;
            fields = new List<FieldNode>();
            methods = new List<MethodNode>();

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


        public override string ToString(string prevIndent)
        {
            string indent = prevIndent + "    ";
            string ret = prevIndent + "class " + Name + Environment.NewLine + prevIndent + '{' + Environment.NewLine;

            if (fields.Count > 0)
            {
                foreach (var field in fields)
                    ret += field.ToString(indent) + Environment.NewLine;

                ret += Environment.NewLine;
            }

            foreach (var method in methods)
                ret += method.ToString(indent) + Environment.NewLine;

            ret += prevIndent + '}' + Environment.NewLine;

            return ret;
        }
    }
}