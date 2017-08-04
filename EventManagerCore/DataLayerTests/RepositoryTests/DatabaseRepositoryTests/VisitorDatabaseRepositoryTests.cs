using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModel;
using DataLayer.Repository;
using DataLayer.Repository.DatabaseRepository;

namespace DataLayerTests.RepositoryTests.DatabaseRepositoryTests
{
    [TestClass]
    public class VisitorDatabaseRepositoryTests : VisitorRepositoryTestFixture
    {
        protected override IRepository<Visitor, int> GetRepository()
        {
            return new VisitorDatabaseRepository("");
        }
    }
}
