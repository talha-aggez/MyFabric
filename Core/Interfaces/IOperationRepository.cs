using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOperationRepository : IGenericRepository<Operation>
    {
        Task<List<Operation>> GetOperationWithProductTypeAsync();
    }
}
