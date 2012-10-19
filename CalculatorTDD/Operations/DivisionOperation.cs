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
            Init(new Dictionary<Priority, char[]>()
                     {
                         {Priority.Larger, new []{'+', '-'}},
                         {Priority.TheSame, new []{'*'}}
                     }, '/', (i, i1) => i/i1 );
        }
    }
}
