using Core.DBModels;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements
{
    public class OperationRepository : GenericRepository<Operation>, IOperationRepository
    {
        public async Task<List<Operation>> GetOperationWithProductTypeAsync()
        {
            using var context = new StoreContext();
            return await context.Operations.Include(p => p.ProductType).ToListAsync();

        }
    }
}
