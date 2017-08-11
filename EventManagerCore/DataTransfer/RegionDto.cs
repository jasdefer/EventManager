using System;
using System.Collections;
using System.Collections.Generic;

namespace DataTransfer
{
    public class RegionDto
    {
        public int Id { get; set; }
        public int? Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Polygon { get; set; }

        public IEnumerable<int> VisitorIds { get; set; }
    }
}
