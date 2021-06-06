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
        public async Task<Order> FindByIDWithOrderItemsAsync(int id)
        {
            using var context = new StoreContext();
            return await context.Orders.Include(p => p.OrderItems).Where(p => p.ID == id).FirstOrDefaultAsync();
        }

        public async  Task<List<DualHelper>> GetMostActive3PersonAsync()
        {
            using var context = new StoreContext();
           
            var list =await  context.Orders.Include(p => p.AppUser).GroupBy(p => p.AppUser.Name).OrderByDescending(p => p.Count()).Take(3).
               Select(p => new DualHelper
               {
                   Name = p.Key,
                   Number = p.Count()
               }).ToListAsync();
            return list;
        }

        public async Task<List<DualHelper>> GetMostActive3ProductAsync()
        {
            using var context = new StoreContext();
            var list =await context.OrderItems.Include(p => p.Product).GroupBy(p => p.Product.ProductName).OrderByDescending(p => p.Count()).Take(3).
              Select(p => new DualHelper
              {
                  Name = p.Key,
                  Number = p.Count()
              }).ToListAsync();
            return list;
        }

        public async Task<List<Order>> GetOrdersFromAppUserIdAsync(int customerId)
        {
            using var context = new StoreContext();
   
   
            return await context.Orders.Include(p=>p.OrderItems).ThenInclude(q=>q.Product).Where(p=>p.CustomerID==customerId).ToListAsync();
        }

        public async Task<List<Order>> GetOrdersWithAllAsync()
        {
            using var context = new StoreContext();


            return await context.Orders.Include(p => p.OrderItems).ThenInclude(q => q.Product).Include(r=>r.AppUser).ToListAsync();
        }

        public async  Task<int> GetTodayOrderCountAsync()
        {
            using var context = new StoreContext();
       
            var todayOrderCount =context.Orders.Where(p => p.OrderDate.Day == DateTime.Now.Day && p.OrderDate.Month==DateTime.Now.Month && p.OrderDate.Year == DateTime.Now.Year).Count();
            return todayOrderCount;
          
        
        }

        public int GetTotalOrderCountAsync()
        {
            using var context = new StoreContext();
            return  context.Orders.Count();
        }
    }
}
