using Core.DBModels;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements
{
    public class SubProductTreeRepository : GenericRepository<SubProductTree>, ISubProductTreeRepository
    {
        public async Task<List<SubProductTree>> GetSubProductsByProductId(int id)
        {
            using var context = new StoreContext();
            return await context.SubProductTrees.Include(p => p.Product).Where(p => p.ProductID == id).ToListAsync();
        }

        public async Task<List<SubProductTree>> GetSubProductTreeWithAllAsync()
        {

            using var context = new StoreContext();
            return await context.SubProductTrees.Include(p => p.Product).ToListAsync();
        }
    }
}
