using System;
using System.Collections.Generic;
using CalculatorTDD.Tests.TestFixtures;
using Moq;
using Xunit;

namespace CalculatorTDD.Tests.Tests
{   
    public class CalculatorTDDTests : IUseFixture<OperationsSetupFixture>
    {
        private Calculator calculator;

        public void SetFixture(OperationsSetupFixture data)
        {
            var mockLexer = new Mock<ILexer>();
            mockLexer.Setup(p => p.Tokenize(It.IsAny<string>())).Returns<IEnumerable<Token>>(p => new List<Token>());

            calculator = new Calculator(data.Operations);
            calculator._lexer = mockLexer.Object;
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
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("1"),
                                 Token.Number("2"),
                                 Token.Operation("+")
                             });

            calculator._transformer = mockTransformer.Object;

            var result = calculator.Calculate("1+2");

            Assert.Equal(3, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionOf5And6Returns11()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("5"),
                                 Token.Number("6"),
                                 Token.Operation("+")
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("5+6");

            Assert.Equal(11, result);
        }

        [Fact]
        public void CalculatorSimpleAdditionOf5And6And8Returns19()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("5"),
                                 Token.Number("6"),
                                 Token.Number("8"),
                                 Token.Operation("+"),
                                 Token.Operation("+")
                             });

            calculator._transformer = mockTransformer.Object;
            
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
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("3"),
                                 Token.Number("2"),
                                 Token.Operation("*")
                             });

            calculator._transformer = mockTransformer.Object;

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
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("5"),
                                 Token.Number("4"),
                                 Token.Operation("-")
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("5-4");
            Assert.Equal(1, result);
        }

        [Fact]
        public void SimpleSubtractionWithNegativeResult()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("4"),
                                 Token.Number("5"),
                                 Token.Operation("-")
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("4-5");
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SimpleSubtractionWithPositiveResultAndBrackets()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("12"),
                                 Token.Number("5"),
                                 Token.Operation("-"),
                             });

            calculator._transformer = mockTransformer.Object;

            var result = calculator.Calculate("(12-5)");
            Assert.Equal(7, result);
        }

        [Fact]
        public void SimpleDivisionOperandsDividedEvenly()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("8"),
                                 Token.Number("2"),
                                 Token.Operation("/"),
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("8/2");
            Assert.Equal(4, result);
        }

        [Fact]
        public void SimpleDivisionOperandsDoNotDividedEvenly()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("5"),
                                 Token.Number("2"),
                                 Token.Operation("/"),
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("5/2");
            Assert.Equal(2, result);
        }

        [Fact]
        public void SimpleDivisionResultLessThan1()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("4"),
                                 Token.Number("5"),
                                 Token.Operation("/"),
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("4/5");
            Assert.Equal(0, result);
        }

        [Fact]
        public void SimpleMultOfNegativeNumber()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("1"),
                                 Token.Number("2"),
                                 Token.Operation("-"),
                                 Token.Number("5"),
                                 Token.Operation("*"),
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("(1-2)*5");
            Assert.Equal(-5, result);
        }

        [Fact]
        public void LongExpressionWithDifferentKindOfOperations()
        {
            var mockTransformer = new Mock<ITransformer>();
            mockTransformer.Setup(p => p.Transform(Moq.It.IsAny<List<Token>>())).Returns<IEnumerable<Token>>(
                    p => new List<Token>()
                             {
                                 Token.Number("12"),
                                 Token.Number("5"),
                                 Token.Operation("+"),
                                 Token.Number("4"),
                                 Token.Operation("*"),
                                 Token.Number("5"),
                                 Token.Operation("+"),
                                 Token.Number("5"),
                                 Token.Number("6"),
                                 Token.Operation("*"),
                                 Token.Operation("+"),
                                 Token.Number("-12"),
                                 Token.Number("5"),
                                 Token.Operation("*"),
                                 Token.Operation("+"),
                             });

            calculator._transformer = mockTransformer.Object;
            
            var result = calculator.Calculate("(12+5)*4+5+5*6+(-12*5)");
            Assert.Equal(43, result);
        }
    }
}
