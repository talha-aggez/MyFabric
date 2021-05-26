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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public async Task<List<Order>> GetOrdersFromAppUserIdAsync(int customerId)
        {
            using var context = new StoreContext();
   
   
            return await context.Orders.Where(p=>p.CustomerID==customerId).ToListAsync();
        }

       
    }
}
