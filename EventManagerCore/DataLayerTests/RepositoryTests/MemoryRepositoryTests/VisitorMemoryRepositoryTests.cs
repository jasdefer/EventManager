﻿using DataLayer.DataModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Repository;
using DataLayer.Repository.MemoryRepository;

namespace DataLayerTests.RepositoryTests.MemoryRepositoryTests
{
    [TestClass]
    public class VisitorMemoryRepositoryTests : VisitorRepositoryTestFixture
    {
        protected override IRepository<Visitor, int> GetRepository()
        {
            return new VisitorMemoryRepository();
        }
    }
}