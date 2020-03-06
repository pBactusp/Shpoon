﻿using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class NamespaceNode : Node
    {
        public static NamespaceNode Parse(TokenString tStr, ref int index)
        {
            int startIndex = index;

            if (tStr[index].Type != TokenType.@namespace)
                return null;

            index++;
            var nameExpression = Exp.Parse(tStr.GetRangeUntil(ref index, TokenType.cBraceOpen));

            if (nameExpression == null)
            {
                index = startIndex;
                return null;
            }

            NamespaceNode node = new NamespaceNode(nameExpression);

            // Get internals
            var classesRange = tStr.GetRangeInBrackets(ref index);

            int cRangeIndex = 0;

            while (cRangeIndex < classesRange.Count)
            {
                if (classesRange[cRangeIndex].Type == TokenType.@class)
                {
                    ClassNode classNode = ClassNode.Parse(classesRange, ref cRangeIndex);

                    if (classNode == null)
                        break;

                    node.AddClass(classNode);
                }
                else
                    cRangeIndex++;
            }

            index += cRangeIndex;
            return node;
        }



        private List<ClassNode> classes;

        private NamespaceNode(ExpressionNode nameExpression)
        {
            NameExpression = nameExpression;
            classes = new List<ClassNode>();
        }

        public string Name
        {
            get { return NameExpression.ToString(); }
        }
        public ExpressionNode NameExpression { get; private set; }
        public ClassNode this[int index]
        {
            get { return classes[index]; }
            set { classes[index] = value; }
        }

        public void AddClass(ClassNode classNode)
        {
            classNode.Namespace = this;
            classes.Add(classNode);
        }


        public override string ToString()
        {
            string indent = "    ";
            string ret = $"namespace {Name}" + Environment.NewLine
                + '{' + Environment.NewLine;


            foreach (var cls in classes)
                ret += cls.ToString(indent) + Environment.NewLine;

            ret += '}' + Environment.NewLine;

            return ret;
        }
    }
}
