using DataLayer.DataModel;
using DataLayer.Repository;
using DataLayer.Repository.MemoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.RepositoryTests
{
    public abstract class RegionRepositoryTestFixture : RepositoryTestFixture<Region,int>
    {
        protected override IRepository<Region, int> GetRepository()
        {
            return new RegionMemoryRepository();
        }

        protected override Region UpdateEntity(Region entity)
        {
            entity.TimeStamp += TimeSpan.FromDays(1);
            return entity;
        }
    }
}
