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

        public override object Execute(object[] args)
        {
            if (args[0] is Expression decExpression)
            {
                if (decExpression.Token is IdentifierToken idToken)
                {
                    object value = null;
                    if (args[1] is Expression valExpression)
                        value = valExpression.Evaluate();
                    else if (args[1] is LithpList valList)
                        value = valList.Evaluate();
                    else
                        throw new InvalidOperationException("Expected 2nd argument to be an expression");

                    ScopeManager.GetCurrentScope().Add(idToken.Value, value);
                    return null;
                }
            }

            throw new InvalidOperationException("Expected 1st arg to be an identifier but");
        }
    }
}