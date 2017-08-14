using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules.PropertyValidation
{
    public static class VisitorValidation
    {
        public static readonly StringPropertyValidator Username = new StringPropertyValidator(nameof(Username), 3, 64, true);
        public static readonly StringPropertyValidator PasswordHash = new StringPropertyValidator(nameof(PasswordHash), null, null, true);
        public static readonly StringPropertyValidator Email = new StringPropertyValidator(nameof(Email), 3, null, true);
        public static readonly StringPropertyValidator Bio = new StringPropertyValidator(nameof(Bio), 0, 1024, false);
    }
}
