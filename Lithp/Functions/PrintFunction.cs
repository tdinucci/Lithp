using System;

namespace Lithp.Functions
{
    public class PrintFunction : Function
    {
        public override string Identifier { get; } = "print";
        public override int? MinArgCount { get; } = 1;
        public override int? MaxArgCount { get; } = 1;

        public PrintFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            Console.WriteLine(args[0]());
            return null;
        }
    }
}