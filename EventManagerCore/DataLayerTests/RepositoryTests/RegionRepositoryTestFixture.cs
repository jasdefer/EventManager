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
        protected override Region CreateEntity()
        {
            return new Region("testregion", new DateTime(2010, 01, 01), "testpolygon");
        }

        protected override Region UpdateEntity(Region entity)
        {
            entity.TimeStamp += TimeSpan.FromDays(1);
            return entity;
        }
    }
}
