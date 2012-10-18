using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD.Tests.TestFixtures
{
    public class OperationsSetupFixture
    {
        public Dictionary<char, IOperation> Operations
        {
            get { return AppSettings.Operations; }
        }
                                                       
    }
}
