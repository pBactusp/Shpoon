﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public class MemberNode : AccessibleNode
    {
        public MemberNode(List<string> accessors) : base(accessors)
        {

        }
    }
}
