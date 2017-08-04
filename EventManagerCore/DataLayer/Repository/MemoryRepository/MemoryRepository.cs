using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository.MemoryRepository
{
    public abstract class MemoryRepository<T, U> : IRepository<T, U> where T : IEntity<U> where U : IComparable
    {
        protected U Id = default(U);
        protected List<T> Entities = new List<T>();

        public T Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Id = GetNextId();
            Entities.Add(entity);
            return entity;
        }

        protected abstract U GetNextId();

        public void Delete(U id)
        {
            T entity = Get(id);
            if (entity == null) throw new KeyNotFoundException($"No entity with the id {id} found.");
            Entities.Remove(entity);
        }

        public T Get(U id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            T entity = Entities.SingleOrDefault(e => e.Id.CompareTo(id)==0);
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            T oldEntity = Get(entity.Id);
            if (oldEntity == null) throw new KeyNotFoundException($"No entity with the id {entity.Id} found.");
            oldEntity = entity;
        }
    }
}
