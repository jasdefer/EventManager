using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer.Repository.MemoryRepository;

namespace DataLayerTests.RepositoryTests.MemoryRepositoryTests
{
    [TestClass]
    public class VisitorMemoryRepositoryTests : VisitorRepositoryTestFixture<VisitorMemoryRepository>
    {
        protected override VisitorMemoryRepository GetRepository()
        {
            return new VisitorMemoryRepository();
        }
    }
}
