using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules.ValidationExceptions
{
    public class PropertyTooSmallException<T> : ValidationException where T : IComparable
    {
        public PropertyTooSmallException(string property, T value, T min) : base(property)
        {
            Value = value;
            Min = min;
        }

        public T Min { get; }
        public T Value { get; }
    }
}
