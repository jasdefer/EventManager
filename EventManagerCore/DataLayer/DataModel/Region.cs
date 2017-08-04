using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.PropertyValidation;

namespace DataLayer.DataModel
{
    public class Region
    {
        public Region(int id, string name, DateTime timeStamp, string polygon)
        {
            Id = id;
            Name = name;
            TimeStamp = timeStamp;
            Polygon = polygon;

            RegionValidation.Name.IsValid(Name);
            RegionValidation.TimeStamp.IsValid(TimeStamp);
            RegionValidation.Polygon.IsValid(Polygon);
        }

        public int Id { get; set; }

        public int Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Polygon { get; set; }

        public IEnumerable<Visitor> Visitors { get; set; }

        

    }
}

