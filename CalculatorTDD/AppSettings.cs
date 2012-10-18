using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CalculatorTDD.Attributes;
using CalculatorTDD.Enums;
using CalculatorTDD.Operations;

namespace CalculatorTDD
{
    public static class AppSettings
    {
        private static Dictionary<char, IOperation> _operations = null;
        private static Dictionary<char, NumberTransformOperation> _numberTransformOperations = null;
        private static IEnumerable<Type> _operationTypes = null;

        private static IEnumerable<Type> OperationTypes
        {
            get
            {
                if (_operationTypes == null)
                {
                    _operationTypes = from type in AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes())
                                      where type.IsSubclassOf(typeof(BaseOperation)) && !type.IsAbstract && !Attribute.GetCustomAttributes(type).Contains(new SkipOperationAttribute())
                                      select type;
                }
                return _operationTypes;
            }
        }

        public static Dictionary<char, IOperation> Operations
        {
            get
            {
                if (_operations == null)
                {
                    _operations = new Dictionary<char, IOperation>();
                    foreach (var operationType in OperationTypes)
                    {
                        var operation = (IOperation)operationType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                        _operations.Add(operation.Sign(), operation);
                    }
                }
                return _operations;
            }
        }

        public static Dictionary<char, NumberTransformOperation> NumberTransformOperations
        {
            get
            {
                if (_numberTransformOperations == null)
                {
                    var numberTransformOperationTypes = from type in OperationTypes
                                                        where type.IsSubclassOf(typeof(NumberTransformOperation))
                                                        select type;

                    _numberTransformOperations = new Dictionary<char, NumberTransformOperation>();
                    foreach (var operationType in numberTransformOperationTypes)
                    {
                        var operation = (NumberTransformOperation)operationType.GetConstructor(new Type[] { }).Invoke(new object[] { });
                        _numberTransformOperations.Add(operation.Sign(), operation);
                    }
                }
                return _numberTransformOperations;
            }
        } 
    }
}
