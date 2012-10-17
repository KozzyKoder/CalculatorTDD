using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using CalculatorTDD.Enums;

namespace CalculatorTDD.Operations
{
    public abstract class BaseOperation : IOperation
    {
        private bool _initialized = false;
        private char _sign;
        private Func<int, int, int> _operationBody;
        private Dictionary<char, Priority> _priorities; 
        
        public IOperation Init(Dictionary<char, Priority> priorities, char sign, Func<int,int,int> operationBody)
        {
            _priorities = priorities;
            _sign = sign;
            _operationBody = operationBody;
            _initialized = true;
            return this;
        }

        public char Sign()
        {
            if (!_initialized)
            {
                throw new OperationNotInitializedException("Call Init method before using this operation");
            }
            return _sign;
        }

        public int Execute(int operand1, int operand2)
        {
            if (!_initialized)
            {
                throw new OperationNotInitializedException("Call Init method before using this operation");
            }

            return _operationBody.Invoke(operand1, operand2);
        }

        public Priority CompareTo(IOperation other)
        {
            if (!_initialized)
            {
                throw new OperationNotInitializedException("Call Init method before using this operation");
            }
            
            if (_priorities.ContainsKey(other.Sign()))
            {
                return _priorities[other.Sign()];
            }

            var result = (Priority)other.GetType().GetMethod("_CompareTo").Invoke(other, new object[] {this});
            if (result == Priority.Lesser)
            {
                return Priority.Larger;
            }
            else if (result == Priority.Larger)
            {
                return Priority.Lesser;
            }
            
            return Priority.TheSame;
        }

        private Priority _CompareTo(IOperation other)
        {
            if (!_initialized)
            {
                throw new OperationNotInitializedException("Call Init method before using this operation");
            }

            if (_priorities.ContainsKey(other.Sign()))
            {
                return _priorities[other.Sign()];
            }
            else
            {
                throw new OperationNotInitializedException(String.Format("Priorities for both operations with sings {0}, {1} are not defined", this.Sign(), other.Sign()));
            }
        }
    }

    [Serializable]
    public class OperationNotInitializedException : Exception
    {
        public OperationNotInitializedException()
        {
        }

        public OperationNotInitializedException(string message)
            : base(message)
        {
        }

        public OperationNotInitializedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected OperationNotInitializedException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
