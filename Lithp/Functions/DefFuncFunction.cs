using System;
using Lithp.Lex;

namespace Lithp.Functions
{
    public class DefFuncFunction : Function
    {
        private readonly CustomFunctionTable _customFunctionTable;

        public override bool DeferChildExecution { get; } = true;

        public override string Identifier { get; } = "func";
        public override int? MinArgCount { get; } = 3;
        public override int? MaxArgCount { get; } = 3;

        public DefFuncFunction(ScopeManager scopeManager, CustomFunctionTable customFunctionTable) : base(scopeManager)
        {
            _customFunctionTable = customFunctionTable ?? throw new ArgumentNullException(nameof(customFunctionTable));
        }

        public override object Execute(Func<object>[] args)
        {
            var funcName = args[0]() as string;
            var funcParams = args[1]() as string[];
            var funcBody = args[2];

            var customFunc = new CustomFunction(funcName, funcParams, funcBody);
            _customFunctionTable.Add(customFunc);

            return null;
        }
    }
}