using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules.ValidationExceptions
{
    public class PropertyRequiredException : ValidationException
    {
        public PropertyRequiredException(string property) : base(property)
        {
        }
    }
}
