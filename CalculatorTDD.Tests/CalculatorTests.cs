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

        [Fact]
        public void CalculatorSimpleAdditionOf1And2Returns3()
        {
            var calculator = new Calculator();

            var result = calculator.Calculate("1+2");
            Assert.Equal(3, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionOf5And6Returns11()
        {
            var calculator = new Calculator();

            var result = calculator.Calculate("5+6");
            Assert.Equal(11, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionOf5And6And8Returns19()
        {
            var calculator = new Calculator();

            var result = calculator.Calculate("5+6+8");
            Assert.Equal(19, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionNonNumberOperandsThrowsException()
        {
            var calculator = new Calculator();

            Assert.Throws(typeof (ArgumentException), () => calculator.Calculate("5+6+b"));
        }

        [Fact(Skip = "This functionality doesn't yet implemented")]
        public void CalculatorSimpleMultOf3And2Returns6()
        {
            var calculator = new Calculator();

            var result = calculator.Calculate("3*2");
            Assert.Equal(6, result);
        }
    }
}
