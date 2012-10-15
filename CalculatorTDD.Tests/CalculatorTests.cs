using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CalculatorTDD.Tests
{
    
    public class CalculatorTDDTests
    {
        [Fact]
        public void CalculatorExpressionShouldNotBeEmotyOrNull()
        {
            const string emptyExpression = "";
            const string nullExpression = null;
            var calculator = new Calculator();

            Assert.Throws(typeof(ArgumentException), () => calculator.Calculate(emptyExpression));
            Assert.Throws(typeof(ArgumentException), () => calculator.Calculate(nullExpression));
        }
    }
}
