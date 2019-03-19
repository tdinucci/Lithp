namespace Lithp.Lex
{
    public interface IToken<TValue> : IToken
    {
        new TValue Value { get; }
    }
    
    public interface IToken
    {
        TokenKind Kind { get; }
        object Value { get; }
        int Length { get; }
        int Line { get; set; }
        int Column { get; set; }
    }
}