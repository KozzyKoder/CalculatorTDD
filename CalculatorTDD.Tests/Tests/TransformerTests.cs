using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Tests.TestFixtures;
using CalculatorTDD.Tests.TestHelpers;
using Xunit;

namespace CalculatorTDD.Tests.Tests
{
    public class TransformerTests : IUseFixture<OperationsSetupFixture>
    {
        private Transformer _transformer;

        public void SetFixture(OperationsSetupFixture data)
        {
            _transformer = new Transformer(data.Operations);
        }

        [Fact]
        public void InputIsNotEmpty()
        {
            Assert.Throws(typeof(ArgumentException), () => _transformer.Transform(new List<Token>()));
        }

        [Fact]
        public void InputIsNotNull()
        {
            Assert.Throws(typeof(ArgumentException), () => _transformer.Transform(null));
        }
        
        [Fact]
        public void SimpleAddTwoNumbersExpression()
        {
            var input = new List<Token>()
                            {
                                Token.Number("2"),
                                Token.Operation("+"),
                                Token.Number("5")
                            };

            var expectedOutput = new List<Token>
                                     {
                                         Token.Number("2"),
                                         Token.Number("5"),
                                         Token.Operation("+")
                                     };
            
            var output = _transformer.Transform(input);

            Assert.Equal(expectedOutput, output, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void SimpleAddTwoNumbersDifferentLengthExpression()
        {
            var input = new List<Token>()
                            {
                                Token.Number("245"),
                                Token.Operation("+"),
                                Token.Number("53"),
                                Token.Operation("+"),
                                Token.Number("1")
                            };

            var expectedOutput = new List<Token>
                                     {
                                         Token.Number("245"),
                                         Token.Number("53"),
                                         Token.Number("1"),
                                         Token.Operation("+"),
                                         Token.Operation("+")
                                     };
            
            var output = _transformer.Transform(input);

            Assert.Equal(expectedOutput, output, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void SimpleAddNumberToMultiplicatedNumbersWithoutBrackets()
        {
            var input = new List<Token>()
                            {
                                Token.Number("2"),
                                Token.Operation("+"),
                                Token.Number("5"),
                                Token.Operation("*"),
                                Token.Number("3")
                            };

            var expectedOutput = new List<Token>
                                     {
                                         Token.Number("2"),
                                         Token.Number("5"),
                                         Token.Number("3"),
                                         Token.Operation("*"),
                                         Token.Operation("+")
                                     };
            
            var output = _transformer.Transform(input);

            Assert.Equal(expectedOutput, output, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void SimpleAddNumberToMultiplicatedNumbersWithoutBracketsAnotherOrder()
        {
            var input = new List<Token>()
                            {
                                Token.Number("5"),
                                Token.Operation("*"),
                                Token.Number("3"),
                                Token.Operation("+"),
                                Token.Number("2")
                            };

            var expectedOutput = new List<Token>
                                     {
                                         Token.Number("5"),
                                         Token.Number("3"),
                                         Token.Operation("*"),
                                         Token.Number("2"),
                                         Token.Operation("+")
                                     };
            
            var output = _transformer.Transform(input);

            Assert.Equal(expectedOutput, output, new CollectionEquivalenceComparer<Token>());
        }
        
        [Fact]
        public void SimpleOperationsWithSomePrioritiesAndBrackets()
        {
            var input = new List<Token>()
                            {
                                Token.Bracket("("),
                                Token.Number("1"),
                                Token.Operation("+"),
                                Token.Number("2"),
                                Token.Bracket(")"),
                                Token.Operation("*"),
                                Token.Number("4"),
                                Token.Operation("+"),
                                Token.Number("3"),
                            };

            var expectedOutput = new List<Token>
                                     {
                                         Token.Number("1"),
                                         Token.Number("2"),
                                         Token.Operation("+"),
                                         Token.Number("4"),
                                         Token.Operation("*"),
                                         Token.Number("3"),
                                         Token.Operation("+")
                                     };
            
            var output = _transformer.Transform(input);

            Assert.Equal(expectedOutput, output, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void SimpleOperationsWithSomePrioritiesAndBracketsAndDifferentNumbersLength()
        {
            var input = new List<Token>()
                            {
                                Token.Bracket("("),
                                Token.Number("123"),
                                Token.Operation("+"),
                                Token.Number("23"),
                                Token.Bracket(")"),
                                Token.Operation("*"),
                                Token.Number("4444"),
                                Token.Operation("+"),
                                Token.Number("31")
                            };

            var expectedOutput = new List<Token>
                                     {
                                         Token.Number("123"),
                                         Token.Number("23"),
                                         Token.Operation("+"),
                                         Token.Number("4444"),
                                         Token.Operation("*"),
                                         Token.Number("31"),
                                         Token.Operation("+")
                                     };
            
            var output = _transformer.Transform(input);

            Assert.Equal(expectedOutput, output, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void UnmatchedOpenBracket()
        {
            var input = new List<Token>()
                            {
                                Token.Bracket("("),
                                Token.Number("123"),
                                Token.Operation("+"),
                                Token.Number("23"),
                                Token.Operation("*"),
                                Token.Number("4444"),
                                Token.Operation("+"),
                                Token.Number("31")
                            };
            
            Assert.Throws(typeof(FormatException), () => _transformer.Transform(input));
        }

        [Fact]
        public void UnmatchedCloseBracket()
        {
            var input = new List<Token>()
                            {
                                Token.Number("123"),
                                Token.Operation("+"),
                                Token.Number("23"),
                                Token.Bracket(")"),
                                Token.Operation("*"),
                                Token.Number("4444"),
                                Token.Operation("+"),
                                Token.Number("31")
                            };

            Assert.Throws(typeof(FormatException), () => _transformer.Transform(input));
        }
    }
}
