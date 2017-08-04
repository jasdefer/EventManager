using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules.PropertyValidation
{
    public static class RegionValidation
    {
        public static readonly StringPropertyValidator Description = new StringPropertyValidator(nameof(Description), null, 1024, false);
        public static readonly StringPropertyValidator Name = new StringPropertyValidator(nameof(Description), 3, 10, true);
        public static readonly StringPropertyValidator Polygon = new StringPropertyValidator(nameof(Description), 1, 1024, true);
        public static readonly PropertyValidator<DateTime> TimeStamp = new PropertyValidator<DateTime>(nameof(TimeStamp), new DateTime(2000, 01, 01), new DateTime(2099, 12, 31), true);
        public static readonly PropertyValidator<int> Value = new PropertyValidator<int>(nameof(Value), 0, 10, true);
    }
}
