using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorTDD.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SkipOperationAttribute : Attribute
    {
    }
}
