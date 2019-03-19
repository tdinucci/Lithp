using System;
using Lithp.Lex;

namespace Lithp
{
    public class Expression
    {
        public IToken Token { get; }

        protected ScopeManager ScopeManager { get; }

        public Expression(ScopeManager scopeManager, IToken token)
        {
            ScopeManager = scopeManager ?? throw new ArgumentNullException(nameof(scopeManager));
            Token = token ?? throw new ArgumentNullException(nameof(token));
        }

        public virtual object Evaluate()
        {
            switch (Token.Kind)
            {
                case TokenKind.Identifier:
                    return ScopeManager.GetCurrentScope().Get(((IdentifierToken) Token).Value);
                case TokenKind.DoubleLiteral:
                case TokenKind.IntegerLiteral:
                case TokenKind.StringLiteral:
                    return Token.Value;
                default:
                    throw new InvalidOperationException($"Unexpected token: {Token}");
            }
        }

        public override string ToString()
        {
            return $"[{Token.GetType().Name}] {Token}";
        }
    }
}