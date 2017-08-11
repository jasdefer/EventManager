using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;

namespace BusinessLayerTests
{
    public static class DataGenerator
    {
        public static VisitorDto GetVisitor()
        {
            return new VisitorDto()
            {
                Email = "AddTestUser@email.de",
                Username = "AddTestUser",
                PasswordHash = "SecurePasswordHash"
            };
        }

        public static RegionDto GetRegion()
        {
            return new RegionDto()
            {
                Name = "Test",
                Description = "This is a temporary test region.",
                TimeStamp = new DateTime(2010, 01, 01),
                Polygon = "ploygon placeholder",
                Value = 5,
            };
        }
    }
}
