using System;
using Lithp.Lex;

namespace Lithp.Functions
{
    public class DefFunction : Function
    {
        public override bool DeferChildExecution { get; } = true;

        public override string Identifier { get; } = "def";
        public override int? MinArgCount { get; } = 2;
        public override int? MaxArgCount { get; } = 2;

        public DefFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            var arg1 = args[0]();
            if (arg1 is string varName)
            {
                var varValue = args[1]();

                ScopeManager.Add(varName, varValue);
                return null;
            }

            throw new InvalidOperationException($"Expected 1st arg to be an identifier but it was '{arg1}'");
        }
    }
}