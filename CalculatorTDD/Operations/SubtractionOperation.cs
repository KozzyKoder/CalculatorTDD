using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public class SubtractionOperation : NumberTransformOperation
    {
        public override int Transformation(int number)
        {
            return -number;
        }

        public SubtractionOperation()
        {
            Init(new Dictionary<char, Priority>()
                     {
                         {'+', Priority.TheSame},
                         {'/', Priority.Lesser},
                         {'*', Priority.Lesser}
                     }, '-', (i, i1) => i - i1);
        }
    }
}
