using System;
using System.Collections.Generic;
using Lithp.Lex;

namespace Lithp
{
    public class Interpreter
    {
        private readonly Lexer _lexer;

        private readonly SystemFunctionTable _sysFuncs;
        private readonly ScopeManager _scopeManager;

        public Interpreter(Lexer lexer, Scope globalScope)
        {
            if (globalScope == null) throw new ArgumentNullException(nameof(globalScope));

            _lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
            _scopeManager = new ScopeManager(globalScope, new Stack<Scope>());

            _sysFuncs = new SystemFunctionTable(_scopeManager);
        }

        public void Run()
        {
            IToken token = null;
            try
            {
                do
                {
                    token = _lexer.Eat();

                    if (token.Kind != TokenKind.Eof)
                    {
                        if (token.Kind != TokenKind.StartExpression)
                            throw new InvalidOperationException(
                                $"Expected start of expression but encountered '{token}'");

                        var list = EatList();
                        list.Evaluate();
                    }
                } while (token.Kind != TokenKind.Eof);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Execution error: {ex.Message} [{token}]");
                throw;
            }
        }

        private LithpList EatList()
        {
            var token = _lexer.Eat();
            if (token is IdentifierToken idToken && _sysFuncs.TryGet(idToken.Value, out var sysFunc))
                token = _lexer.Eat();
            else
                sysFunc = _sysFuncs.Get("list");

            var result = new LithpList(sysFunc);
            do
            {
                switch (token.Kind)
                {
                    case TokenKind.StartExpression:
                        result.Add(new LithpListItem(EatList()));
                        break;
                    case TokenKind.EndExpression:
                        return result;

                    case TokenKind.DoubleLiteral:
                    case TokenKind.IntegerLiteral:
                    case TokenKind.StringLiteral:
                    case TokenKind.Identifier:
                        result.Add(new LithpListItem(new Expression(_scopeManager, token)));
                        break;

                    default:
                        throw new InvalidOperationException($"Unexpected token: {token}");
                }

                token = _lexer.Eat();

            } while (token.Kind != TokenKind.Eof);

            throw new InvalidOperationException("Statement not closed");
        }
    }
}