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

        public override object Execute(object[] args)
        {
            if (args[0] is Expression funcExp && funcExp.Token is IdentifierToken funcId &&
                args[1] is LithpList funcArgs && args[2] is LithpList funcBody)
            {
                _customFunctionTable.Add(funcId.Value, new[] {funcArgs, funcBody});
                return null;
            }

            throw new InvalidOperationException($"Invalid function definition '{args[0]}'");
        }
    }
}