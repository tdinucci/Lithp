using System;
using System.Collections.Generic;
using System.Text;

namespace Lithp.SExp
{
    public class SExp : SExpNode
    {
        public IList<SExpNode> Nodes { get; }

        public SExp(int line, int column, IList<SExpNode> nodes) : base(line, column)
        {
            Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            foreach (var node in Nodes)
                result.AppendLine($"> {node}");

            return result.ToString();
        }
    }
}