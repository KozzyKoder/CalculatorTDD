using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public class DivisionOperation : IOperation
    {
        public char Sign()
        {
            return '/';
        }

        public int Execute(int operand1, int operand2)
        {
            return operand1 / operand2;
        }

        public Priority CompareTo(IOperation other)
        {
            switch (other.Sign())
            {
                case '+':
                case '-': return Priority.Larger;
                case '*':
                case '/': return Priority.TheSame;
                default: throw new InvalidOperationException();
            }
        }
    }
}
