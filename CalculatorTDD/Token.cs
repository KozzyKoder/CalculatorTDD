using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;

namespace CalculatorTDD
{
    public class Token : IEquatable<Token>
    {
        public TokenKind Kind { get; private set; }

        public string Text { get; private set; }

        private Token(TokenKind kind) : this(kind, null) { }

        private Token() {}

        private Token(TokenKind kind, string text)
            : this()
        {
            Kind = kind;
            Text = text;
        }

        public static Token Operation(string text)
        {
            ValidateTextArgument(text);
            return new Token(TokenKind.Operation, text);
        }

        public static Token Bracket(string text)
        {
            ValidateTextArgument(text);
            return new Token(TokenKind.Bracket, text);
        }

        public static Token Number(string text)
        {
            ValidateTextArgument(text);
            return new Token(TokenKind.Number, text);
        }

        private static void ValidateTextArgument(string text)
        {
            if (text == null) throw new ArgumentNullException("text");
            if (text.Length == 0) throw new ArgumentException(null, "text");
        }

        public bool Equals(Token other)
        {
            if ((this.Kind == other.Kind) && (String.Compare(this.Text, other.Text, System.StringComparison.Ordinal) == 0))
            {
                return true;
            }
            else return false;
                
        }
    }
}
