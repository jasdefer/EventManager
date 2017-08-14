using System;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repository.MemoryRepository
{
    public abstract class MemoryRepository<T, TU> : IRepository<T, TU> where T : IEntity<TU> where TU : IComparable
    {
        protected static TU Id = default(TU);
        protected static List<T> Entities = new List<T>();

        public T Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Id = GetNextId();
            Entities.Add(entity);
            return entity;
        }

        protected abstract TU GetNextId();

        public void Delete(TU id)
        {
            T entity = Get(id);
            if (entity == null) throw new KeyNotFoundException($"No entity with the id {id} found.");
            Entities.Remove(entity);
        }

        public T Get(TU id)
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
            Entities[Entities.IndexOf(oldEntity)] = entity;
        }
    }
}
