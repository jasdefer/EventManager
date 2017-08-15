using DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataLayer.Repository
{
    public interface IRegionRepository : IRepository<RegionDto,int>
    {
        Task RemoveVisitorAsync(int regionId, int visitorId);
    }
}
