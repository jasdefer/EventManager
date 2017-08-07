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
    public class RegionMemoryRepositoryTests : RegionRepositoryTestFixture<RegionMemoryRepository>
    {
        public override IVistorRepository VisitorRepository => new VisitorMemoryRepository();

        protected override RegionMemoryRepository GetRepository()
        {
            return new RegionMemoryRepository(new VisitorMemoryRepository());
        }
    }
}
