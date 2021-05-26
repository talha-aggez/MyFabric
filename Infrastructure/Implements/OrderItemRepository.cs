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
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public async  Task<List<OrderItem>> GetOrderItemsFromOrderIdAsync(int orderId)
        {
            using var context = new StoreContext();
            return await context.OrderItems.Where(P=>P.OrderID==orderId).ToListAsync();
        }
    }
}
