using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorTDD
{
    public class Calculator
    {
        public int Calculate(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentException();
            }
            
            int result = 0;
            var operands = expression.Split(new char[] {'+'});
            foreach (var operand in operands)
            {
                var addedValue = 0;
                if (int.TryParse(operand, out addedValue))
                {
                    result += addedValue;
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            return result;
        }
    }
}
