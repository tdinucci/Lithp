namespace Lithp.Lex
{
    public class StartExpressionToken : ExpressionBoundaryToken
    {
        public StartExpressionToken() : base(TokenKind.StartExpression, "(")
        {
        }
    }
}