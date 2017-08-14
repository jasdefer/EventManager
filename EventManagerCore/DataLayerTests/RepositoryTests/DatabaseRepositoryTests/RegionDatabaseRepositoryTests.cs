using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer.Repository.DatabaseRepository;
using DataLayer.Repository;
using System.Data.SqlClient;
using Dapper;

namespace DataLayerTests.RepositoryTests.DatabaseRepositoryTests
{
    [TestClass]
    public class RegionDatabaseRepositoryTests : RegionRepositoryTestFixture<RegionDatabaseRepository>
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventTestRegionsDb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";

        public override IVistorRepository VisitorRepository => new VisitorDatabaseRepository(TestConnectionString);

        protected override RegionDatabaseRepository GetRepository()
        {
            return new RegionDatabaseRepository(TestConnectionString);
        }

        [ClassCleanup]
        public static void DropDatabase()
        {
            var builder = new SqlConnectionStringBuilder(TestConnectionString);
            string dbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";

            using (var sql = new SqlConnection(builder.ConnectionString))
            {
                sql.Execute($"DROP DATABASE {dbName};");
            }
        }
    }
}
