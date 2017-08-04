using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.VisitorValidation.ValidationExceptions;

namespace ValidationRules
{
    public class StringPropertyValidator : IPropertyValidator<string>
    {
        public StringPropertyValidator(string propertyName, int? minLength, int? maxLength, bool required)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            MinLength = minLength;
            MaxLength = maxLength;
            Required = required;
        }

        public string PropertyName { get; }
        public int? MinLength { get; }
        public int? MaxLength { get; }
        public bool Required { get; }

        public bool IsValid(string property, bool throwException = true)
        {
            if (!Required && property == null) return true;
            if (Required && property == null) return false;

            if (property.Length < MinLength)
            {
                return throwException ? throw new PropertyTooShortException(PropertyName, property.Length, (int)MinLength) : false;
            }

            if (property.Length > MaxLength)
            {
                return throwException ? throw new PropertyTooShortException(PropertyName, property.Length, (int)MaxLength) : false;
            }

            return true;
        }
    }
}
