using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<List<Order>> GetOrdersFromAppUserIdAsync(int customerId);
        Task<List<Order>> GetOrdersWithAllAsync();
        Task<int> GetTodayOrderCountAsync();
        int GetTotalOrderCountAsync();
        Task<List<DualHelper>> GetMostActive3PersonAsync();
        Task<List<DualHelper>> GetMostActive3ProductAsync();
        Task<Order> FindByIDWithOrderItemsAsync(int id);

    }
}
