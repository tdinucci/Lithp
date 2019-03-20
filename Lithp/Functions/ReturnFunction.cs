using System;

namespace Lithp.Functions
{
    public class ReturnFunction : Function
    {
        public const string ReturnVarName = "***&&&Return&&&***";

        public override string Identifier { get; } = "return";
        public override int? MinArgCount { get; } = 1;
        public override int? MaxArgCount { get; } = 1;

        public ReturnFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            var result = args[0]();
            ScopeManager.Add(ReturnVarName, result);
            return result;
        }
    }
}