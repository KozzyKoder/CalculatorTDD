using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                switch (symbol)
                {
                    case "*" :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = number1*number2;
                            evalStack.Push(result);
                            break;
                        }
                    case "/" :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = (int)Math.Floor((double)(number1 / number2));
                            evalStack.Push(result);
                            break;
                        }
                    case "+" :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = number1+number2;
                            evalStack.Push(result);
                            break;
                        }
                    case "-" :
                        {
                            var number1 = evalStack.Pop();
                            var number2 = evalStack.Pop();
                            var result = number1 - number2;
                            evalStack.Push(result);
                            break;
                        }
                    default:
                        {
                            var numberValue = 0;
                            if (!int.TryParse(symbol, out numberValue))
                            {
                                throw new ArgumentException();
                            }
                            evalStack.Push(numberValue);
                            break;
                        }
                }
            }
            
            return evalStack.Pop();
        }
    }
}
