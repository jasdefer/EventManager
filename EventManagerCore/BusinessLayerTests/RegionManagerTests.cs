using BusinessLayer;
using DataLayer.Repository;
using DataLayer.Repository.DatabaseRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransfer;
using AutoMapper;
using BusinessLayer.Mapping;

namespace BusinessLayerTests
{
    [TestClass]
    public class RegionManagerTests
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventManagerBusinessTests; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";
        private readonly IVistorRepository VisitorRepository = new VisitorDatabaseRepository(TestConnectionString);
        private readonly IRegionRepository RegionRepository = new RegionDatabaseRepository(TestConnectionString);
        private readonly RegionManager RegionManager;

        public RegionManagerTests()
        {
            Mapper.Initialize(m => m.AddProfile<MappingProfiles>());
            Mapper.AssertConfigurationIsValid();
            RegionManager = new RegionManager(VisitorRepository, Mapper.Instance, RegionRepository);
        }
        [TestMethod]
        public void TestAddGetDelete()
        {
            int id = RegionManager.Add(DataGenerator.GetRegion());
            RegionDto region = RegionManager.Get(id);
            Assert.IsNotNull(region);
            RegionManager.Delete(id);
            Assert.IsNull(RegionManager.Get(id));
        }
    }
}
