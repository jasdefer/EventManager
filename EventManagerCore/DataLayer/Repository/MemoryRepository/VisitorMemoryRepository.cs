using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModel;

namespace DataLayer.Repository.MemoryRepository
{
    public class VisitorMemoryRepository : MemoryRepository<Visitor, int>, IVistorRepository
    {
        public IEnumerable<Region> GetAllVisiting(int userId)
        {
            Visitor visitor = Get(userId);
            if (visitor == null) throw new KeyNotFoundException($"No user with the id {userId} found.");
            return visitor.Regions;
        }

        protected override int GetNextId()
        {
            return ++Id;
        }
    }
}
