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
    public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
    {
        public async Task<List<Schedule>> GetScheduleByOrderIdAndProductIdAsync(int orderId, int productId)
        {
            using var context = new StoreContext();
            return await context.Schedules.Include(p => p.WorkCenter).Where(p => p.ProductID==productId && p.OrderID==orderId).ToListAsync();
        }
    }
}
