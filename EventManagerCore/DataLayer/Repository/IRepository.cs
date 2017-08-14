using System;
using System.Collections.Generic;

namespace DataLayer.Repository
{
    public interface IRepository<T, in TU> where T:IEntity<TU> where TU:IComparable
    {
        T Get(TU id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        void Update(T entity);
        void Delete(TU id);
    }
}
