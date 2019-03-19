namespace Lithp.Lex
{
    public class IdentifierToken : Token<string>
    {
        public IdentifierToken(string value) : base(TokenKind.Identifier, value, value.Length)
        {
        }
    }
}