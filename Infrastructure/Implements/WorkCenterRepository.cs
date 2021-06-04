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
    public class WorkCenterRepository : GenericRepository<WorkCenter>, IWorkCenterRepository
    {
        public async Task<List<WorkCenter>> GetWorkCenterWithProductIdAsync(int productId)
        {
            using var context = new StoreContext();
            var x= await context.Products.Include(p => p.ProductType).ThenInclude(p => p.Operations).ThenInclude(p => p.WorkCenterOperations).ThenInclude(p => p.WorkCenter).Where(p => p.ID == productId).Where(p => p.ID == productId).ToListAsync();
            var workCenterList = new List<WorkCenter>();
            foreach (var item in x)
            {
                var y = item.ProductType;
                foreach (var item2 in y.Operations)
                {
                    foreach (var item3 in item2.WorkCenterOperations)
                    {
                        workCenterList.Add(item3.WorkCenter);
                    }

                }

            }
            return workCenterList;
        }
    }
}
