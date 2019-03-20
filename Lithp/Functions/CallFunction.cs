using System;
using System.Collections.Generic;

namespace Lithp.Functions
{
    public class CallFunction : Function
    {
        public override bool DeferChildExecution { get; } = true;

        public override string Identifier { get; } = "call";
        public override int? MinArgCount { get; } = 2;
        public override int? MaxArgCount { get; } = null;

        public CallFunction(ScopeManager scopeManager) : base(scopeManager)
        {
        }

        public override object Execute(Func<object>[] args)
        {
            var func = (CustomFunction) args[0]();

            try
            {
                var callArgsObj = args[1]();
                if (!(callArgsObj is object[] callArgs))
                {
                    throw new InvalidOperationException(
                        $"Malformed called to '{func.Name}'.  Expected a list of arguments but received '{callArgsObj}'");
                }

                if (callArgs.Length != func.Parameters.Length)
                {
                    throw new InvalidOperationException(
                        $"'{func.Name}' expects {func.Parameters.Length} arguments but is being called with {callArgs.Length}");
                }

                InitialiseLocals(func.Parameters, callArgs);

                func.Execute();

                object result = null;
                if (ScopeManager.Contains(ReturnFunction.ReturnVarName))
                    result = ScopeManager.Get(ReturnFunction.ReturnVarName);

                ScopeManager.LeaveScope();

                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error executing function '{func.Name}': {ex.Message}");
            }
        }

        private void InitialiseLocals(string[] parameters, object[] arguments)
        {
            var vars = new Dictionary<string, object>();
            for (var i = 0; i < parameters.Length; i++)
            {
                vars.Add(parameters[i], arguments[i]);
            }

            ScopeManager.EnterScope();

            foreach (var variable in vars)
                ScopeManager.Add(variable.Key, variable.Value);
        }
    }
}