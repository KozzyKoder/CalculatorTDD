using System;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public class AdditionOperation : IOperation
    {
        public char Sign()
        {
            return '+';
        }

        public int Execute(int operand1, int operand2)
        {
            return operand1 + operand2;
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
