using System;
using Lithp.Functions;
using Lithp.Lex;
using Lithp.SExp;

namespace Lithp
{
    public class Interpreter
    {
        private readonly Parser _parser;
        private readonly ScopeManager _scopeManager;
        private readonly SystemFunctionTable _systemFunctionTable;
        private readonly CustomFunctionTable _customFunctionTable;

        public Interpreter(Parser parser, ScopeManager scopeManager, SystemFunctionTable systemFunctionTable,
            CustomFunctionTable customFunctionTable)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _scopeManager = scopeManager ?? throw new ArgumentNullException(nameof(scopeManager));
            _systemFunctionTable = systemFunctionTable ?? throw new ArgumentNullException(nameof(systemFunctionTable));
            _customFunctionTable = customFunctionTable ?? throw new ArgumentNullException(nameof(customFunctionTable));
        }

        public void Execute()
        {
            SExp.SExp exp;
            while ((exp = _parser.Eat()) != null)
            {
                if (!(exp is FunctionSExp fexp))
                    throw new InvalidOperationException($"Expected a function expression but encountered: {exp}");

                ExecuteFunction(fexp);
            }
        }

        private object ExecuteFunction(FunctionSExp fexp)
        {
            var args = new Func<object>[fexp.Nodes.Count];
            if (fexp.Nodes.Count > 0)
            {
                for (var i = 0; i < fexp.Nodes.Count; i++)
                {
                    var node = fexp.Nodes[i];

                    Func<object> nodeValueFunc;
                    switch (node)
                    {
                        case FunctionSExp childFExp:
                            nodeValueFunc = () => ExecuteFunction(childFExp);
                            break;

                        case Atom childAtom:
                            if (!(fexp.Function is DefFunction) && !(fexp.Function is DefFuncFunction) &&
                                !(fexp.Function is ParamsFunction) &&
                                childAtom.Token is IdentifierToken idToken)
                            {
                                nodeValueFunc = () =>
                                {
                                    if (_scopeManager.Contains(idToken.Value))
                                        return _scopeManager.Get(idToken.Value);

                                    if (_customFunctionTable.Contains(idToken.Value))
                                        return _customFunctionTable.GetCustomFunction(idToken.Value);

                                    if (_systemFunctionTable.TryGet(idToken.Value, out var sysFunc))
                                        return sysFunc;

                                    throw new InvalidOperationException($"Unknown identifier '{idToken}'");
                                };
                            }
                            else
                                nodeValueFunc = () => childAtom.Token.Value;

                            break;

                        // this will be an empty parameter list
                        case SExp.SExp _:
                            nodeValueFunc = () => new object[0];
                            break;

                        default:
                            throw new InvalidOperationException($"Expected a function or atom but encountered: {node}");
                    }

                    args[i] = nodeValueFunc;
                }
            }

            CheckCallingArgs(fexp.Function, args);

            return fexp.Function.Execute(args);
        }

        private void CheckCallingArgs(Function function, Func<object>[] args)
        {
            if (args.Length < function.MinArgCount.GetValueOrDefault(0))
            {
                throw new InvalidOperationException(
                    $"'{function.Identifier}' expects at least {function.MinArgCount} arguments but {args.Length} where provided");
            }

            if (args.Length > function.MaxArgCount.GetValueOrDefault(int.MaxValue))
            {
                throw new InvalidOperationException(
                    $"'{function.Identifier}' expects at most {function.MaxArgCount} arguments but {args.Length} where provided");
            }
        }
    }
}