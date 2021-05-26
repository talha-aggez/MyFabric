using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<List<OrderItem>> GetOrderItemsFromOrderIdAsync(int orderId);
    }
}
