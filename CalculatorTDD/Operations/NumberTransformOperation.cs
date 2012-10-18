using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorTDD.Operations
{
    public abstract class NumberTransformOperation : BaseOperation
    {
        public virtual int Transformation(int number)
        {
            return number;
        }
    }
}
