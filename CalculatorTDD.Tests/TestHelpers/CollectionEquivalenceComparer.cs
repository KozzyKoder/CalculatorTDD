using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorTDD.Tests.TestHelpers
{
    class CollectionEquivalenceComparer<T> : IEqualityComparer<IEnumerable<T>>
       where T : IEquatable<T>
    {
        public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
        {
            var leftList = new List<T>(x);
            var rightList = new List<T>(y);

            IEnumerator<T> enumeratorX = leftList.GetEnumerator();
            IEnumerator<T> enumeratorY = rightList.GetEnumerator();

            while (true)
            {
                bool hasNextX = enumeratorX.MoveNext();
                bool hasNextY = enumeratorY.MoveNext();

                if (!hasNextX || !hasNextY)
                    return (hasNextX == hasNextY);

                if (!enumeratorX.Current.Equals(enumeratorY.Current))
                    return false;
            }
        }

        public int GetHashCode(IEnumerable<T> obj)
        {
            throw new NotImplementedException();
        }
    }
}
