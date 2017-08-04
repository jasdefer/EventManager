using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModel;

namespace DataLayer.Repository.MemoryRepository
{
    public class RegionMemoryRepository : MemoryRepository<Region, int>, IRegionRepository
    {
        public IEnumerable<Region> GetAllAfter(DateTime time)
        {
            return Entities.Where(x => x.TimeStamp > time);
        }

        public IEnumerable<Visitor> GetAllVisitors(int regionId)
        {
            Region region = Get(regionId);
            if (region == null) throw new KeyNotFoundException($"No region with the id {regionId} found.");
            return region.Visitors;
        }

        protected override int GetNextId()
        {
            return Id++;
        }
    }
}
