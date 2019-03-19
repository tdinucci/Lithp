namespace Lithp.Lex
{
    public class ExpressionBoundaryToken : Token<string>
    {
        protected ExpressionBoundaryToken(TokenKind kind, string value) : base(kind, value, value.Length)
        {
        }
    }
}