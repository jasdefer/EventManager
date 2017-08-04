using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModel;
using DataLayer.Repository.MemoryRepository;
using DataLayer.Repository.DatabaseRepository;
using DataLayer.Repository;

namespace DataLayerTests.RepositoryTests.DatabaseRepositoryTests
{
    [TestClass]
    public class RegionDatabaseRepositoryTests : RegionRepositoryTestFixture
    {
        protected override IRepository<Region, int> GetRepository()
        {
            return new RegionDatabaseRepository("");
        }
    }
}
