using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorTDD
{
    public interface ITransformer
    {
        IEnumerable<Token> Transform(IEnumerable<Token> tokens);
    }
}
