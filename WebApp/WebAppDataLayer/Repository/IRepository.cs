using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppDataLayer.Repository
{
    public interface IRepository<T, TU>  where TU : IComparable where T:class
    {
        Task<T> GetAsync(TU id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(TU id);
    }
}
