using DataLayer.DataModel;
using DataLayer.Repository;
using DataLayer.Repository.MemoryRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.RepositoryTests
{
    public abstract class RegionRepositoryTestFixture<V> : RepositoryTestFixture<Region,int,V> where V:IRegionRepository
    {
        public abstract IVistorRepository VisitorRepository { get; }
        protected override Region CreateEntity()
        {
            return new Region
            {
                Name = "testregion",
                TimeStamp = new DateTime(2010,01,01),
                Polygon = "testpolygon",
            };
        }

        protected override Region UpdateEntity(Region entity)
        {
            entity.TimeStamp += TimeSpan.FromDays(1);
            return entity;
        }

        [TestMethod]
        public void TestGetAfterDate()
        {
            Region region = CreateEntity();
            Repository.Add(region);
            var regions = Repository.GetAllAfter(region.TimeStamp.AddDays(-1));
            Assert.IsNotNull(regions);
            Assert.IsTrue(regions.Count() > 0);
        }

        [TestMethod]
        public void TestAddVisitor()
        {
            Region region = CreateEntity();
            region = Repository.Add(region);

            Visitor visitor = new Visitor()
            {
                Email = "RandomVisitor@email.org",
                Username = "RandomVisitor",
                PasswordHash = "SuperSecurePasswordHash123",
            };
            visitor = VisitorRepository.Add(visitor);

            Repository.AddVisitor(region.Id, visitor.Id);
            var visitors = Repository.GetAllVisitors(region.Id);

            Assert.IsNotNull(visitors);
            Assert.AreEqual(1, visitors.Count());
            Assert.IsTrue(visitors.Single().CompareTo(visitor.Id) == 0);
        }

        [TestMethod]
        public void TestRemoveVisitor()
        {
            Region region = CreateEntity();
            region = Repository.Add(region);

            Visitor visitor = new Visitor()
            {
                Email = "RandomVisitor@email.org",
                Username = "RandomVisitor",
                PasswordHash = "SuperSecurePasswordHash123",
            };
            visitor = VisitorRepository.Add(visitor);

            Repository.AddVisitor(region.Id, visitor.Id);
            Repository.RemoveVisitor(region.Id, visitor.Id);
            Assert.AreEqual(0,Repository.GetAllVisitors(region.Id).Count());
        }
    }
}
