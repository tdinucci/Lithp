namespace Lithp.Lex
{
    public class Token<TValue> : IToken<TValue>
    {
        public TokenKind Kind { get; }

        object IToken.Value => Value;
        public TValue Value { get; }
        public int Length { get; }

        public int Line { get; set; }
        public int Column { get; set; }

        protected Token(TokenKind kind, TValue value, int length)
        {
            Kind = kind;
            Value = value;
            Length = length;
        }

        public override string ToString()
        {
            return $"{Kind} -> {{{Value}}} [{Line}:{Column}]";
        }
    }
}