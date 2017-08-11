using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.DataModel;

namespace DataLayer.Repository.MemoryRepository
{
    public class RegionMemoryRepository : MemoryRepository<Region, int>, IRegionRepository
    {
        private readonly VisitorMemoryRepository _visitorMemoryRepository;


        public RegionMemoryRepository(VisitorMemoryRepository visitorMemoryRepository)
        {
            _visitorMemoryRepository = visitorMemoryRepository;
        }

        public void AddVisitor(int regionId, int visitorId)
        {
            Region region = Get(regionId);
            if (region == null) throw new KeyNotFoundException($"No region with the id {regionId} found.");

            Visitor visitor = _visitorMemoryRepository.Get(visitorId);
            if (visitor == null) throw new KeyNotFoundException($"No visitor with the id {visitorId} found.");

            var visitors = region.Visitors?.ToList() ?? new List<Visitor>();
            visitors.Add(visitor);

            region.Visitors = visitors;
        }

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

        public void RemoveAllVisitors(int regionId)
        {
            Region region = Get(regionId);
            if (region == null) throw new KeyNotFoundException($"No region with the id {regionId} found.");

            region.Visitors = new List<Visitor>();
        }

        public void RemoveVisitor(int regionId, int visitorId)
        {
            Region region = Get(regionId);
            if (region == null) throw new KeyNotFoundException($"Cannot find a region with the id {regionId}.");

            var visitors = region.Visitors.ToList();
            visitors.Remove(region.Visitors.Single(v => v.Id == visitorId));
            region.Visitors = visitors;
        }

        protected override int GetNextId()
        {
            return ++Id;
        }

        IEnumerable<int> IRegionRepository.GetAllVisitors(int regionId)
        {
            Region region = Get(regionId);
            if (region == null) throw new KeyNotFoundException($"No region with the id {regionId} found.");

            return region.Visitors.Select(v => v.Id);
        }
    }
}
