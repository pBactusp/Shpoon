﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shpoon.Parse.Nodes_2
{
    public abstract class LoopStatementNode : StatementNode
    {
        public ExpressionNode Condition { get; protected set; }
        public StatementNode Body { get; protected set; }
    }
}
