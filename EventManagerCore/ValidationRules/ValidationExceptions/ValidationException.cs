using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules.ValidationExceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string property) : base()
        {
            Property = property ?? string.Empty;
        }

        public string Property { get; }
    }
}
