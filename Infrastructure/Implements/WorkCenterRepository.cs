using Core.DBModels;
using Core.Interfaces;
using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements
{
    public class WorkCenterRepository : GenericRepository<WorkCenter>,IWorkCenterRepository
    {
        public Task AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
