using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace CalculatorTDD.Tests
{
    public class RpnTransformerTests
    {
        [Fact]
        public void InputIsNotEmpty()
        {
            const string input = "";

            var rpnTransformer = new RpnTransformer();
            Assert.Throws(typeof(ArgumentException), () => rpnTransformer.Transform(input));
        }

        [Fact]
        public void InputIsNotNull()
        {
            const string input = null;

            var rpnTransformer = new RpnTransformer();
            Assert.Throws(typeof(ArgumentException), () => rpnTransformer.Transform(input));
        }
        
        [Fact]
        public void SimpleAddTwoNumbersExpression()
        {
            var rpnTransformer = new RpnTransformer();

            var output = rpnTransformer.Transform("2+5");

            Assert.Equal("2 5 +", output);
        }

        [Fact]
        public void SimpleAddNumberToMultiplicatedNumbersWithoutBrackets()
        {
            var rpnTransformer = new RpnTransformer();

            var output = rpnTransformer.Transform("2+5*3");

            Assert.Equal("2 5 3 * +", output);
        }

        [Fact]
        public void SimpleAddNumberToMultiplicatedNumbersWithoutBracketsAnotherOrder()
        {
            var rpnTransformer = new RpnTransformer();

            var output = rpnTransformer.Transform("5*3+2");

            Assert.Equal("5 3 * 2 +", output);
        }
    }
}
