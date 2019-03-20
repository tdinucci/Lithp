using System;
using System.Collections.Generic;
using Lithp.Lex;
using Lithp.SExp;

namespace Lithp
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private readonly SystemFunctionTable _systemFunctionTable;

        public Parser(Lexer lexer, SystemFunctionTable systemFunctionTable)
        {
            _lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
            _systemFunctionTable = systemFunctionTable ?? throw new ArgumentNullException(nameof(systemFunctionTable));
        }

        public SExp.SExp Eat()
        {
            var token = _lexer.Eat();
            if (token.Kind == TokenKind.Eof)
                return null;

            if (token.Kind != TokenKind.StartExpression)
                throw new InvalidOperationException($"Expected to encounter a new expression but found {token}");

            return EatExpression(token);
        }

        private SExp.SExp EatExpression(IToken startExpToken)
        {
            var exp = new SExp.SExp(startExpToken.Line, startExpToken.Column, new List<SExpNode>());
            var token = _lexer.Eat();
            do
            {
                switch (token.Kind)
                {
                    case TokenKind.StartExpression:
                        exp.Nodes.Add(EatExpression(token));
                        break;

                    case TokenKind.EndExpression:
                        if (exp.Nodes.Count > 0 && exp.Nodes[0] is Atom atom && atom.Token is IdentifierToken idToken &&
                            _systemFunctionTable.TryGet(idToken.Value, out var function))
                        {
                            exp.Nodes.RemoveAt(0);
                            exp = new FunctionSExp(function, token.Line, token.Column, exp.Nodes);
                        }
                        else if (exp.Nodes.Count > 0)
                        {
                            exp = new FunctionSExp(_systemFunctionTable.Get("list"), token.Line, token.Column,
                                exp.Nodes);
                        }

                        return exp;

                    case TokenKind.DoubleLiteral:
                    case TokenKind.IntegerLiteral:
                    case TokenKind.StringLiteral:
                    case TokenKind.Identifier:
                        exp.Nodes.Add(new Atom(token.Line, token.Column, token));
                        break;

                    case TokenKind.Eof:
                        return null;

                    default:
                        throw new InvalidOperationException($"Unexpected token: {token}");
                }

                token = _lexer.Eat();

            } while (token.Kind != TokenKind.Eof);

            throw new InvalidOperationException($"Unclosed statement: {token}");
        }
    }
}