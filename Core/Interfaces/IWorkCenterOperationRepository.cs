using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IWorkCenterOperationRepository : IGenericRepository<WorkCenterOperation>
    {
        Task<List<WorkCenterOperation>> GetWorkCenterOperationWithAllAsync();
    }
}
