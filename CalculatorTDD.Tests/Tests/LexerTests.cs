using System;
using System.Collections.Generic;
using System.Linq;
using CalculatorTDD.Tests.TestFixtures;
using CalculatorTDD.Tests.TestHelpers;
using Xunit;

namespace CalculatorTDD.Tests.Tests
{
    public class LexerTests : IUseFixture<OperationsSetupFixture>
    {
        private Lexer _lexer;
        
        public void SetFixture(OperationsSetupFixture data)
        {
            _lexer = new Lexer(data.Operations);
        }

        [Fact]
        public void InputIsNotEmpty()
        {
            Assert.Throws(typeof(ArgumentException), () => _lexer.Tokenize("").ToList());
        }

        [Fact]
        public void InputIsNotNull()
        {
            Assert.Throws(typeof(ArgumentException), () => _lexer.Tokenize(null).ToList());
        }
        
        [Fact]
        public void ExpressionWithoutBracketsWithAllowedOperations()
        {
            var expectedLexems = new List<Token>
                                     {
                                         Token.Number("2"),
                                         Token.Operation("*"),
                                         Token.Number("5"),
                                         Token.Operation("+"),
                                         Token.Number("45"),
                                         Token.Operation("-"),
                                         Token.Number("15"),
                                         Token.Operation("/"),
                                         Token.Number("5")
                                     }.ToList();
            var lexems = _lexer.Tokenize("2*5+45-15/5").ToList();
            Assert.Equal(expectedLexems, lexems, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void ExpressionWithWrongNumberFormat()
        {
            Assert.Throws(typeof(FormatException), () => _lexer.Tokenize("5+4+1a").ToList());
        }

        [Fact]
        public void ExpressionWithBracketsWithAllowedOperations()
        {
            var expectedLexems = new List<Token>
                                     {
                                         Token.Number("2"),
                                         Token.Operation("*"),
                                         Token.Bracket("("),
                                         Token.Number("5"),
                                         Token.Operation("+"),
                                         Token.Number("45"),
                                         Token.Bracket(")"),
                                         Token.Operation("-"),
                                         Token.Number("15"),
                                         Token.Operation("/"),
                                         Token.Bracket("("),
                                         Token.Number("5"),
                                         Token.Operation("+"),
                                         Token.Number("3"),
                                         Token.Bracket(")")
                                     }.ToList();
            var lexems = _lexer.Tokenize("2*(5+45)-15/(5+3)").ToList();
            Assert.Equal(expectedLexems, lexems, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void ExpressionWithSignedNumbers()
        {
            var expectedLexems = new List<Token>
                                     {
                                         Token.Number("-2"),
                                         Token.Operation("*"),
                                         Token.Bracket("("),
                                         Token.Number("-5"),
                                         Token.Operation("+"),
                                         Token.Number("45"),
                                         Token.Bracket(")"),
                                         Token.Operation("-"),
                                         Token.Number("15"),
                                         Token.Operation("/"),
                                         Token.Bracket("("),
                                         Token.Number("-5"),
                                         Token.Operation("+"),
                                         Token.Number("-3"),
                                         Token.Bracket(")")
                                     }.ToList();
            var lexems = _lexer.Tokenize("-2*(-5+45)-15/(-5+-3)").ToList();
            Assert.Equal(expectedLexems, lexems, new CollectionEquivalenceComparer<Token>());
        }

        [Fact]
        public void ExpressionContainsNowAllowedOperation()
        {
            Assert.Throws(typeof (FormatException), () => _lexer.Tokenize("5+4&1").ToList());
        }
    }
}
