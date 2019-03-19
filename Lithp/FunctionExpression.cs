//using System;
//using Lithp.Functions;
//
//namespace Lithp
//{
//    public class FunctionExpression : Expression
//    {
//        public Function Function { get; }
//        public Expression[] ArgExpressions { get; }
//
//        public FunctionExpression(ScopeManager scopeManager, IdentifierToken token, Function function,
//            Expression[] argExpressions) : base(scopeManager, token)
//        {
//            Function = function ?? throw new ArgumentNullException(nameof(function));
//            ArgExpressions = argExpressions ?? new Expression[0];
//
//            if (function.MinArgCount.HasValue && ArgExpressions.Length < function.MinArgCount)
//            {
//                throw new InvalidOperationException(
//                    $"{ArgExpressions.Length} argument(s) provided to '{function.Identifier}'.  Minimum of {function.MinArgCount} required");
//            }
//
//            if (function.MaxArgCount.HasValue && ArgExpressions.Length > function.MaxArgCount)
//            {
//                throw new InvalidOperationException(
//                    $"{ArgExpressions.Length} argument(s) provided to '{function.Identifier}'.  Maximum of {function.MaxArgCount} required");
//            }
//        }
//
//        public override object Evaluate()
//        {
//            return Evaluate(this, false, false);
//        }
//
//        private object Evaluate(Expression expression, bool inFuncDefinition, bool inIf, string indent = "")
//        {
//            Console.WriteLine($"{indent}{expression.GetType().Name} -> {expression}");
//
//            object result = null;
//            if (expression is FunctionExpression funcExp)
//            {
//                inIf = inIf || funcExp.Function is IfFunction;
//
//                foreach (var argExp in funcExp.ArgExpressions)
//                {
//                    var argResult = Evaluate(argExp, inFuncDefinition, inIf, indent + "\t");
//                }
//
//                if ((!inIf || funcExp.Function is IfFunction) &&
//                    (!inFuncDefinition ))
//                    result = funcExp.Function.Execute(funcExp.ArgExpressions);
//            }
//
//            return result;
//        }
//    }
//}