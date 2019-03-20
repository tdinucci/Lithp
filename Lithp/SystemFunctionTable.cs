using System;
using System.Collections.Generic;
using Lithp.Functions;

namespace Lithp
{
    public class SystemFunctionTable
    {
        private readonly Dictionary<string, Function> _functions;

        public SystemFunctionTable(ScopeManager scopeManager, CustomFunctionTable customFunctionTable)
        {
            _functions = new Dictionary<string, Function>
            {
                {"+", new AddFunction(scopeManager)},
                {"-", new SubtractFunction(scopeManager)},
                {"*", new MultiplyFunction(scopeManager)},
                {"/", new DivideFunction(scopeManager)},
                {"=", new EqualFunction(scopeManager)},
                {"!=", new NotEqualFunction(scopeManager)},
                {">", new GreaterThanFunction(scopeManager)},
                {"<", new LessThanFunction(scopeManager)},
                {"print", new PrintFunction(scopeManager)},
                {"concat", new ConcatFunction(scopeManager)},
                {"def", new DefFunction(scopeManager)},
                {"func", new DefFuncFunction(scopeManager, customFunctionTable)},
                {"call", new CallFunction(scopeManager)},
                {"if", new IfFunction(scopeManager)},
                {"list", new ListFunction(scopeManager)},
                {"params", new ParamsFunction(scopeManager)},
                {"return", new ReturnFunction(scopeManager)}
            };
        }

        public Function Get(string name)
        {
            if (!_functions.TryGetValue(name, out var func))
                throw new InvalidOperationException($"Unknown function '{name}'");

            return func;
        }

        public bool TryGet(string name, out Function function)
        {
            return _functions.TryGetValue(name, out function);
        }
    }
}