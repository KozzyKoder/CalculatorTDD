using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Operations;
using CalculatorTDD.Tests.TestFixtures;
using Xunit;

namespace CalculatorTDD.Tests
{    
    public class RpnTransformerTests : IUseFixture<OperationsSetupFixture>
    {
        private RpnTransformer _transformer;

        public void SetFixture(OperationsSetupFixture data)
        {
            _transformer = new RpnTransformer(data.Operations);
        }

        [Fact]
        public void InputIsNotEmpty()
        {
            Assert.Throws(typeof(ArgumentException), () => _transformer.Transform(""));
        }

        [Fact]
        public void InputIsNotNull()
        {
            Assert.Throws(typeof(ArgumentException), () => _transformer.Transform(null));
        }
        
        [Fact]
        public void SimpleAddTwoNumbersExpression()
        {
            var output = _transformer.Transform("2+5");

            Assert.Equal("2 5 +", output);
        }

        [Fact]
        public void SimpleAddTwoNumbersDifferentLengthExpression()
        {
            var output = _transformer.Transform("245+53+1");

            Assert.Equal("245 53 1 + +", output);
        }

        [Fact]
        public void SimpleAddNumberToMultiplicatedNumbersWithoutBrackets()
        {
            var output = _transformer.Transform("2+5*3");

            Assert.Equal("2 5 3 * +", output);
        }

        [Fact]
        public void SimpleAddNumberToMultiplicatedNumbersWithoutBracketsAnotherOrder()
        {
            var output = _transformer.Transform("5*3+2");

            Assert.Equal("5 3 * 2 +", output);
        }
        
        [Fact]
        public void SimpleOperationsWithSomePrioritiesAndBrackets()
        {
            var output = _transformer.Transform("(1+2)*4+3");

            Assert.Equal("1 2 + 4 * 3 +", output);
        }

        [Fact]
        public void SimpleOperationsWithSomePrioritiesAndBracketsAndDifferentNumbersLength()
        {
            var output = _transformer.Transform("(123+23)*4444+31");

            Assert.Equal("123 23 + 4444 * 31 +", output);
        }

        [Fact]
        public void UnmatchedOpenBracket()
        {

            Assert.Throws(typeof(FormatException), () => _transformer.Transform("(123+23*4444+31"));
        }

        [Fact]
        public void UnmatchedCloseBracket()
        {
            Assert.Throws(typeof(FormatException), () => _transformer.Transform("123+23)*4444+31"));
        }
    }
}
