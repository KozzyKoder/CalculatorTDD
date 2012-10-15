using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorTDD
{ 
    public class RpnTransformer
    {
        private static readonly Stack<string> SymbolsStack = new Stack<string>();
        private static Dictionary<string, int> Priorities = new Dictionary<string, int>()
                                                                {
                                                                    {"+", 0},
                                                                    {"*", 1}
                                                                };

        public string Transform(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException();
            }

            int pos = 0;
            string output = string.Empty;
            while (pos < input.Length)
            {
                string s = string.Empty + input[pos];
                bool isNumber = true;
                if (Char.IsDigit(input[pos]))
                    for (int i = pos + 1; i < input.Length &&
                        (Char.IsDigit(input[i]) || input[i] == ',' || input[i] == '.'); i++)
                        s += input[i];
                else
                {
                    if (SymbolsStack.Count != 0  && Priorities[input[pos].ToString()] < Priorities[SymbolsStack.Peek()])
                    {
                        var operation = SymbolsStack.Pop();
                        SymbolsStack.Push(s);
                        s = operation;
                    }
                    else
                    {
                        SymbolsStack.Push(input[pos].ToString());
                        isNumber = false;
                    }
                    
                }

                pos += s.Length;
                
                if (isNumber)
                {
                    if (output != string.Empty)
                    {
                        output += ' ' + s;
                    }
                    else
                    {
                        output += s;
                    }
                }
            }

            while (SymbolsStack.Count != 0)
            {
                output += ' ' + SymbolsStack.Pop();
            }

            return output;
        }


    }
}
