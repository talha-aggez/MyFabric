
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFabric.Core.Interfaces
{
    public interface IGenericRepository<T> where T : new()
    {
        Task AddAsync(T entity);
        Task RemoveAsync(int id);
        Task UpdateAsync(T entity);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> GetAllAsync();


    }
}
