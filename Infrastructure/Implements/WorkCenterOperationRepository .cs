using Core.DBModels;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using MyFabric.Infrastructure.Implements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implements
{
    public class WorkCenterOperationRepository : GenericRepository<WorkCenterOperation>, IWorkCenterOperationRepository
    {
        public async Task<List<WorkCenterOperation>> GetWorkCenterOperationWithAllAsync()
        {
            using var context = new StoreContext();
            return await context.WorkCenterOperations.Include(p => p.WorkCenter).Include(q=>q.Operation).ToListAsync();
        }
    }
}
