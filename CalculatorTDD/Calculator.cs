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

            var transformer = new RpnTransformer(_operations);
            var rpnExpressionSymbols = transformer.Transform(expression).Split(new char[] {' '});
            foreach (var symbol in rpnExpressionSymbols)
            {
                char chr = '\n';
                if (symbol.Length == 1)
                {
                    chr = Convert.ToChar(symbol);
                    if (Char.IsDigit(chr))
                    {
                        var numberValue = 0;
                        if (!int.TryParse(symbol, out numberValue))
                        {
                            throw new ArgumentException();
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
                        throw new ArgumentException();
                    }
                    var result = operation.Execute(number2, number1);
                    _evalStack.Push(result);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            
            return _evalStack.Pop();
        }
    }
}
