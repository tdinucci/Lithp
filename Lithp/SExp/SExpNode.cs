namespace Lithp.SExp
{
    public class SExpNode
    {
        public int Line { get; }
        public int Column { get; }

        public SExpNode(int line, int column)
        {
            Line = line;
            Column = column;
        }
    }
}