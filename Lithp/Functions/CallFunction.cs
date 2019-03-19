using System;
using System.Collections.Generic;
using System.Linq;
using Lithp.Lex;

namespace Lithp.Functions
{
    public class CallFunction : Function
    {
        private readonly CustomFunctionTable _customFunctionTable;

        public override bool DeferChildExecution { get; } = true;

        public override string Identifier { get; } = "call";
        public override int? MinArgCount { get; } = 2;
        public override int? MaxArgCount { get; } = null;

        public CallFunction(ScopeManager scopeManager, CustomFunctionTable customFunctionTable) : base(scopeManager)
        {
            _customFunctionTable = customFunctionTable ?? throw new ArgumentNullException(nameof(customFunctionTable));
        }

        public override object Execute(object[] args)
        {
            var idToken = (IdentifierToken) ((Expression) args[0]).Token;

            var funcList = _customFunctionTable.Get(idToken.Value);
            var funcParams = funcList[0];
            var funcBody = funcList[1];

            InitialiseLocals((LithpList)args[1], funcParams);

            funcBody.Evaluate();

            object result = null;
            if (ScopeManager.GetCurrentScope().Contains(ReturnFunction.ReturnVarName))
                result = ScopeManager.GetCurrentScope().Get(ReturnFunction.ReturnVarName);
            
            ScopeManager.LeaveScope();

            return result;
        }

        private void InitialiseLocals(LithpList args, LithpList funcParams)
        {
            var vars = new Dictionary<string, object>();
            for (var i = 0; i < args.Items.Count; i++)
            {
                var paramName = (IdentifierToken) ((Expression) funcParams.Items[i].Value()).Token;

                object argValue = args.Items[i].Evaluate();

                vars.Add(paramName.Value, argValue);
            }

            ScopeManager.EnterScope();

            foreach (var variable in vars)
                ScopeManager.GetCurrentScope().Add(variable.Key, variable.Value);
        }
    }
}