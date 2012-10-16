using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public class SubtractionOperation : IOperation
    {
        public char Sign()
        {
            return '-';
        }

        public int Execute(int operand1, int operand2)
        {
            return operand1 - operand2;
        }

        public Priority CompareTo(IOperation other)
        {
            switch (other.Sign())
            {
                case '+':
                case '-': return Priority.TheSame;
                case '*':
                case '/': return Priority.Lesser;
                default: throw new InvalidOperationException();
            }
        }
    }
}
