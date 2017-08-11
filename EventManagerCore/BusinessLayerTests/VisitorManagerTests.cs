using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using AutoMapper;
using BusinessLayer.Mapping;
using DataLayer.Repository.DatabaseRepository;
using DataLayer.Repository;
using DataTransfer;
using DataLayer.DataModel;
using System.Collections.Generic;

namespace BusinessLayerTests
{
    [TestClass]
    public class VisitorManagerTests
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventManagerBusinessTests; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";
        private readonly IVistorRepository VisitorRepository = new VisitorDatabaseRepository(TestConnectionString);
        private readonly IRegionRepository RegionRepository = new RegionDatabaseRepository(TestConnectionString);
        private readonly VisitorManager VisitorManager;

        public VisitorManagerTests()
        {
            Mapper.Initialize(m => m.AddProfile<MappingProfiles>());
            Mapper.AssertConfigurationIsValid();
            VisitorManager = new VisitorManager(VisitorRepository, Mapper.Instance, RegionRepository);
        }

        

        [TestMethod]
        public void TestAddGetDelete()
        {
            int id = VisitorManager.Add(DataGenerator.GetVisitor());
            VisitorDto visitor = VisitorManager.Get(id);
            Assert.IsNotNull(visitor);
            VisitorManager.Delete(id);
            Assert.IsNull(VisitorManager.Get(id));
        }
    }
}
