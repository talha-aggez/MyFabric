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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public async Task<List<Product>> GetProductNotSalableAsync()
        {
            using var context = new StoreContext();
            return await context.Products.Include(p => p.ProductType).Where(p=> p.IsSalable==false).ToListAsync();
                    
        }

        public async Task<List<Product>> GetProductSalableAsync()
        {
            using var context = new StoreContext();
            return await context.Products.Include(p => p.ProductType).Where(p => p.IsSalable == true).ToListAsync();
        }

        public async Task<List<Product>> GetProductWithProductTypeAsync()
        {
            using var context = new StoreContext();
            return await context.Products.Include(p => p.ProductType).ToListAsync();


        }

        public int GetTotalProductCount()
        {
            using var context = new StoreContext();
            return context.Products.Count(); 
        }

        public async Task<List<Product>> SearchProductAsync(string key)
        {
            using var context = new StoreContext();
            return await context.Products.Where(x => x.ProductName.Contains(key)).ToListAsync();

        }
    }
}
