using System;
using CalculatorTDD.Tests.TestFixtures;
using Xunit;

namespace CalculatorTDD.Tests.Tests
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
            Assert.Throws(typeof (FormatException), () => calculator.Calculate("5+6+b"));
        }

        [Fact]
        public void OperationsWithoutOperands()
        {
            Assert.Throws(typeof(FormatException), () => calculator.Calculate("5+/6+b"));
        }

        [Fact]
        public void CalculatorSimpleMultOf3And2Returns6()
        {
            var result = calculator.Calculate("3*2");
            Assert.Equal(6, result);
        }

        [Fact]
        public void DivisionByZeroThrowsArgumentException()
        {
            Assert.Throws(typeof(ArgumentException), () => calculator.Calculate("4/0"));
        }

        [Fact]
        public void SimpleSubtractionTest()
        {
            var result = calculator.Calculate("5-4");
            Assert.Equal(1, result);
        }

        [Fact]
        public void SimpleSubtractionWithNegativeResult()
        {
            var result = calculator.Calculate("4-5");
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SimpleDivisionOperandsDividedEvenly()
        {
            var result = calculator.Calculate("8/2");
            Assert.Equal(4, result);
        }

        [Fact]
        public void SimpleDivisionOperandsDoNotDividedEvenly()
        {
            var result = calculator.Calculate("5/2");
            Assert.Equal(2, result);
        }

        [Fact]
        public void SimpleDivisionResultLessThan1()
        {
            var result = calculator.Calculate("4/5");
            Assert.Equal(0, result);
        }

        [Fact]
        public void SimpleMultOfNegativeNumber()
        {
            var result = calculator.Calculate("(1-2)*5");
            Assert.Equal(-5, result);
        }
    }
}
