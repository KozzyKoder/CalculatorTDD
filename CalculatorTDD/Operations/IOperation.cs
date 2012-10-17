using System;
using System.Runtime.Serialization;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public interface IOperation
    {
        char Sign();

        int Execute(int operand1, int operand2);

        Priority CompareTo(IOperation other);
    }
}
