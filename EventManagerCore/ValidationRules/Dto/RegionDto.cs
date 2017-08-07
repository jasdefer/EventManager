using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidationRules.Dto
{
    public class RegionDto
    {
        public int Id { get; set; }
        public int? Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Polygon { get; set; }
        IEnumerable<int> VisitorIds { get; set; }
    }
}
