using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var calculator = new Calculator(AppSettings.Operations, AppSettings.NumberTransformOperations);
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
