using DataLayer.DataModel;
using System;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    public interface IRegionRepository : IRepository<Region, int>
    {
        IEnumerable<Region> GetAllAfter(DateTime time);
        IEnumerable<int> GetAllVisitors(int regionId);
        void AddVisitor(int regionId, int visitorId);
        void RemoveVisitor(int regionId, int visitorId);
        void RemoveAllVisitors(int regionId);
    }
}
