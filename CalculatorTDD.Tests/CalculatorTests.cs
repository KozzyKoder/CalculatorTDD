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
        public void CalculatorExpressionShouldNotBeEmpty()
        {
            const string emptyExpression = "";
            var calculator = new Calculator();

            Assert.Throws(typeof(ArgumentException), () => calculator.Calculate(emptyExpression));
        }
        
        [Fact]
        public void CalculatorExpressionShouldNotBeNull()
        {
            const string nullExpression = null;
            var calculator = new Calculator();

            Assert.Throws(typeof(ArgumentException), () => calculator.Calculate(nullExpression));
        }
    }
}
