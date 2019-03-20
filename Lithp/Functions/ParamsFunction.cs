using System;
using System.Linq;

namespace Lithp.Functions
{
    public class ParamsFunction : Function
    {
        public override string Identifier { get; } = "params";
        public override int? MinArgCount { get; }
        public override int? MaxArgCount { get; }

        public ParamsFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            return args.Select(a => a().ToString()).ToArray();
        }
    }
}