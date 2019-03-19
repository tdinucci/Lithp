using System;

namespace Lithp.Lex
{
    public class Lexer
    {
        private int _currLine;
        private int _currCol;
        private int _currIndex;
        private readonly ReadOnlyMemory<char> _source;

        public Lexer(char[] source)
        {
            _source = new ReadOnlyMemory<char>(source);
        }

        private void SkipWhitespace(ReadOnlySpan<char> span)
        {
            if (_currIndex < span.Length)
            {
                var ch = span[_currIndex];
                while (char.IsWhiteSpace(ch))
                {
                    if (ch == '\n')
                    {
                        _currLine++;
                        _currCol = 0;
                    }
                    else
                        _currCol++;

                    if (_currIndex == span.Length - 1)
                    {
                        ++_currIndex;
                        break;
                    }

                    ch = span[++_currIndex];
                }
            }
        }

        public IToken Eat()
        {
            var span = _source.Span;
            SkipWhitespace(span);

            var token = _currIndex == span.Length
                ? new EofToken()
                : Eat(span.Slice(_currIndex));

            token.Line = _currLine;
            token.Column = _currCol;

            _currCol += token.Length;

            // this will always be one more than the actual index
            _currIndex += token.Length;

            return token;
        }

        private IToken Eat(ReadOnlySpan<char> span)
        {
            var ch = span[0];
            IToken token;

            if (ch == '(')
                token = new StartExpressionToken();
            else if (ch == ')')
                token = new EndExpressionToken();
            else if (ch == '"')
                token = EatString(span);

            else if (char.IsDigit(ch))
                token = EatNumber(span);

            else
                token = EatIdentifier(span);

            return token;
        }

        private IToken EatNumber(ReadOnlySpan<char> span)
        {
            var len = GetTokenLength(span, allowDot: true);
            span = span.Slice(0, len);

            if (span.Contains(".", StringComparison.Ordinal))
            {
                if (!double.TryParse(span, out _))
                    throw new InvalidOperationException($"Expected double literal but read '{span.ToString()}'");

                return new DoubleLiteralToken(span.ToString());
            }

            if (!int.TryParse(span, out _))
                throw new InvalidOperationException($"Expected integer literal but read '{span.ToString()}'");

            return new IntegerLiteralToken(span.ToString());
        }

        private IdentifierToken EatIdentifier(ReadOnlySpan<char> span)
        {
            var len = GetTokenLength(span);
            span = span.Slice(0, len);

            return new IdentifierToken(span.ToString());
        }

        private StringLiteralToken EatString(ReadOnlySpan<char> span)
        {
            var index = 1;
            char ch;
            do
            {
                ch = span[index++];
            } while (ch != '"' && index < span.Length);

            return new StringLiteralToken(span.Slice(0, index).ToString());
        }

        private int GetTokenLength(ReadOnlySpan<char> span, bool allowDot = false)
        {
            var index = 0;
            bool consumed;
            do
            {
                consumed = false;
                var ch = span[index];
                if ((!char.IsWhiteSpace(ch) && ch != '(' && ch != ')') || (allowDot && ch == '.'))
                {
                    index++;
                    consumed = true;
                }

            } while (consumed && index < span.Length);

            return index;
        }
    }
}