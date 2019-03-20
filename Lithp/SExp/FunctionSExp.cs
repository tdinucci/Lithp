using System;
using System.Collections.Generic;
using Lithp.Functions;

namespace Lithp.SExp
{
    public class FunctionSExp : SExp
    {
        public Function Function { get; }

        public FunctionSExp(Function function, int line, int column, IList<SExpNode> nodes) : base(line, column, nodes)
        {
            Function = function ?? throw new ArgumentNullException(nameof(function));
        }
    }
}