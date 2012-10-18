using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    public class Lexer : ILexer
    {
        private readonly Dictionary<char, IOperation> _operations;
        private readonly Dictionary<char, NumberTransformOperation> _numberTransformOperations;
        private readonly Stack<Token> _tokens = new Stack<Token>();

        public Lexer(Dictionary<char, IOperation> operations, Dictionary<char, NumberTransformOperation> numberTransformOperations)
        {
            _operations = operations;
            _numberTransformOperations = numberTransformOperations;
        }

        public IEnumerable<Token> Tokenize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException();
            }
            
            int pos = 0;
            while (pos < input.Length)
            {
                var nextSymbol = input[pos];
                var nextTokenText = string.Empty + nextSymbol;
                if (Char.IsDigit(nextSymbol))
                {
                    for (int i = pos + 1; (i < input.Length) && (Char.IsDigit(input[i])); i++)
                    {
                        nextTokenText += input[i];
                    }
                    var token = Token.Number(nextTokenText);
                    _tokens.Push(token);
                    pos += nextTokenText.Length;
                    yield return token;
                }
                else if ((_tokens.Count != 0 && _tokens.Peek().Kind == TokenKind.Operation && (_numberTransformOperations.ContainsKey(nextSymbol))) ||
                         (_tokens.Count != 0 && _tokens.Peek().Kind == TokenKind.Bracket && _tokens.Peek().Text != ")" && (_numberTransformOperations.ContainsKey(nextSymbol))) ||
                         (_tokens.Count == 0 && (_numberTransformOperations.ContainsKey(nextSymbol))))
                {
                    for (int i = pos + 1; (i < input.Length) && (Char.IsDigit(input[i])); i++)
                    {
                         nextTokenText += input[i];
                    }
                    var token = Token.Number(nextTokenText);
                    _tokens.Push(token);
                    pos += nextTokenText.Length;
                    yield return token;
                }
                else if (nextSymbol == ')' || nextSymbol == '(')
                {
                    var token = Token.Bracket(nextSymbol.ToString(CultureInfo.InvariantCulture));
                    _tokens.Push(token);
                    pos += 1;
                    yield return token;
                }
                else if (_operations.ContainsKey(input[pos]))
                {
                    var token = Token.Operation(nextSymbol.ToString(CultureInfo.InvariantCulture));
                    _tokens.Push(token);
                    pos += 1;
                    yield return token;
                }
                else
                {
                    throw new FormatException(string.Format("Unexpected symbol occured on lexem analyze: {0}", input[pos]));
                }
            }
            _tokens.Clear();
        }
    }
}
