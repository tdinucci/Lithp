using System;

namespace Lithp.Lex
{
    public class IntegerLiteralToken : LiteralToken<int>
    {
        public IntegerLiteralToken(string value) : base(TokenKind.IntegerLiteral, Convert.ToInt32(value), value.Length)
        {
        }
    }
}