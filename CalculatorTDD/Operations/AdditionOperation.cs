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
            Init(new Dictionary<char, Priority>()
                     {
                         {'-', Priority.TheSame},
                         {'/', Priority.Lesser},
                         {'*', Priority.Lesser}
                     }, '+', (i, i1) => i + i1 );
        }
    }
}
