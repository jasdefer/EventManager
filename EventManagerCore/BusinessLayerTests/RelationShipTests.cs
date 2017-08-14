using AutoMapper;
using BusinessLayer;
using BusinessLayer.BusinessExceptions;
using BusinessLayer.Mapping;
using DataLayer.Repository;
using DataLayer.Repository.DatabaseRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using DataTransfer;

namespace BusinessLayerTests
{
    [TestClass]
    public class RelationShipTests
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventManagerBusinessTests; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";
        private readonly IVistorRepository _visitorRepository = new VisitorDatabaseRepository(TestConnectionString);
        private readonly IRegionRepository _regionRepository = new RegionDatabaseRepository(TestConnectionString);
        private readonly RegionManager _regionManager;
        private readonly VisitorManager _visitorManager;

        public RelationShipTests()
        {
            Mapper.Initialize(m => m.AddProfile<MappingProfiles>());
            Mapper.AssertConfigurationIsValid();
            _regionManager = new RegionManager(_visitorRepository, Mapper.Instance, _regionRepository);
            _visitorManager = new VisitorManager(_visitorRepository, Mapper.Instance, _regionRepository);
        }

        [TestMethod]
        public void TestAddingVisitorWithRegion()
        {
            //Create data
            VisitorDto visitor = DataGenerator.GetVisitor();
            RegionDto region = DataGenerator.GetRegion();

            //Fail, when adding the visitor with an unknown region
            visitor.RegionIds = new List<int>() { 0 };
            Assert.ThrowsException<BusinessException>(() => _visitorManager.Add(visitor));

            region.Id = _regionManager.Add(region);
            visitor.RegionIds = new List<int>() { region.Id };
            visitor.Id = _visitorManager.Add(visitor);

            Assert.AreEqual(visitor.Id, _regionManager.Get(region.Id).VisitorIds.Single());
            Assert.AreEqual(region.Id, _visitorManager.Get(visitor.Id).RegionIds.Single());

            _visitorManager.Delete(visitor.Id);
            _regionManager.Delete(region.Id);
        }

        [TestMethod]
        public void TestUpdateRelationships()
        {
            //Create visitors and regions
            VisitorDto[] visitors = new VisitorDto[2];
            for (int i = 0; i < visitors.Length; i++)
            {
                visitors[i] = DataGenerator.GetVisitor();
                visitors[i].Id = _visitorManager.Add(visitors[i]);
            }
            RegionDto[] regions = new RegionDto[visitors.Length];
            for (int i = 0; i < regions.Length; i++)
            {
                regions[i] = DataGenerator.GetRegion();
                regions[i].Id = _regionManager.Add(regions[i]);
            }

            //Add relation ships
            visitors[0].RegionIds = new List<int>() { regions[0].Id };
            _visitorManager.Update(visitors[0]);
            regions[1].VisitorIds = new List<int>() { visitors[1].Id };
            _regionManager.Update(regions[1]);

            //Update the data
            for (int i = 0; i < visitors.Length; i++)
            {
                visitors[i] = _visitorManager.Get(visitors[i].Id);
                regions[i] = _regionManager.Get(regions[i].Id);
            }

            //Validate relationships
            Assert.AreEqual(regions[0].Id, visitors[0].RegionIds.Single());
            Assert.AreEqual(regions[1].Id, visitors[1].RegionIds.Single());
            Assert.AreEqual(visitors[0].Id, regions[0].VisitorIds.Single());
            Assert.AreEqual(visitors[1].Id, regions[1].VisitorIds.Single());

            //Switch relationships
            regions[1].VisitorIds = new List<int>();
            _regionManager.Update(regions[1]);
            visitors[0].RegionIds = new List<int>() { regions[1].Id };
            _visitorManager.Update(visitors[0]);
            regions[0].VisitorIds = new List<int>() { visitors[1].Id };
            _regionManager.Update(regions[0]);
            

            //Update the data
            for (int i = 0; i < visitors.Length; i++)
            {
                visitors[i] = _visitorManager.Get(visitors[i].Id);
                regions[i] = _regionManager.Get(regions[i].Id);
            }

            Assert.AreEqual(regions[0].Id, visitors[1].RegionIds.Single());
            Assert.AreEqual(regions[1].Id, visitors[0].RegionIds.Single());
            Assert.AreEqual(visitors[0].Id, regions[1].VisitorIds.Single());
            Assert.AreEqual(visitors[1].Id, regions[0].VisitorIds.Single());


            //Cleanup
            for (int i = 0; i < visitors.Length; i++)
            {
                _visitorManager.Delete(visitors[i].Id);
                _regionManager.Delete(regions[i].Id);
            }
        }
    }
}
