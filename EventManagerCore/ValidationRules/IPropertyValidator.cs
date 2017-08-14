using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules
{
    public interface IPropertyValidator<T>
    {
        string PropertyName { get; }
        bool IsValid(T property, bool throwException);
    }
}
