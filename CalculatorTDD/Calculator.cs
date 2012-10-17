using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    public class Calculator
    {
        private readonly Stack<int> _evalStack = new Stack<int>();
        private readonly Dictionary<char, IOperation> _operations;
        
        public Calculator(Dictionary<char, IOperation> operations)
        {
            _operations = operations;
        }
        
        private Calculator() {}

        public int Calculate(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException();
            }

            var lexer = new Lexer(_operations);
            var transformer = new Transformer(_operations);

            var lexems = lexer.Tokenize(expression).ToList();

            var RevPolNotTokens = transformer.Transform(lexems);
            foreach (var token in RevPolNotTokens)
            {
                char chr = '\n';
                if (token.Text.Length == 1)
                {
                    chr = Convert.ToChar(token.Text);
                    if (Char.IsDigit(chr))
                    {
                        var numberValue = 0;
                        if (!int.TryParse(token.Text, out numberValue))
                        {
                            throw new ArgumentException("Could not parse number");
                        }
                        _evalStack.Push(numberValue);
                        continue;
                    }
                }
                
                if (_operations.ContainsKey(chr))
                {
                    var number1 = _evalStack.Pop();
                    var number2 = _evalStack.Pop();
                    var operation = _operations[chr];
                    if (operation.Sign() == '/' && number1 == 0)
                    {
                        throw new ArgumentException("Error occured on performing calculation: division by zero");
                    }
                    var result = operation.Execute(number2, number1);
                    _evalStack.Push(result);
                }
                else
                {
                    throw new InvalidOperationException("No operation with this sign found");
                }
            }
            
            return _evalStack.Pop();
        }
    }
}
