using System;

namespace Lithp.Functions
{
    public abstract class MathsFunction : Function
    {
        public override int? MinArgCount { get; } = 2;
        public override int? MaxArgCount { get; } = null;

        protected abstract double ApplyOperator(double running, double operand);

        protected MathsFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            double result = 0;
            var isDoubleResult = false;
            for (var i = 0; i < args.Length; i++)
            {
                var arg = args[i]();
                switch (arg)
                {
                    case int intVal:
                        result = i == 0 ? intVal : ApplyOperator(result, intVal);
                        break;
                    case double doubleVal:
                        result = i == 0 ? doubleVal : ApplyOperator(result, doubleVal);
                        isDoubleResult = true;
                        break;
                    default:
                        throw new InvalidOperationException($"Unexpected argument type given to '{Identifier}': {arg}");
                }
            }

            if (!isDoubleResult)
                return Convert.ToInt32(result);

            return result;
        }
    }
}