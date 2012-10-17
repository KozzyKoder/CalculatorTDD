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
                
                switch (chr)
                {
                    case '*' :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = new MultiplicationOperation().Execute(number1, number2);
                            evalStack.Push(result);
                            break;
                        }
                    case '/' :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = new DivisionOperation().Execute(number1, number2);
                            evalStack.Push(result);
                            break;
                        }
                    case '+' :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = new AdditionOperation().Execute(number1, number2);
                            evalStack.Push(result);
                            break;
                        }
                    case '-' :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = new SubtractionOperation().Execute(number1, number2);
                            evalStack.Push(result);
                            break;
                        }
                }
            }
            
            return evalStack.Pop();
        }
    }
}
