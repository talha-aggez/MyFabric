using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductWithProductTypeAsync();
        Task<List<Product>> GetProductNotSalableAsync();
        Task<List<Product>> GetProductSalableAsync();

    }
}
