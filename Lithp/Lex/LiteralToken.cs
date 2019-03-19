namespace Lithp.Lex
{
    public class LiteralToken<T> : Token<T>
    {
        protected LiteralToken(TokenKind kind, T value, int length) : base(kind, value, length)
        {
        }
    }
}