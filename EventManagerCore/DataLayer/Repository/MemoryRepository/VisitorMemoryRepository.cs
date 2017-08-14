using System.Collections.Generic;
using System.Linq;
using DataLayer.DataModel;

namespace DataLayer.Repository.MemoryRepository
{
    public class VisitorMemoryRepository : MemoryRepository<Visitor, int>, IVistorRepository
    {
        public IEnumerable<int> GetAllVisiting(int userId)
        {
            Visitor visitor = Get(userId);
            if (visitor == null) throw new KeyNotFoundException($"No user with the id {userId} found.");
            return visitor.Regions.Select(r => r.Id);
        }

        protected override int GetNextId()
        {
            return ++Id;
        }
    }
}
