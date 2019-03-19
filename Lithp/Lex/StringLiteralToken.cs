namespace Lithp.Lex
{
    public class StringLiteralToken : LiteralToken<string>
    {
        public StringLiteralToken(string value) : base(TokenKind.StringLiteral, value.Trim('"'), value.Length)
        {
        }
    }
}