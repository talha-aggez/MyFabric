using Core.DBModels;
using MyFabric.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISubProductTreeRepository : IGenericRepository<SubProductTree>
    {
        Task<List<SubProductTree>> GetSubProductTreeWithAllAsync();
        Task<List<SubProductTree>> GetSubProductsByProductId(int id);

    }
}
