using DataLayer.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IVistorRepository : IRepository<Visitor,int>
    {
        IEnumerable<Visitor> GetAllOfRegion(Region region);
    }
}
