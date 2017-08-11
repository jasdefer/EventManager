using AutoMapper;
using BusinessLayer;
using BusinessLayer.BusinessExceptions;
using BusinessLayer.Mapping;
using DataLayer.DataModel;
using DataLayer.Repository;
using DataLayer.Repository.DatabaseRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;

namespace BusinessLayerTests
{
    [TestClass]
    public class RelationShipTests
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventManagerBusinessTests; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";
        private readonly IVistorRepository VisitorRepository = new VisitorDatabaseRepository(TestConnectionString);
        private readonly IRegionRepository RegionRepository = new RegionDatabaseRepository(TestConnectionString);
        private readonly RegionManager RegionManager;
        private readonly VisitorManager VisitorManager;

        public RelationShipTests()
        {
            Mapper.Initialize(m => m.AddProfile<MappingProfiles>());
            Mapper.AssertConfigurationIsValid();
            RegionManager = new RegionManager(VisitorRepository, Mapper.Instance, RegionRepository);
            VisitorManager = new VisitorManager(VisitorRepository, Mapper.Instance, RegionRepository);
        }

        [TestMethod]
        public void TestAddingVisitorWithRegion()
        {
            //Create data
            VisitorDto visitor = DataGenerator.GetVisitor();
            RegionDto region = DataGenerator.GetRegion();

            //Fail, when adding the visitor with an unknown region
            visitor.RegionIds = new List<int>() { 0 };
            Assert.ThrowsException<BusinessException>(() => VisitorManager.Add(visitor));

            region.Id = RegionManager.Add(region);
            visitor.RegionIds = new List<int>() { region.Id };
            visitor.Id = VisitorManager.Add(visitor);

            Assert.AreEqual(visitor.Id, RegionManager.Get(region.Id).VisitorIds.Single());
            Assert.AreEqual(region.Id, VisitorManager.Get(visitor.Id).RegionIds.Single());

            VisitorManager.Delete(visitor.Id);
            RegionManager.Delete(region.Id);
        }

        [TestMethod]
        public void TestUpdateRelationships()
        {
            //Create visitors and regions
            VisitorDto[] visitors = new VisitorDto[2];
            for (int i = 0; i < visitors.Length; i++)
            {
                visitors[i] = DataGenerator.GetVisitor();
                visitors[i].Id = VisitorManager.Add(visitors[i]);
            }
            RegionDto[] regions = new RegionDto[visitors.Length];
            for (int i = 0; i < regions.Length; i++)
            {
                regions[i] = DataGenerator.GetRegion();
                regions[i].Id = RegionManager.Add(regions[i]);
            }

            //Add relation ships
            visitors[0].RegionIds = new List<int>() { regions[0].Id };
            VisitorManager.Update(visitors[0]);
            regions[1].VisitorIds = new List<int>() { visitors[1].Id };
            RegionManager.Update(regions[1]);

            //Update the data
            for (int i = 0; i < visitors.Length; i++)
            {
                visitors[i] = VisitorManager.Get(visitors[i].Id);
                regions[i] = RegionManager.Get(regions[i].Id);
            }

            //Validate relationships
            Assert.AreEqual(regions[0].Id, visitors[0].RegionIds.Single());
            Assert.AreEqual(regions[1].Id, visitors[1].RegionIds.Single());
            Assert.AreEqual(visitors[0].Id, regions[0].VisitorIds.Single());
            Assert.AreEqual(visitors[1].Id, regions[1].VisitorIds.Single());

            //Switch relationships
            regions[1].VisitorIds = new List<int>();
            RegionManager.Update(regions[1]);
            visitors[0].RegionIds = new List<int>() { regions[1].Id };
            VisitorManager.Update(visitors[0]);
            regions[0].VisitorIds = new List<int>() { visitors[1].Id };
            RegionManager.Update(regions[0]);
            

            //Update the data
            for (int i = 0; i < visitors.Length; i++)
            {
                visitors[i] = VisitorManager.Get(visitors[i].Id);
                regions[i] = RegionManager.Get(regions[i].Id);
            }

            Assert.AreEqual(regions[0].Id, visitors[1].RegionIds.Single());
            Assert.AreEqual(regions[1].Id, visitors[0].RegionIds.Single());
            Assert.AreEqual(visitors[0].Id, regions[1].VisitorIds.Single());
            Assert.AreEqual(visitors[1].Id, regions[0].VisitorIds.Single());


            //Cleanup
            for (int i = 0; i < visitors.Length; i++)
            {
                VisitorManager.Delete(visitors[i].Id);
                RegionManager.Delete(regions[i].Id);
            }
        }
    }
}
