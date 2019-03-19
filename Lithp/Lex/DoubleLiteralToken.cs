using System;

namespace Lithp.Lex
{
    public class DoubleLiteralToken : LiteralToken<double>
    {
        public DoubleLiteralToken(string value) : base(TokenKind.DoubleLiteral, Convert.ToDouble(value), value.Length)
        {
        }
    }
}