using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules.ValidationExceptions
{
    public class PropertyTooBigException<T> : ValidationException where T : IComparable
    {
        public PropertyTooBigException(string property, T value, T max) : base(property)
        {
            Value = value;
            Max = max;
        }

        public T Max { get; }
        public T Value { get; }
    }
}
