using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{ 
    public class RpnTransformer
    {
        private readonly Stack<string> SymbolsStack = new Stack<string>();
        private readonly Dictionary<char, IOperation> _operations;
        private static Dictionary<string, int> Priorities = new Dictionary<string, int>()
                                                                {
                                                                    {"+", 0},
                                                                    {"*", 1},
                                                                    {"/", 1},
                                                                    {"-", 0}
                                                                };

        public RpnTransformer(Dictionary<char, IOperation> operations)
        {
            _operations = operations;
        }

        private RpnTransformer() {}

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
                    if (SymbolsStack.Count != 0 && Priorities.ContainsKey(SymbolsStack.Peek()) && Priorities.ContainsKey(input[pos].ToString()) && Priorities[input[pos].ToString()] < Priorities[SymbolsStack.Peek()])
                    {
                        var operation = SymbolsStack.Pop();
                        SymbolsStack.Push(s);
                        s = operation;
                    }
                    else if (input[pos].ToString() == ")")
                    {
                        isNumber = false;
                        s = string.Empty;
                        bool isCloseBracketReached = false;
                        while (SymbolsStack.Count != 0)
                        {
                            var symbol = SymbolsStack.Pop();
                            if (symbol != "(")
                            {
                                if (s == string.Empty)
                                {
                                    s += ' ' + symbol;
                                    pos += 1;
                                }
                                else
                                {
                                    s += symbol;
                                }
                            }
                            else
                            {
                                isCloseBracketReached = true;
                            }
                        }

                        if (!isCloseBracketReached)
                        {
                            throw new FormatException();
                        }
                        else
                        {
                            output += s;
                            continue;
                        }
                    }
                    else
                    {
                        if (Priorities.ContainsKey(input[pos].ToString()) || input[pos] == '(')
                        {
                            SymbolsStack.Push(input[pos].ToString());
                            isNumber = false;
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                        
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
                var symbol = SymbolsStack.Pop();
                if (symbol == "(" || symbol == ")")
                {
                    throw new FormatException();
                }

                output += ' ' + symbol;
            }

            return output;
        }


    }
}
