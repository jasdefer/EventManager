using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using AutoMapper;
using BusinessLayer.Mapping;
using DataLayer.Repository.DatabaseRepository;
using DataLayer.Repository;
using DataTransfer;

namespace BusinessLayerTests
{
    [TestClass]
    public class VisitorManagerTests
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventManagerBusinessTests; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";
        private readonly IVistorRepository _visitorRepository = new VisitorDatabaseRepository(TestConnectionString);
        private readonly IRegionRepository _regionRepository = new RegionDatabaseRepository(TestConnectionString);
        private readonly VisitorManager _visitorManager;

        public VisitorManagerTests()
        {
            Mapper.Initialize(m => m.AddProfile<MappingProfiles>());
            Mapper.AssertConfigurationIsValid();
            _visitorManager = new VisitorManager(_visitorRepository, Mapper.Instance, _regionRepository);
        }

        

        [TestMethod]
        public void TestAddGetDelete()
        {
            int id = _visitorManager.Add(DataGenerator.GetVisitor());
            VisitorDto visitor = _visitorManager.Get(id);
            Assert.IsNotNull(visitor);
            _visitorManager.Delete(id);
            Assert.IsNull(_visitorManager.Get(id));
        }
    }
}
