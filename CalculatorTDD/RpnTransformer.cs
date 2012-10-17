using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{ 
    public class RpnTransformer
    {
        private readonly Stack<char> SymbolsStack = new Stack<char>();
        private readonly Dictionary<char, IOperation> _operations;

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
                {
                    for (int i = pos + 1; (i < input.Length) && (Char.IsDigit(input[i])); i++)
                    {
                        s += input[i];
                    }
                }
                else
                {
                    if (SymbolsStack.Count != 0 && _operations.ContainsKey(SymbolsStack.Peek()) && _operations.ContainsKey(input[pos]) && _operations[input[pos]].CompareTo(_operations[SymbolsStack.Peek()]) == Priority.Lesser)
                    {
                        var operation = SymbolsStack.Pop();
                        if (s.Length == 1)
                        {
                            var chr = Convert.ToChar(s);
                            SymbolsStack.Push(chr);
                        }
                        else
                        {
                            throw new FormatException();
                        }
                        
                        s = operation.ToString();
                    }
                    else if (input[pos].ToString() == ")")
                    {
                        isNumber = false;
                        s = string.Empty;
                        bool isCloseBracketReached = false;
                        while (SymbolsStack.Count != 0)
                        {
                            var symbol = SymbolsStack.Pop();
                            if (symbol != '(')
                            {
                                if (s == string.Empty)
                                {
                                    s += ' ';
                                    s += symbol;
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
                        if (_operations.ContainsKey(input[pos]) || input[pos] == '(')
                        {
                            SymbolsStack.Push(input[pos]);
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
                if (symbol == '(' || symbol == ')')
                {
                    throw new FormatException();
                }

                output += ' ';
                output += symbol;
            }

            return output;
        }
    }
}
