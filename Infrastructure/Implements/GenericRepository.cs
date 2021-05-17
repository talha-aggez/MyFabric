
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFabric.Infrastructure.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        public async Task AddAsync(T entity)
        {
            using var context = new StoreContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            using var context = new StoreContext();
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAllAsync()
        {
            using var context = new StoreContext();
            return await context.Set<T>().ToListAsync();
        }
       
        public async Task RemoveAsync(int id)
        {
            using var context = new StoreContext();
            var temp = await context.Set<T>().FindAsync(id);
            context.Remove(temp);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {

            using var context = new StoreContext();
            context.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}

