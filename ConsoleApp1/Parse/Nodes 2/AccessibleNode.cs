using Shpoon.Lex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class AccessibleNode : Node
    {
        public static List<string> GetAccessors(TokenString tStr, ref int index)
        {
            var accessors = new List<string>();

            while (tStr[index].Type == TokenType.accessor)
            {
                accessors.Add(tStr[index].Value);
                index++;
            }

            return accessors;
        }

        public List<string> Accessors;

        protected AccessibleNode(List<string> accessors)
        {
            Accessors = accessors;
        }

        protected string AccessorsToString(string separator = " ", bool addSeparatorAtEnd = true)
        {
            string ret = string.Join(separator, Accessors);

            if (ret.Length > 0 && addSeparatorAtEnd)
                ret += separator;

            return ret;
        }
    }
}
