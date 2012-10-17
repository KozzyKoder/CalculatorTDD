using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    class Program
    {
        public static readonly Dictionary<char, IOperation> Operations = new Dictionary<char, IOperation>()
                                                              {
                                                                  {'*', new MultiplicationOperation()},
                                                                  {'/', new DivisionOperation()},
                                                                  {'+', new AdditionOperation()},
                                                                  {'-', new SubtractionOperation()}
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
