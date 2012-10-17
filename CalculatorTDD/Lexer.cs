﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    public class Lexer
    {
        private readonly Dictionary<char, IOperation> _operations;
        private readonly Stack<Token> _tokens = new Stack<Token>();

        public Lexer(Dictionary<char, IOperation> operations)
        {
            _operations = operations;
        }
        
        private Lexer() {}

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
                else if ((_tokens.Count != 0 && _tokens.Peek().Kind == TokenKind.Operation && (nextSymbol.ToString() == "+" || nextSymbol.ToString() == "-")) ||
                         (_tokens.Count != 0 && _tokens.Peek().Kind == TokenKind.Bracket && _tokens.Peek().Text != ")" && (nextSymbol.ToString() == "+" || nextSymbol.ToString() == "-")) ||
                         (_tokens.Count == 0 && (nextSymbol.ToString() == "+" || nextSymbol.ToString() == "-")))
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
                else if (nextSymbol.ToString() == ")" || nextSymbol.ToString() == "(")
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
                    throw new FormatException();
                }
            }
        }
    }
}
