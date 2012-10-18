using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public class DivisionOperation : BaseOperation
    {
        public DivisionOperation()
        {
            Init(new Dictionary<char, Priority>()
                     {
                         {'-', Priority.Larger},
                         {'+', Priority.Larger},
                         {'*', Priority.TheSame}
                     }, '/', (i, i1) => i/i1 );
        }
    }
}
