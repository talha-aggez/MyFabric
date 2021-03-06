using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWorkCenterRepository : IGenericRepository<WorkCenter>
    {
        Task<List<WorkCenter>> GetWorkCenterWithProductIdAsync(int productId);
        int GetWorkCenterTotalCount();
        int GetActiveWorkCenterTotalCount();
    }
}
