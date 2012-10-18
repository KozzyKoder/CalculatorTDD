using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    public static class AppSettings
    {
        private static Dictionary<char, IOperation> _operations = null;

        public static Dictionary<char, IOperation> Operations
        {
            get
            {
                if (_operations == null)
                {
                    var operationTypes = from type in AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes())
                                         where type.IsSubclassOf(typeof(BaseOperation)) && !type.IsAbstract
                                         select type;

                    _operations = new Dictionary<char, IOperation>();
                    foreach (var operationType in operationTypes)
                    {
                        var operation = (IOperation)operationType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                        _operations.Add(operation.Sign(), operation);
                    }
                }
                return _operations;
            }
        } 
        
        
    }
}
