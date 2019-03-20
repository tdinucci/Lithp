using System;
using System.Collections;

namespace Lithp.Functions
{
    public class LessThanFunction : EqualityFunction
    {
        public override string Identifier { get; } = "<";

        public LessThanFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            var result = Comparer.Default.Compare(args[0](), args[1]());
            return result < 0;
        }
    }
}