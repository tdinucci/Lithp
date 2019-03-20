using Lithp.Lex;

namespace Lithp.SExp
{
    public class Atom : SExpNode
    {
        public IToken Token { get; }

        public Atom(int line, int column, IToken token) : base(line, column)
        {
            Token = token;
        }

        public override string ToString()
        {
            var result = Token?.Value == null ? string.Empty : Token.Value.ToString();
            return $"({result})";
        }
    }
}