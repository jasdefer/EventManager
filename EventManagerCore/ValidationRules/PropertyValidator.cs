using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.ValidationExceptions;

namespace ValidationRules
{
    public class PropertyValidator<T> : IPropertyValidator<T> where T : IComparable
    {
        public PropertyValidator(string propertyName, T min, T max, bool required)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            Min = min;
            Max = max;
            Required = required;
        }
        public string PropertyName { get; }
        public T Max { get; }
        public T Min { get; }
        public bool Required { get; }

        public bool IsValid(T property, bool throwException = true)
        {
            if (property == null && !Required) return true;
            if (property == null && Required) return throwException?throw new ArgumentNullException(PropertyName):false;

            if (property.CompareTo(Min) < 0)
            {
                return throwException ? throw new PropertyTooSmallException<T>(PropertyName, property, Min) : false;
            }

            if (property.CompareTo(Max) > 0)
            {
                return throwException ? throw new PropertyTooSmallException<T>(PropertyName, property, Max) : false;
            }

            return true;
        }
    }
}
