using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Tests.TestFixtures;
using Xunit;

namespace CalculatorTDD.Tests
{   
    public class CalculatorTDDTests : IUseFixture<OperationsSetupFixture>
    {
        private Calculator calculator;

        public void SetFixture(OperationsSetupFixture data)
        {
            calculator = new Calculator(data.Operations);
        }

        [Fact]
        public void CalculatorExpressionShouldNotBeEmpty()
        {
            Assert.Throws(typeof(ArgumentException), () => calculator.Calculate(""));
        }
        
        [Fact]
        public void CalculatorExpressionShouldNotBeNull()
        {
            Assert.Throws(typeof(ArgumentException), () => calculator.Calculate(null));
        }

        [Fact]
        public void CalculatorSimpleAdditionOf1And2Returns3()
        {
            var result = calculator.Calculate("1+2");

            Assert.Equal(3, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionOf5And6Returns11()
        {
            var result = calculator.Calculate("5+6");

            Assert.Equal(11, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionOf5And6And8Returns19()
        {
            var result = calculator.Calculate("5+6+8");

            Assert.Equal(19, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionNonNumberOperandsThrowsException()
        {
            Assert.Throws(typeof (ArgumentException), () => calculator.Calculate("5+6+b"));
        }

        [Fact]
        public void CalculatorSimpleMultOf3And2Returns6()
        {
            var result = calculator.Calculate("3*2");
            Assert.Equal(6, result);
        }
    }
}
