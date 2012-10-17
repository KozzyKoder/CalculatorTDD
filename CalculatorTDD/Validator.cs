using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorTDD
{
    public class Validator
    {
        private readonly Lexer _lexer;
        
        private Validator() {}

        public Validator(Lexer lexer)
        {
            _lexer = lexer;
        }

        
    }
}
