using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataLayer.Repository.DatabaseRepository;
using System.Data.SqlClient;
using Dapper;

namespace DataLayerTests.RepositoryTests.DatabaseRepositoryTests
{
    [TestClass]
    public class VisitorDatabaseRepositoryTests : VisitorRepositoryTestFixture<VisitorDatabaseRepository>
    {
        protected static string TestConnectionString = @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog = EventTestVisitorsDb; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False;Pooling=false";

        protected override VisitorDatabaseRepository GetRepository()
        {
            return new VisitorDatabaseRepository(TestConnectionString);
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
