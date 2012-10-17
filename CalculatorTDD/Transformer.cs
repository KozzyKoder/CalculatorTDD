using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    public class Transformer
    {
        private readonly Stack<Token> SymbolsStack = new Stack<Token>();
        private readonly Dictionary<char, IOperation> _operations;

        public Transformer(Dictionary<char, IOperation> operations)
        {
            _operations = operations;
        }

        private Transformer() {}

        public IEnumerable<Token> Transform(IEnumerable<Token> tokens)
        {
            if (tokens == null || tokens.Count() == 0)
            {
                throw new ArgumentException();
            }

            var outputList = new List<Token>();
            bool isPreviousSymbolOperation = false;
            foreach (var token in tokens)
            {
                if (token.Kind == TokenKind.Number)
                {
                    outputList.Add(token);
                    isPreviousSymbolOperation = false;
                }
                else
                {
                    if ((token.Kind == TokenKind.Operation) && isOperationPriorityLessThanPreceedOperationPriority(token))
                    {
                        if (isPreviousSymbolOperation)
                        {
                            throw new FormatException();
                        }

                        var prevOperation = SymbolsStack.Pop();
                        outputList.Add(prevOperation);
                        SymbolsStack.Push(token);
                        isPreviousSymbolOperation = true;
                    }
                    else if (token.Kind == TokenKind.Bracket && token.Text == ")")
                    {
                        bool isCloseBracketReached = false;
                        while (SymbolsStack.Count != 0)
                        {
                            var operationToken = SymbolsStack.Pop();
                            if (operationToken.Text != "(")
                            {
                                outputList.Add(operationToken);
                            }
                            else
                            {
                                isCloseBracketReached = true;
                            }
                        }

                        isPreviousSymbolOperation = false;
                        if (!isCloseBracketReached)
                        {
                            throw new FormatException();
                        }
                    }
                    else
                    {
                        char operationTokenSign;
                        if (token.Text.Length == 1)
                        {
                            operationTokenSign = Convert.ToChar(token.Text);
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        if (_operations.ContainsKey(operationTokenSign) || operationTokenSign == '(')
                        {
                            if (isPreviousSymbolOperation)
                            {
                                throw new FormatException();
                            }
                            SymbolsStack.Push(token);
                            isPreviousSymbolOperation = true;
                        }
                        else
                        {
                            throw new ArgumentException();
                        }                        
                    }
                }
            }

            while (SymbolsStack.Count != 0)
            {
                var symbol = SymbolsStack.Pop();
                if (symbol.Text == "(" || symbol.Text == ")")
                {
                    throw new FormatException();
                }

                outputList.Add(symbol);
            }

            return outputList;
        }

        private bool isOperationPriorityLessThanPreceedOperationPriority(Token operationToken)
        {
            if (operationToken.Kind != TokenKind.Operation)
            {
                throw new ArgumentException();
            }

            char operationTokenSign;
            if (operationToken.Text.Length == 1)
            {
                operationTokenSign = Convert.ToChar(operationToken.Text);
            }
            else
            {
                throw new FormatException();
            }

            if (SymbolsStack.Count == 0)
            {
                return false;
            }

            char operationTokenSignFromStack;
            if (SymbolsStack.Peek().Text.Length == 1)
            {
                operationTokenSignFromStack = Convert.ToChar(SymbolsStack.Peek().Text);
            }
            else
            {
                throw new FormatException();
            }

            if (_operations.ContainsKey(operationTokenSignFromStack) && _operations.ContainsKey(operationTokenSign) && _operations[operationTokenSign].CompareTo(_operations[operationTokenSignFromStack]) == Priority.Lesser)
            {
                return true;
            }

            return false;
        }
    }
}
