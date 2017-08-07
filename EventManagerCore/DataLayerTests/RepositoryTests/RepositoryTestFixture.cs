using DataLayer.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayerTests.RepositoryTests
{
    [TestClass]
    public abstract class RepositoryTestFixture<T,U> where T:IEntity<U> where U:IComparable
    {
        private IRepository<T, U> Repository;
        

        [TestInitialize]
        public void Setup()
        {
            Repository = GetRepository();
        }

        [TestMethod]
        public void TestAdd()
        {
            T entity = CreateEntity();
            Repository.Add(entity);
            Assert.AreNotEqual(0,entity.Id.CompareTo(default(U)));
        }

        [TestMethod]
        public void TestGet()
        {
            T entity = CreateEntity();
            Repository.Add(entity);
            T otherEntity = Repository.Get(entity.Id);
            Assert.AreEqual(0, entity.Id.CompareTo(otherEntity.Id));
        }

        [TestMethod]
        public void TestDelete()
        {
            T entity = CreateEntity();
            Repository.Add(entity);
            U id = entity.Id;
            Repository.Delete(id);
            Assert.AreEqual(null, Repository.Get(id));
        }

        [TestMethod]
        public void TestUpdate()
        {
            T entity = CreateEntity();
            Repository.Add(entity);
            T updated = UpdateEntity(entity);
            Repository.Update(updated);
            Assert.IsNotNull(updated);
        }

        

        protected abstract T UpdateEntity(T entity);
        protected abstract T CreateEntity();
        protected abstract IRepository<T, U> GetRepository();
    }
}
