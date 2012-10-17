using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    public class Calculator
    {
        private Stack<int> evalStack = new Stack<int>();

        private Dictionary<char, IOperation> operations = new Dictionary<char, IOperation>()
                                                              {
                                                                  {'*', new MultiplicationOperation()},
                                                                  {'/', new DivisionOperation()},
                                                                  {'+', new AdditionOperation()},
                                                                  {'-', new SubtractionOperation()}
                                                              };
        
        public int Calculate(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException();
            }

            var transformer = new RpnTransformer();
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
                        evalStack.Push(numberValue);
                        continue;
                    }
                }
                
                if (operations.ContainsKey(chr))
                {
                    var number1 = evalStack.Pop();
                    var number2 = evalStack.Pop();
                    var operation = operations[chr];
                    var result = operation.Execute(number1, number2);
                    evalStack.Push(result);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            
            return evalStack.Pop();
        }
    }
}
