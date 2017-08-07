using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRules.PropertyValidation;
using ValidationRules.ValidationExceptions;

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

        public bool IsValid(bool throwException = true)
        {
            if (!RegionValidation.Name.IsValid(Name, throwException)) return false;
            if (!RegionValidation.Description.IsValid(Description, throwException)) return false;
            if (!RegionValidation.Polygon.IsValid(Polygon, throwException)) return false;
            if (!RegionValidation.TimeStamp.IsValid(TimeStamp, throwException)) return false;

            return true;
        }
        

    }
}

