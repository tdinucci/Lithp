namespace Lithp.Lex
{
    public class EndExpressionToken : ExpressionBoundaryToken
    {
        public EndExpressionToken() : base(TokenKind.EndExpression, ")")
        {
        }
    }
}