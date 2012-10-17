using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    class Program
    {
        public static readonly Dictionary<char, IOperation> Operations = new Dictionary<char, IOperation>()
                                                              {
                                                                  {'*', new MultiplicationOperation().Init(new Dictionary<char, Priority>()
                                                                                                         {
                                                                                                             {'-', Priority.Larger},
                                                                                                             {'+', Priority.Larger},
                                                                                                             {'/', Priority.TheSame},
                                                                                                             {'*', Priority.TheSame}
                                                                                                         }, '*', (i, i1) => {return  i * i1;})},
                                                                  {'/', new DivisionOperation().Init(new Dictionary<char, Priority>()
                                                                                                         {
                                                                                                             {'-', Priority.Larger},
                                                                                                             {'+', Priority.Larger},
                                                                                                             {'/', Priority.TheSame},
                                                                                                             {'*', Priority.TheSame}
                                                                                                         }, '/', (i, i1) => {return  i / i1;})},
                                                                  {'+', new AdditionOperation().Init(new Dictionary<char, Priority>()
                                                                                                         {
                                                                                                             {'-', Priority.TheSame},
                                                                                                             {'+', Priority.TheSame},
                                                                                                             {'/', Priority.Lesser},
                                                                                                             {'*', Priority.Lesser}
                                                                                                         }, '+', (i, i1) => {return  i + i1;})},
                                                                  {'-', new SubtractionOperation().Init(new Dictionary<char, Priority>()
                                                                                                         {
                                                                                                             {'-', Priority.TheSame},
                                                                                                             {'+', Priority.TheSame},
                                                                                                             {'/', Priority.Lesser},
                                                                                                             {'*', Priority.Lesser}
                                                                                                         }, '-', (i, i1) => {return  i - i1;})}
                                                              };
        
        static void Main(string[] args)
        {
            try
            {
                var calculator = new Calculator(Operations);
                var expression = Console.ReadLine();
                Console.WriteLine(calculator.Calculate(expression));
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }            
        }
    }
}
