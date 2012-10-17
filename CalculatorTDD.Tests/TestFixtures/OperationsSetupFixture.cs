using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Operations;

namespace CalculatorTDD.Tests.TestFixtures
{
    public class OperationsSetupFixture
    {
        public readonly Dictionary<char, IOperation> Operations = new Dictionary<char, IOperation>()
                                                              {
                                                                  {'*', new MultiplicationOperation()},
                                                                  {'/', new DivisionOperation()},
                                                                  {'+', new AdditionOperation()},
                                                                  {'-', new SubtractionOperation()}
                                                              };
    }
}
