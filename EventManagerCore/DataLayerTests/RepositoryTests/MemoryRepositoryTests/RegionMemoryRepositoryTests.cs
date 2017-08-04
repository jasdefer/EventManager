using DataLayer.DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repository;
using DataLayer.Repository.MemoryRepository;

namespace DataLayerTests.RepositoryTests.MemoryRepositoryTests
{
    [TestClass]
    public class RegionMemoryRepositoryTests : RegionRepositoryTestFixture
    {
        protected override Region CreateEntity()
        {
            return new Region("testregion", new DateTime(2010, 01, 01), "testpolygon");
        }
    }
}
