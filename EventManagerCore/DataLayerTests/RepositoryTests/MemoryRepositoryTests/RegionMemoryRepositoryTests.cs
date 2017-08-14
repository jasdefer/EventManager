using Microsoft.VisualStudio.TestTools.UnitTesting;
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
