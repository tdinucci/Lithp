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

        public override object Execute(object[] args)
        {
            if (!(args[0] is LithpList eqList) || !(eqList.Function is EqualityFunction))
            {
                throw new InvalidOperationException(
                    $"Expected first argument to be an equality function but it was '{args[0]}'");
            }

            if ((bool) eqList.Evaluate())
            {
                if (!(args[1] is LithpList thenList))
                {
                    throw new InvalidOperationException(
                        $"Expected second argument to be a list but it was '{args[1]}'");
                }

                var result = thenList.Evaluate();
                return result;
            }

            if (args.Length == 3)
            {
                if (!(args[2] is LithpList elseList))
                {
                    throw new InvalidOperationException(
                        $"Expected third argument to be a list but it was '{args[2]}'");
                }

                return elseList.Evaluate();
            }

            return null;
        }
    }
}