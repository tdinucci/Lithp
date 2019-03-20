using System;

namespace Lithp.Functions
{
    public class IfFunction : Function
    {
        public override bool DeferChildExecution { get; } = true;

        public override string Identifier { get; } = "if";
        public override int? MinArgCount { get; } = 2;
        public override int? MaxArgCount { get; } = 3;

        public IfFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            var arg1 = args[0]();
            if (!(arg1 is bool testResult))
            {
                throw new InvalidOperationException(
                    $"Expected first argument to evaluate to a boolean value but it was '{args[0]}'");
            }

            if (testResult)
                args[1]();
            else if (args.Length == 3)
                args[2]();

            return null;
        }
    }
}