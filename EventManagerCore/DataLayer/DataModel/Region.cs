using DataLayer.Repository;
using System;
using System.Collections.Generic;

namespace DataLayer.DataModel
{
    public class Region : IEntity<int>
    {
        public int Id { get; set; }
        public int? Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Polygon { get; set; }

        public IEnumerable<Visitor> Visitors { get; set; }
    }
}

