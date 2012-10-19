using System;
using System.Collections.Generic;
using CalculatorTDD.Attributes;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public class AdditionOperation : NumberTransformOperation
    {
        public AdditionOperation()
        {
            Init(new Dictionary<Priority, char[]>()
                     {
                         {Priority.TheSame, new [] {'-'}},
                         {Priority.Lesser, new [] {'/', '*'}}
                     }, '+', (i, i1) => i + i1 );
        }
    }
}
