using BusinessLayer;
using DataLayer.Repository;
using DataLayer.Repository.DatabaseRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataTransfer;
using AutoMapper;
using BusinessLayer.Mapping;

namespace BusinessLayerTests
{
    [TestClass]
    public class RegionManagerTests
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventManagerBusinessTests; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";
        private readonly IVistorRepository _visitorRepository = new VisitorDatabaseRepository(TestConnectionString);
        private readonly IRegionRepository _regionRepository = new RegionDatabaseRepository(TestConnectionString);
        private readonly RegionManager _regionManager;

        public RegionManagerTests()
        {
            Mapper.Initialize(m => m.AddProfile<MappingProfiles>());
            Mapper.AssertConfigurationIsValid();
            _regionManager = new RegionManager(_visitorRepository, Mapper.Instance, _regionRepository);
        }
        [TestMethod]
        public void TestAddGetDelete()
        {
            int id = _regionManager.Add(DataGenerator.GetRegion());
            RegionDto region = _regionManager.Get(id);
            Assert.IsNotNull(region);
            _regionManager.Delete(id);
            Assert.IsNull(_regionManager.Get(id));
        }
    }
}
