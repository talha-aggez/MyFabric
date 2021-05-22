using Core.DBModels;
using MyFabric.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAppUserRepository: IGenericRepository<AppUser>
    {
        Task<AppUser> FindByUserName(string userName);
        Task<bool> CheckPassword(string name,string password);
        Task<List<AppRole>> GetRolesByUserName(string userName);
    }
}
