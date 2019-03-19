namespace Lithp.Lex
{
    public enum TokenKind
    {
        StartExpression,
        EndExpression,

        StringLiteral,
        IntegerLiteral,
        DoubleLiteral,

        Identifier,

        Eof
    }
}