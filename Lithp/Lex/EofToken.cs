namespace Lithp.Lex
{
    public class EofToken : Token<string>
    {
        public EofToken() : base(TokenKind.Eof, string.Empty, 0)
        {
        }
    }
}