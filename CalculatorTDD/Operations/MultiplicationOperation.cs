using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public class MultiplicationOperation : BaseOperation
    {
        public MultiplicationOperation()
        {
            Init(new Dictionary<char, Priority>()
                     {
                         {'-', Priority.Larger},
                         {'+', Priority.Larger},
                         {'/', Priority.TheSame}
                     }, '*', (i, i1) => i*i1);
        }
    }
}
