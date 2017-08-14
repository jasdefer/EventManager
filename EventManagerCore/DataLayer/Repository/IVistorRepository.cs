using DataLayer.DataModel;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    public interface IVistorRepository : IRepository<Visitor,int>
    {
        IEnumerable<int> GetAllVisiting(int visitorId);
    }
}
